using UnityEngine;

public class PlayerAnimatorController : NestedComponent
{
    public string VelocityXParameterName;
    public string VelocityYParameterName;
    public string IsGroundedParameterName;

    private Animator m_animator;
    private PlayerPhysics2DHandler m_playerPhysics2DHandler;

    private float m_velocityX;
    private float m_velocityY;
    private bool m_isGrounded;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_playerPhysics2DHandler = GetComponentInRoot<PlayerPhysics2DHandler>();

        m_playerPhysics2DHandler.IsGrounded.AddChangedListener(OnGroundedChanged);
        m_playerPhysics2DHandler.Velocity.AddChangedListener(OnVelocityChanged);
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
        if (m_playerPhysics2DHandler.IsGrounded.Value)
            return;

        EventsManager.Instance.CallEvent(PlayerObjectEvents.OnJumpStart, transform.parent.position);
    }

    public void OnJumpEnd()
    {
        if (!m_playerPhysics2DHandler.IsGrounded.Value)
            return;

        EventsManager.Instance.CallEvent(PlayerObjectEvents.OnJumpEnd, transform.parent.position);
    }
}
