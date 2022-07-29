using UnityEngine.InputSystem;

public class PlayerInteractionInputHandler : NestedComponent
{
    public string InteractionActionName;

    private ObjectInteractionHandler m_interactionHandler;

    void Awake()
    {
        GetComponentInRoot<PlayerInput>().actions.FindAction(InteractionActionName).performed += Interact;
        m_interactionHandler = GetComponentInRoot<ObjectInteractionHandler>();
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        m_interactionHandler.Interact();
    }
}