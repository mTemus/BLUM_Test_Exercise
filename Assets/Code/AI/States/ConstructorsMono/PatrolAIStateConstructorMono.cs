using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolAIStateConstructorMono : AIStateConstructorMono
{
    public bool Patrol = true;
    public List<GameObject> PatrolPoints;
    public float PatrolSpeed;

    public override AIState Construct(ObjectGenericAIController aiController)
    {
        var package = new PatrolAIState.PatrolAIStatePackage
        {
            PatrolPoints = PatrolPoints.Select(point => point.transform.position).ToList(),
            PatrolSpeed = PatrolSpeed,
            Patrol = Patrol,
            Controller = aiController,
        };

        return new PatrolAIState(package);
    }

    public override void Clear()
    {
        Destroy(GetComponent<PatrolAIStateConstructorMono>());
    }
}