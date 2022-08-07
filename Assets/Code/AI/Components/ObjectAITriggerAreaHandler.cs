using System.Collections.Generic;
using UnityEngine;

public class ObjectAITriggerAreaHandler : NestedComponent
{
    public List<OnDistanceLostAIStateSwitch> AiStateSwitches;

    [SerializeField]
    private LayerMask TargetLayerMask;

    [HideInInspector]
    public GameObject Target { get; private set; }

    public AIStateType OnTriggerEnterState;
    public AIStateType OnTriggerExitState;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (1 << trigger.gameObject.layer != TargetLayerMask.value)
            return;

        Target = trigger.gameObject;
        GetComponentFromRoot<ObjectEventsContainer>().CallEvent(AIEvents.OnStateChangeRequest, OnTriggerEnterState);

        var data = CreateDistanceData();

        for (var i = 0; i < AiStateSwitches.Count; i++)
            AiStateSwitches[i].SetEvaluationData(data);
    }

    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (Target == null)
            return;

        var data = CreateDistanceData();

        for (var i = 0; i < AiStateSwitches.Count; i++)
            AiStateSwitches[i].SetEvaluationData(data);
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (1 << trigger.gameObject.layer != TargetLayerMask.value)
            return;

        if (trigger.gameObject != Target)
            return;

        Target = null;
        GetComponentFromRoot<ObjectEventsContainer>().CallEvent(AIEvents.OnStateChangeRequest, OnTriggerExitState);
    }

    private OnDistanceLostAIStateSwitch.DistanceData CreateDistanceData()
    {
        return new OnDistanceLostAIStateSwitch.DistanceData
        {
            Target = Target.transform.position,
            Position = transform.parent.position,
        };
    }
}
