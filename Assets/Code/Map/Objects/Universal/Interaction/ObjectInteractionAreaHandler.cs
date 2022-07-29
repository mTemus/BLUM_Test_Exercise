using UnityEngine;

public class ObjectInteractionAreaHandler : NestedComponent
{
    [SerializeField] 
    private LayerMask InteractionLayer;

    private ObjectInteractionHandler m_interactionHandler;

    private void Awake()
    {
        m_interactionHandler = GetComponent<ObjectInteractionHandler>();
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (1 << trigger.gameObject.layer != InteractionLayer.value)
            return;

        if (IsInteractable(trigger.gameObject))
            m_interactionHandler.AddInteractable(trigger.gameObject);
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (1 << trigger.gameObject.layer != InteractionLayer.value)
            return;

        if (IsInteractable(trigger.gameObject))
            m_interactionHandler.RemoveInteractable(trigger.gameObject);
    }

    private bool IsInteractable(GameObject interactionObject)
    {
        return interactionObject.GetComponentInChildren<IInteractable>() != null;
    }
}
