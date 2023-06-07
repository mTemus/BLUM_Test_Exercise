using UnityEngine;

public class ObjectAnimator2DIsGroundedHandler : NestedComponent
{
    public const string IsGroundedParameterName = "IsGrounded";


    protected Animator m_animator;
    protected ObjectPhysics2DState m_physics2DState;

    private bool m_isGrounded;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_physics2DState = GetComponentInRoot<ObjectPhysics2DState>();

        m_physics2DState.IsGrounded.AddChangedListener(OnGroundedChanged);
    }

    private void OnGroundedChanged(SimpleValueBase value)
    {
        var isGrounded = value.GetValueAs<bool>();

        if (m_isGrounded == isGrounded)
            return;

        m_animator.SetBool(IsGroundedParameterName, isGrounded);
        m_isGrounded = isGrounded;
    }
}