using UnityEngine;

public abstract class ObjectAttackHandler : NestedComponent
{
    [SerializeField]
    protected Transform m_attackPoint;

    [SerializeField]
    protected LayerMask m_enemiesLayerMask;

    protected ObjectAttackState m_attackState;

    protected void Awake()
    {
        m_attackState = GetComponentInRoot<ObjectAttackState>();
        m_attackState.IsAttacking.AddChangedListener(OnAttack, false);
    }

    private void OnAttack(SimpleValueBase value)
    {
        var isAttacking = value.GetValueAs<bool>();

        if (!isAttacking)
            return;

        OnAttackInternal();
    }

    protected abstract void OnAttackInternal();
}