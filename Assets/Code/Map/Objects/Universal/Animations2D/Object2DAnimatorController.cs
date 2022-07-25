using UnityEngine;

public class Object2DAnimatorController : NestedComponent
{
    public const string VelocityXParameterName = "VelocityX";
    public const string VelocityYParameterName = "VelocityY";
    public const string IsGroundedParameterName = "IsGrounded";
    public const string IsAttackingTriggerName = "IsAttacking";
    public const string GetHitTriggerName = "GetHit";
    public const string DieHitTriggerName = "Die";

    [SerializeField] 
    private bool UpdateVelocity = true;

    [SerializeField]
    private bool UpdateAttacking = true;

    [SerializeField] 
    private bool UpdateIsGrounded = true;
    
    [SerializeField] 
    private bool UpdateHealth = true;

    protected Animator m_animator;
    protected ObjectPhysics2DState m_physics2DState;

    private float m_velocityX;
    private bool m_isGrounded;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_physics2DState = GetComponentInRoot<ObjectPhysics2DState>();

        GetComponentFromRoot<ObjectEventsContainer>().SubscribeToEvent(ObjectEvents.BeforeObjectDeath, OnDeathStart);

        if (UpdateVelocity)
            m_physics2DState.Velocity.AddChangedListener(OnVelocityChanged);

        if (UpdateIsGrounded)
            m_physics2DState.IsGrounded.AddChangedListener(OnGroundedChanged);

        if (UpdateAttacking)
            GetComponentInRoot<ObjectAttackState>().IsAttacking.AddChangedListener(OnAttackingChanged, false);

        if (UpdateHealth)
            GetComponentInRoot<ObjectHealthState>().Health.AddChangedListener(OnGetHit, false);

        enabled = false;
        StartInternal();
    }

    protected virtual void StartInternal() {}

    private void OnDeathStart(string eventName, object data)
    {
        m_animator.SetTrigger(DieHitTriggerName);
    }

    private void OnGetHit(SimpleValueBase obj)
    {
        m_animator.SetTrigger(GetHitTriggerName);
    }

    private void OnAttackingChanged(SimpleValueBase value)
    {
        m_animator.SetTrigger(IsAttackingTriggerName);
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

    public virtual void OnDeath()
    {
        GetComponentFromRoot<ObjectEventsContainer>().CallEvent(ObjectEvents.OnObjectDeath, transform.parent.gameObject);
    }
}