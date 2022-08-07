using UnityEngine;

public abstract class AIStateSwitch : NestedComponent
{
    [SerializeField] private AIStateType SwitchFrom;
    [SerializeField] private AIStateType SwitchTo;
    [SerializeField] private bool Condition = true;

    protected bool m_conditionValue;

    protected void Awake()
    {
        GetComponent<ObjectGenericAIController>().CurrentAIState.AddChangedListener(OnAIStateChanged);
        m_conditionValue = !Condition;
        enabled = false;
    }

    private void OnAIStateChanged(SimpleValueBase value)
    {
        var newState = value.GetValueAs<AIState>();

        if (enabled)
        {
            enabled = false;
            return;
        }

        if (newState.AIStateType == SwitchFrom)
            enabled = true;
    }

    public abstract void SetEvaluationData(object data);
    protected abstract void Evaluate();

    private void OnConditionMet()
    {
        enabled = false;
        m_conditionValue = !Condition;
        GetComponentFromRoot<ObjectEventsContainer>().CallEvent(AIEvents.OnStateChangeRequest, SwitchTo);
    }

    protected void Update()
    {
        if (m_conditionValue == Condition)
            OnConditionMet();
        else
            Evaluate();
    } 
}