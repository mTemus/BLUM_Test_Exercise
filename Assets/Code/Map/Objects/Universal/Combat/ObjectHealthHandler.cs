public class ObjectHealthHandler : NestedComponent
{
    private ObjectHealthState m_healthState;

    private void Awake()
    {
        m_healthState = GetComponentInRoot<ObjectHealthState>();

        GetComponentFromRoot<ObjectEventsContainer>().SubscribeToEvent(ObjectEvents.OnObjectDeath, OnDeath);
    }

    public void GetHurt(int damage)
    {
        var health = m_healthState.Health.Value;
        health -= damage;

        m_healthState.Health.Value = health;

        if (health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        var eventsContainer = GetComponentFromRoot<ObjectEventsContainer>();
        eventsContainer.CallEvent(AIEvents.OnStateChangeRequest, AIStateType.Die);
    }

    private void OnDeath(string eventName, object data)
    {
        Destroy(transform.parent.gameObject);
    }
}