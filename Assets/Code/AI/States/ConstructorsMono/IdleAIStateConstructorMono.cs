using UnityEngine;

public class IdleAIStateConstructorMono : AIStateConstructorMono
{
    public Vector3 IdlePosition;
    public bool ReturnToPosition = true;

    public override AIState Construct(ObjectGenericAIController aiController)
    {
        var package = new IdleAIState.IdleAIStatePackage
        {
            Controller = aiController,
            IdlePosition = IdlePosition,
            ReturnToPosition = ReturnToPosition
        };

        return new IdleAIState(package);
    }

    public override void Clear()
    {
        Destroy(GetComponent<IdleAIStateConstructorMono>());
    }
}