public class DieAIStateConstructorMono : AIStateConstructorMono
{
    public override AIState Construct(ObjectGenericAIController aiController)
    {
        var package = new DieAIState.DieAIStatePackage
        {
            Controller = aiController
        };

        return new DieAIState(package);
    }

    public override void Clear()
    {
        Destroy(GetComponent<DieAIStateConstructorMono>());
    }
}