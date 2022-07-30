public class ChaseAIStateConstructorMono : AIStateConstructorMono
{
    public float ChasingSpeed;

    public override AIState Construct(ObjectGenericAIController aiController)
    {
        var package = new ChaseAIState.ChaseAIStatePackage
        {
            Controller = aiController,
            ChasingSpeed = ChasingSpeed,
            TriggerHandler = aiController.GetComponentInRoot<ObjectAITriggerAreaHandler>(),
        };

        return new ChaseAIState(package);
    }

    public override void Clear()
    {
        Destroy(GetComponent<ChaseAIStateConstructorMono>());
    }
}