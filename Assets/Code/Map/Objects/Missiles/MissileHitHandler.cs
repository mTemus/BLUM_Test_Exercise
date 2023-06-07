using UnityEngine;

public class MissileHitHandler : NestedComponent
{
    [SerializeField]
    protected LayerMask m_targetLayerMask;

    private void OnEnable()
    {
        GetComponentFromRoot<ObjectEventsContainer>().SubscribeToEvent(ObjectEvents.BeforeObjectDeath, OnMissileHitOrDeath);
    }

    private void OnDisable()
    {
        GetComponentFromRoot<ObjectEventsContainer>().UnsubscribeFromEvent(ObjectEvents.BeforeObjectDeath, OnMissileHitOrDeath);
    }

    private void OnMissileHitOrDeath(string eventName, object data)
    {
        if (data == null)
        {
            GetComponentInRoot<ObjectAttackState>().IsAttacking.Value = true;
        }
        else
        {
            if (1 << (data as GameObject).layer != m_targetLayerMask.value)
                return;

            GetComponentInRoot<ObjectDeathTimer>().StopAllCoroutines();
            GetComponentInRoot<ObjectAttackState>().IsAttacking.Value = true;
        }
    }
}
