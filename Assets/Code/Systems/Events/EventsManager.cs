using System.Collections.Generic;

public delegate void EventManagerEventListener(string eventName, object data);

public interface IEventsManager
{
    public void SubscribeToEvent(string eventName, ObjectEventListener listener);
    public void UnsubscribeFromEvent(string eventName, ObjectEventListener listener);
    public void CallEvent(string eventName, object data);
}

public class EventsManager : IEventsManager
{
    private Dictionary<string, ObjectEventListener> m_eventListeners = new Dictionary<string, ObjectEventListener>();

    public void SubscribeToEvent(string eventName, ObjectEventListener listener)
    {
        if (m_eventListeners.ContainsKey(eventName))
            m_eventListeners[eventName] += listener;
        else
            m_eventListeners.Add(eventName, listener);
    }

    public void UnsubscribeFromEvent(string eventName, ObjectEventListener listener)
    {
        if (m_eventListeners.ContainsKey(eventName))
            m_eventListeners[eventName] -= listener;
    }

    public void CallEvent(string eventName, object data)
    {
        if (!m_eventListeners.TryGetValue(eventName, out var listener))
            return;

        listener?.Invoke(eventName, data);
    }
}
