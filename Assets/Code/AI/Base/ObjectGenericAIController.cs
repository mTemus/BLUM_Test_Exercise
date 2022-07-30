using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectGenericAIController : NestedComponent
{
    public SimpleValue<AIState> CurrentAIState = new SimpleValue<AIState>(true);
    protected List<AIState> m_aiStates;
    protected AIStateType m_previousAIState;

    private void CreateObjectAI()
    {
        var objectEvents = GetComponentFromRoot<ObjectEventsContainer>();
        objectEvents.SubscribeToEvent(AIEvents.OnStateChangeRequest, OnStateChangeRequest);
        objectEvents.SubscribeToEvent(AIEvents.OnGoBackToPreviousStateRequest, SetPreviousAIStateBack);

        var aiConstructors = GetComponentsInChildren<AIStateConstructorMono>();
        m_aiStates = new List<AIState>();

        foreach (var constructor in aiConstructors)
        {
            var aiState = constructor.Construct(this);

            if (constructor.IsStartingState)
            {
                CurrentAIState.Value = aiState;
                CurrentAIState.Value.OnStateSet();
            }

            m_aiStates.Add(aiState);
            constructor.Clear();
        }
    }

    private void Start()
    {
        CreateObjectAI();
    }

    private void FixedUpdate()
    {
        HandleState();
    }

    private void OnStateChangeRequest(string eventName, object data)
    {
        var newState = (AIStateType)data;
        SetNewAIState(newState);
    }

    private void SetPreviousAIStateBack(string eventName, object data)
    {
        SetNewAIState(m_previousAIState);
    }

    protected void HandleState()
    {
        CurrentAIState.Value.Update(this);
    }

    private void SetNewAIState(AIStateType stateType)
    {
        var state = m_aiStates.FirstOrDefault(state => state.AIStateType == stateType);

        if (state == null)
        {
            Debug.LogError($"Can't find AI State {stateType} in {transform.parent.name}!");
            return;
        }

        var eventsContainer = GetComponentFromRoot<ObjectEventsContainer>();
        
        eventsContainer.CallEvent(AIEvents.BeforeStateChanged, CurrentAIState);
        CurrentAIState.Value.OnStateChanged();
        m_previousAIState = CurrentAIState.Value.AIStateType;
        CurrentAIState.Value = state;
        CurrentAIState.Value.OnStateSet();
        eventsContainer.CallEvent(AIEvents.AfterStateChanged, state);
    }
}