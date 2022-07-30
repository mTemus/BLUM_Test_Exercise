public class AttackAIStateConstructorMono : AIStateConstructorMono
{
    public float AttackCooldown;

    public override AIState Construct(ObjectGenericAIController aiController)
    {
        var package = new AttackAIState.AttackAIStatePackage
        {
            Controller = aiController,
            AttackCooldown = AttackCooldown
        };

        return new AttackAIState(package);
    }

    public override void Clear()
    {
        Destroy(GetComponent<AttackAIStateConstructorMono>());
    }
}