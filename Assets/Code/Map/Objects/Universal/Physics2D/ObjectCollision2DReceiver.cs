using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision2DReceiver : MonoBehaviour
{
    [SerializeField] 
    private bool m_callAdditionalEvents;

    [ShowWhen("m_callAdditionalEvents", true)]
    [SerializeField] 
    private List<string> OnCollisionEnterEventsName;

    [ShowWhen("m_callAdditionalEvents", true)]
    [SerializeField] 
    private List<string> OnCollisionExitEventsName;

    private ObjectEventsContainer m_eventsContainer;

    private void Awake()
    {
        m_eventsContainer = GetComponent<ObjectEventsContainer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_eventsContainer.CallEvent(ObjectEvents.OnCollision2DEnter, collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        m_eventsContainer.CallEvent(ObjectEvents.OnCollision2DExit, collision.gameObject);
    }
}
