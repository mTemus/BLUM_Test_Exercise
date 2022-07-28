using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaChasingHandler : NestedComponent
{
    public List<OnDistanceLostAIStateSwitch> AiStateSwitches;

    [SerializeField]
    private LayerMask TargetLayerMask;

    [HideInInspector]
    public GameObject Target;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (1 << trigger.gameObject.layer != TargetLayerMask.value)
            return;

        Target = trigger.gameObject;
        GetComponentFromRoot<ObjectEventsContainer>().CallEvent(AIEvents.OnStateChangeRequest, AIStateType.Chase);

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
        GetComponentFromRoot<ObjectEventsContainer>().CallEvent(AIEvents.OnStateChangeRequest, AIStateType.Patrol);
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
