using UnityEngine;

public class ObjectPhysics2DVelocityHandler : NestedComponent
{
    private Rigidbody2D m_Rigidbody;
    private ObjectPhysics2DState m_physics2DState;

    private void Awake()
    {
        m_Rigidbody = GetComponentFromRoot<Rigidbody2D>();
        m_physics2DState = GetComponent<ObjectPhysics2DState>();
    }

    private void FixedUpdate()
    {
        m_physics2DState.Velocity.Value = m_Rigidbody.velocity;
    }
}