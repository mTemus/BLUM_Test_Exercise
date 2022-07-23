using UnityEngine;
using Zenject;

public class PlayerAnimatorController : NestedComponent
{
    public string VelocityXParameterName;
    public string VelocityYParameterName;
    public string IsGroundedParameterName;

    private IEventsManager m_eventsManager;
    private Animator m_animator;
    private ObjectPhysics2DState m_physics2DState;

    private float m_velocityX;
    private float m_velocityY;
    private bool m_isGrounded;

    [Inject]
    private void Construct(IEventsManager eventsManager)
    {
        m_eventsManager = eventsManager;
    }

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_physics2DState = GetComponentInRoot<ObjectPhysics2DState>();

        m_physics2DState.IsGrounded.AddChangedListener(OnGroundedChanged);
        m_physics2DState.Velocity.AddChangedListener(OnVelocityChanged);
    }

    private void OnVelocityChanged(SimpleValueBase value)
    {
        var velocity = value.GetValueAs<Vector2>();

        if (m_velocityX != velocity.x)
        {
            m_animator.SetFloat(VelocityXParameterName, Mathf.Abs(velocity.x));
            m_velocityX = velocity.x;
        }
        
        if (m_velocityY != velocity.y)
        {
            m_animator.SetFloat(VelocityYParameterName, velocity.y);
            m_velocityY = velocity.y;
        }
    }

    private void OnGroundedChanged(SimpleValueBase value)
    {
        var isGrounded = value.GetValueAs<bool>();

        if (m_isGrounded == isGrounded)
            return;

        m_animator.SetBool(IsGroundedParameterName, isGrounded);
        m_isGrounded = isGrounded;
    }

    public void OnJumpStart()
    {
        if (m_physics2DState.IsGrounded.Value)
            return;

        m_eventsManager.CallEvent(PlayerObjectEvents.OnJumpStart, transform.parent.position);
    }

    public void OnJumpEnd()
    {
        if (!m_physics2DState.IsGrounded.Value)
            return;

        m_eventsManager.CallEvent(PlayerObjectEvents.OnJumpEnd, transform.parent.position);
    }
}
