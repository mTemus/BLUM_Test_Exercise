using UnityEngine;

public class IdleAIState : AIState
{
    public class IdleAIStatePackage : AIStatePackage
    {
        public Vector3 IdlePosition;
        public bool ReturnToPosition;
    }

    public IdleAIState(AIStatePackage package) : base(package)
    {
        AIStateType = AIStateType.Idle;

        var concretePackage = package as IdleAIStatePackage;
        m_idlePosition = concretePackage.IdlePosition;
        m_movement = package.Controller.GetComponentInRoot<ObjectMovement2DController>();
    }

    private ObjectMovement2DController m_movement;

    private Vector3 m_idlePosition;
    private bool m_isOnPosition;
    private bool m_returnToPosition;

    public override void Update(ObjectGenericAIController controller)
    {
        if (!m_returnToPosition)
            return;
        
        if (m_isOnPosition)
            return;

        if (m_movement.MoveOnXTo())
            m_isOnPosition = true;
    }

    public override void OnStateSet()
    {
        if (!m_returnToPosition)
            return;

        m_movement.PrepareToMoveOnXTo(m_idlePosition.x);
    }

    public override void OnStateChanged()
    {
        if (!m_returnToPosition)
            return;

        m_isOnPosition = false;
        m_movement.PrepareToMoveOnXTo(m_idlePosition.x);
    }
}