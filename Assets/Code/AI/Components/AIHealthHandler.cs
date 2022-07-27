public class AIHealthHandler : ObjectHealthHandler
{
    protected override void GetHurtInternal()
    {
        GetComponentFromRoot<ObjectEventsContainer>().CallEvent(AIEvents.OnStateChangeRequest, AIStateType.IsHurt);
    }
}