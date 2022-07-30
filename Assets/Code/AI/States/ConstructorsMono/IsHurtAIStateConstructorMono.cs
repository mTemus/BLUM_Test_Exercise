public class IsHurtAIStateConstructorMono : AIStateConstructorMono
{
    public float IsHurtDuration;

    public override AIState Construct(ObjectGenericAIController aiController)
    {
        var package = new IsHurtAIState.IsHurtAIStatePackage
        {
            Controller = aiController,
            StateTime = IsHurtDuration
        };

        return new IsHurtAIState(package);
    }

    public override void Clear()
    {
        Destroy(GetComponent<IsHurtAIStateConstructorMono>());
    }
}