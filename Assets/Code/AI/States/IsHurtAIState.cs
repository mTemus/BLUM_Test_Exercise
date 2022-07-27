using System.Collections;
using UnityEngine;

public class IsHurtAIState : AIState
{
    public class IsHurtAIStatePackage : AIStatePackage
    {
        public float StateTime;
    }

    private float m_stateTime;
    private float m_timeCounter;
    private ObjectEventsContainer m_eventsContainer;
    private Rigidbody2D m_rigidbody2D;

    public IsHurtAIState(AIStatePackage package) : base(package)
    {
        AIStateType = AIStateType.IsHurt;

        var concretePackage = package as IsHurtAIStatePackage;
        m_stateTime = concretePackage.StateTime;
        m_eventsContainer = package.Controller.GetComponentFromRoot<ObjectEventsContainer>();
        m_rigidbody2D = package.Controller.GetComponentFromRoot<Rigidbody2D>();
    }

    public override void Update(ObjectAIController controller)
    {
        if (m_timeCounter >= m_stateTime)
            m_eventsContainer.CallEvent(AIEvents.OnGoBackToPreviousStateRequest, null);
        else
            m_timeCounter += Time.deltaTime;
    }

    public override void OnStateSet()
    {
        m_rigidbody2D.velocity = Vector2.zero;
    }

    public override void OnStateChanged()
    {
        m_timeCounter = 0f;
    }
}
