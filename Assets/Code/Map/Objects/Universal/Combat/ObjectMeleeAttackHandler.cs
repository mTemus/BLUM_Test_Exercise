using UnityEngine;

public class ObjectMeleeAttackHandler : ObjectAttackHandler
{
    [SerializeField]
    private Vector2 m_attackPointSize;

    protected override void OnAttackInternal()
    {
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