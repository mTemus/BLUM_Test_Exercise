using UnityEngine;

public class PlayerPhysics2DHandler : NestedComponent
{
    [SerializeField] private LayerMask m_WhatIsGround;

    public SimpleValue<bool> IsGrounded = new SimpleValue<bool>(true, false);
    public SimpleValue<Vector2> Velocity = new SimpleValue<Vector2>(true, Vector2.zero);

    private Rigidbody2D m_Rigidbody;
    private BoxCollider2D m_Collider;

    private void Awake()
    {
        m_Collider = GetComponentFromRoot<BoxCollider2D>();
        m_Rigidbody = GetComponentFromRoot<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleVelocity();
        HandleGrounded();
    }

    private void HandleVelocity()
    {
        Velocity.Value = m_Rigidbody.velocity;
    }

    private void HandleGrounded()
    {
        var colliderBounds = m_Collider.bounds;
        var colliderRadius = m_Collider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        var groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        var colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        
        IsGrounded.Value = false;

        if (colliders.Length <= 0) 
            return;

        for (var i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] == m_Collider) 
                continue;

            IsGrounded.Value = true;
            break;
        }
    }
}
