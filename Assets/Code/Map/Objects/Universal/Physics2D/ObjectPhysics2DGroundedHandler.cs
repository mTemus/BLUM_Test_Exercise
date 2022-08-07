using UnityEngine;

public class ObjectPhysics2DGroundedHandler : NestedComponent
{
    [SerializeField] private LayerMask m_groundLayerMask;

    private Collider2D m_Collider;
    private ObjectPhysics2DState m_physics2DState;

    private void Awake()
    {
        m_Collider = GetComponentFromRoot<Collider2D>();
        m_physics2DState = GetComponent<ObjectPhysics2DState>();
    }

    private void FixedUpdate()
    {
        var colliderBounds = m_Collider.bounds;
        var colliderRadius = colliderBounds.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        var groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        var colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius, m_groundLayerMask);

        m_physics2DState.IsGrounded.Value = false;

        if (colliders.Length <= 0)
            return;

        for (var i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] == m_Collider)
                continue;

            m_physics2DState.IsGrounded.Value = true;
            break;
        }
    }
}