using UnityEditor;
using UnityEngine;

public class MissileAttackHandler : ObjectAttackHandler
{
    [SerializeField] private float m_radius;

    protected override void OnAttackInternal()
    {
        var enemiesHit = Physics2D.OverlapCircleAll(m_attackPoint.position, m_radius, m_enemiesLayerMask);

        foreach (var enemy in enemiesHit)
            enemy.GetComponentInChildren<ObjectHealthHandler>().GetHurt(m_attackState.BaseDamage.Value);

        m_attackState.IsAttacking.Value = false;
        GetComponentFromRoot<ObjectEventsContainer>().CallEvent(ObjectEvents.OnObjectDeath, transform.parent.gameObject);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(m_attackPoint.transform.position, Vector3.forward, m_radius);
    }

#endif
}
