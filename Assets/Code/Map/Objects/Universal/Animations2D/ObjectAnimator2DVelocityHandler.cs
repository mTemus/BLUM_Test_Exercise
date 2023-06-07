using UnityEngine;

public class ObjectAnimator2DVelocityHandler : NestedComponent
{
    public const string VelocityXParameterName = "VelocityX";
    public const string VelocityYParameterName = "VelocityY";

    protected Animator m_animator;
    protected ObjectPhysics2DState m_physics2DState;
    
    private float m_velocityX;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_physics2DState = GetComponentInRoot<ObjectPhysics2DState>();

        m_physics2DState.Velocity.AddChangedListener(OnVelocityChanged);
    }

    protected virtual void OnVelocityChanged(SimpleValueBase value)
    {
        var velocity = value.GetValueAs<Vector2>();

        if (m_velocityX == velocity.x) 
            return;

        m_animator.SetFloat(VelocityXParameterName, Mathf.Abs(velocity.x));
        m_velocityX = velocity.x;
    }
}