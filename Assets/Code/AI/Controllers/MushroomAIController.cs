using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MushroomAIController : ObjectAIController
{
    public List<GameObject> PatrolPoints;
    public float PatrolSpeed;

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

        AIState patrol = new PatrolAIState(patrolPackage);

        m_aiStates = new List<AIState>
        {
            patrol, 
            new DieAIState(diePackage)
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