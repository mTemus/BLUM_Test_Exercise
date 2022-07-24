using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectMeleeAttackHandler : NestedComponent
{
    [SerializeField]
    private Transform m_attackPoint;
    
    [SerializeField]
    private Vector2 m_attackPointSize;

    [SerializeField] 
    private LayerMask m_enemiesLayerMask;

    private ObjectAttackState m_attackState;

    void Awake()
    {
        m_attackState = GetComponentInRoot<ObjectAttackState>();
        m_attackState.IsAttacking.AddChangedListener(OnAttack, false);
    }

    private void OnAttack(SimpleValueBase value)
    {
        var isAttacking = value.GetValueAs<bool>();

        if (!isAttacking)
            return;

        var enemiesHit = Physics2D.OverlapBoxAll(m_attackPoint.position, m_attackPointSize, 0, m_enemiesLayerMask);

        foreach (var enemy in enemiesHit)
            enemy.GetComponentInChildren<ObjectHealthHandler>().GetHurt(m_attackState.BaseDamage.Value);
        
        m_attackState.IsAttacking.Value = false;
    }

#if UNITY_EDITOR
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(m_attackPoint.position, m_attackPointSize);
    }

#endif

}