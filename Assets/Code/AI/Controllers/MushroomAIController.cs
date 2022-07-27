using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MushroomAIController : ObjectAIController
{
    public List<GameObject> PatrolPoints;
    public float PatrolSpeed;
    public float IsHurtDuration;

    protected override void CreateObjectAI()
    {
        base.CreateObjectAI();

        var patrolPackage = new PatrolAIState.PatrolAIStatePackage
        {
            PatrolPoints = PatrolPoints.Select(point => point.transform.position).ToList(),
            PatrolSpeed = PatrolSpeed,
            Controller = this
        };

        var diePackage = new DieAIState.DieAIStatePackage
        {
            Controller = this
        };

        var isHurtPackage = new IsHurtAIState.IsHurtAIStatePackage
        { 
            Controller = this,
            StateTime = IsHurtDuration
        };

        AIState patrol = new PatrolAIState(patrolPackage);

        m_aiStates = new List<AIState>
        {
            patrol, 
            new DieAIState(diePackage),
            new IsHurtAIState(isHurtPackage)
        };

        m_currentAIState = patrol;
        m_currentAIState.OnStateSet();
    }

    private void Start()
    {
        CreateObjectAI();
    }

    private void FixedUpdate()
    {
        HandleState();
    }
}