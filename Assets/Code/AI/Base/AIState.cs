using UnityEngine;

public enum AIStateType
{
    Idle = 1,
    Patrol = 2,
    Attack = 3,
    Chase = 4,
    Wander = 5,
    Die = 6,
    IsHurt = 7,
    Count
}

public abstract class AIState
{
    public abstract class AIStatePackage
    {
        public ObjectAIController Controller;

    }

    public AIState(AIStatePackage package) { }

    public AIStateType AIStateType { get; protected set; }

    public abstract void Update(ObjectAIController controller);
    public abstract void OnStateSet();
    public abstract void OnStateChanged();
}