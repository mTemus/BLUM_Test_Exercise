using UnityEngine;

public class DieAIState : AIState
{
    public class DieAIStatePackage : AIStatePackage { }

    private ObjectAIController m_aiController;

    public DieAIState(AIStatePackage package) : base(package)
    {
        AIStateType = AIStateType.Die;
        m_aiController = package.Controller;
    }

    public override void Update(ObjectAIController controller)
    {
        
    }

    public override void OnStateSet()
    {
        m_aiController.GetComponentFromRoot<ObjectEventsContainer>().CallEvent(ObjectEvents.BeforeObjectDeath, m_aiController.transform.parent.gameObject);
    }

    public override void OnStateChanged()
    {
        
    }
}