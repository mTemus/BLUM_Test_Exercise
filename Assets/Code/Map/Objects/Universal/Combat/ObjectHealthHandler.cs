using System.Collections;
using UnityEngine;

public class ObjectHealthHandler : NestedComponent
{
    public float HurtCooldownInSeconds;
    private ObjectHealthState m_healthState;

    private bool m_canBeHurt = true;

    private void Awake()
    {
        m_healthState = GetComponentInRoot<ObjectHealthState>();

        GetComponentFromRoot<ObjectEventsContainer>().SubscribeToEvent(ObjectEvents.OnObjectDeath, OnDeath);
    }

    public void GetHurt(int damage)
    {
        if (!m_canBeHurt)
            return;

        m_canBeHurt = false;

        var health = m_healthState.Health.Value;
        health -= damage;

        m_healthState.Health.Value = health;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            GetHurtInternal();
            StartCoroutine(CountHurtCooldown());
        }
    }

    protected virtual void GetHurtInternal() { }

    protected virtual void Die()
    {
        var eventsContainer = GetComponentFromRoot<ObjectEventsContainer>();
        eventsContainer.CallEvent(AIEvents.OnStateChangeRequest, AIStateType.Die);
    }

    private void OnDeath(string eventName, object data)
    {
        Destroy(transform.parent.gameObject);
    }

    private IEnumerator CountHurtCooldown()
    {
        yield return new WaitForSeconds(HurtCooldownInSeconds);
        m_canBeHurt = true;
    }
}