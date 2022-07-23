using UnityEngine;
using Zenject;

public class PlayerAnimatorController : Object2DAnimatorController
{
    private IEventsManager m_eventsManager;
    private float m_velocityY;

    [Inject]
    private void Construct(IEventsManager eventsManager)
    {
        m_eventsManager = eventsManager;
    }

    protected override void OnVelocityChanged(SimpleValueBase value)
    {
        base.OnVelocityChanged(value);
        var velocity = value.GetValueAs<Vector2>();

        if (m_velocityY != velocity.y)
        {
            m_animator.SetFloat(VelocityYParameterName, velocity.y);
            m_velocityY = velocity.y;
        }
    }

    // Called by animation event
    public void OnJumpStart()
    {
        if (m_physics2DState.IsGrounded.Value)
            return;

        m_eventsManager.CallEvent(PlayerObjectEvents.OnJumpStart, transform.parent.position);
    }

    // Called by animation event
    public void OnJumpEnd()
    {
        if (!m_physics2DState.IsGrounded.Value)
            return;

        m_eventsManager.CallEvent(PlayerObjectEvents.OnJumpEnd, transform.parent.position);
    }
}
