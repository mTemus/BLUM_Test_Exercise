using UnityEngine;

public class Object2DAnimatorController : NestedComponent
{
    public interface AnimationEndListener
    {
        void OnAnimationEnded();
    }

    public const string VelocityXParameterName = "VelocityX";
    public const string VelocityYParameterName = "VelocityY";
    public const string IsGroundedParameterName = "IsGrounded";

    [SerializeField] 
    private readonly bool UpdateVelocity = true;

    [SerializeField] 
    private readonly bool UpdateIsGrounded = true;

    private AnimationEndListener m_listener = null;
    protected Animator m_animator;
    protected ObjectPhysics2DState m_physics2DState;

    private float m_velocityX;
    private bool m_isGrounded;
    private bool m_animStarted = true;
    private bool m_animFinished = true;

    private int m_currAnimNameHash = 0;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_physics2DState = GetComponentInRoot<ObjectPhysics2DState>();

        if (UpdateVelocity)
            m_physics2DState.Velocity.AddChangedListener(OnVelocityChanged);

        if (UpdateIsGrounded)
            m_physics2DState.IsGrounded.AddChangedListener(OnGroundedChanged);

        enabled = false;
    }

    protected virtual void OnVelocityChanged(SimpleValueBase value)
    {
        var velocity = value.GetValueAs<Vector2>();

        if (m_velocityX != velocity.x)
        {
            m_animator.SetFloat(VelocityXParameterName, Mathf.Abs(velocity.x));
            m_velocityX = velocity.x;
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

    public void PlayAnimation(string animFullPath, AnimationEndListener listener, float speed = 1f)
    {
        m_animator.Play(animFullPath, 0, 0.0f);
        PerformPlayAnimCommon(animFullPath, listener, speed);
    }

    private void PerformPlayAnimCommon(string animFullPath, AnimationEndListener listener, float speed)
    {
        m_listener = listener;

        m_currAnimNameHash = Animator.StringToHash(animFullPath);
        enabled = true;
        m_animStarted = false;
        m_animFinished = false;

        m_animator.speed *= speed;
    }

    private void Update()
    {
        var animInfo = m_animator.GetCurrentAnimatorStateInfo(0);

        if (animInfo.fullPathHash == m_currAnimNameHash)
        {
            m_animStarted = true;

            if (animInfo.normalizedTime >= 1.0f)
                m_animFinished = true;
        }
        else
        {
            if (m_animStarted)
                m_animFinished = true;
        }

        if (!m_animFinished) 
            return;

        m_animator.speed = 1.0f;

        enabled = false;

        m_listener?.OnAnimationEnded();
    }
}