using UnityEngine;

public abstract class AIStateConstructorMono : MonoBehaviour
{
    public bool IsStartingState;

    public abstract AIState Construct(ObjectGenericAIController aiController);
    public abstract void Clear();
}