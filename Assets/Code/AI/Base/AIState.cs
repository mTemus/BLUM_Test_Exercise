using UnityEngine;

public enum AIStateType
{
    Idle = 1,
    Patrol = 2,
    Attack = 3,
    Chasing = 4,
    Wandering = 5,
    Dying = 6,
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