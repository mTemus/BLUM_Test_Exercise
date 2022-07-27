using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCollectablesHandler : NestedComponent
{
    [SerializeField] 
    private LayerMask m_collectableMask;

    private void Awake()
    {
        GetComponentFromRoot<ObjectEventsContainer>().SubscribeToEvent(PlayerObjectEvents.OnCollisionEnter2D, OnCollect);
    }

    private void OnCollect(string eventName, object data)
    {
        var collectableCollider = data as Collision2D;

        if (1 << collectableCollider.gameObject.layer != m_collectableMask.value)
            return;

        collectableCollider.gameObject.GetComponentInChildren<CollectableItemBase>().Collect(transform.parent.gameObject);
    }
}
