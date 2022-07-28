using UnityEngine;

public class AttackAIState : AIState
{
    public class AttackAIStatePackage : AIStatePackage
    {
        public float AttackCooldown;
    }

    private readonly ObjectAttackState m_attackState;
    private readonly float m_attackCooldown;
    private float m_cooldownCounter;
     
    public AttackAIState(AIStatePackage package) : base(package)
    {
        AIStateType = AIStateType.Attack;

        var concretePackage = package as AttackAIStatePackage;
        m_attackCooldown = concretePackage.AttackCooldown;
        m_attackState = package.Controller.GetComponentInRoot<ObjectAttackState>();
    }

    public override void Update(ObjectAIController controller)
    {
        if (m_cooldownCounter <= m_attackCooldown)
        {
            m_cooldownCounter += Time.deltaTime;
        }
        else
        {
            m_attackState.IsAttacking.Value = true;
            m_cooldownCounter = 0f;
        }
    }

    public override void OnStateSet()
    {
        m_attackState.IsAttacking.Value = true;
        m_attackState.GetComponentFromRoot<Rigidbody2D>().velocity = Vector2.zero;
    }

    public override void OnStateChanged()
    {
        m_cooldownCounter = 0f;
    }
}