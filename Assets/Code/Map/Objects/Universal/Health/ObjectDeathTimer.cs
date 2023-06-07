using System.Collections;
using UnityEngine;

public class ObjectDeathTimer : NestedComponent
{
    [SerializeField] private float m_secondsToDeath;

    private void Start()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(m_secondsToDeath);
        GetComponentFromRoot<ObjectEventsContainer>().CallEvent(ObjectEvents.BeforeObjectDeath, null);
    }
}