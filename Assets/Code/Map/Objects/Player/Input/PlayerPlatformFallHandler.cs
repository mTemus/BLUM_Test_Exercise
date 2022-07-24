using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlatformFallHandler : NestedComponent
{
    public string FallActionName;

    private GameObject m_currentPlatform;

    void Awake()
    {
        var playerInput = GetComponentInRoot<PlayerInput>();
        playerInput.actions.FindAction(FallActionName).performed += FallThrough;
        var eventsContainer = GetComponentFromRoot<ObjectEventsContainer>();
        eventsContainer.SubscribeToEvent(PlayerObjectEvents.OnCollisionEnter2D, OnCollision2DEntered);
        eventsContainer.SubscribeToEvent(PlayerObjectEvents.OnCollisionExit2D, OnCollision2DExited);
    }

    private void OnCollision2DEntered(string eventName, object data)
    {
        var other = data as Collision2D;

        if (!other.gameObject.CompareTag("Platform"))
            return;

        if (other.gameObject.TryGetComponent<PlatformEffector2D>(out _))
            m_currentPlatform = other.gameObject;
    }

    private void OnCollision2DExited(string eventName, object data)
    {
        var other = data as Collision2D;

        if (other.gameObject == m_currentPlatform)
            m_currentPlatform = null;
    }

    private void FallThrough(InputAction.CallbackContext obj)
    {
        if (m_currentPlatform == null)
            return;

        m_currentPlatform.GetComponent<ObjectEventsContainer>().CallEvent(PlatformEvents.OnPlayerFallThrough, null);
    }
}