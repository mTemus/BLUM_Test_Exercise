using UnityEngine;
using Zenject;

public class PlayerHealthHandler : ObjectHealthHandler
{
    [Inject] 
    private IEventsManager m_eventsManager;

    protected override void Die()
    {
        GetComponentInRoot<Rigidbody2D>().drag = 1000f;
        GetComponentFromRoot<BoxCollider2D>().enabled = false;
        GetComponentInRoot<PlayerInputController>().BlockInput();
        m_eventsManager.CallEvent(PlayerObjectEvents.BeforePlayerDeath, null);
    }
}