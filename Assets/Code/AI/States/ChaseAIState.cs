using UnityEngine;

public class ChaseAIState : AIState
{
    public class ChaseAIStatePackage : AIStatePackage
    {
        public ObjectAITriggerAreaHandler TriggerHandler;
        public float ChasingSpeed;
    }

    private readonly ObjectAITriggerAreaHandler m_triggerHandler;
    private GameObject m_target;
    private readonly float m_chasingSpeed;
    private readonly ObjectMovement2DController m_movement;

    public ChaseAIState(AIStatePackage package) : base(package)
    {
        AIStateType = AIStateType.Chase;

        var concretePackage = package as ChaseAIStatePackage;
        m_triggerHandler = concretePackage.TriggerHandler;
        m_chasingSpeed = concretePackage.ChasingSpeed;
        m_movement = concretePackage.Controller.GetComponentInRoot<ObjectMovement2DController>();
    }

    public override void Update(ObjectGenericAIController controller)
    {
        m_movement.PrepareToMoveOnXTo(m_target.transform.position.x);
        m_movement.MoveOnXTo(m_chasingSpeed);
    }

    public override void OnStateSet()
    {
        m_target = m_triggerHandler.Target;
    }

    public override void OnStateChanged()
    {
        m_target = null;
    }
}