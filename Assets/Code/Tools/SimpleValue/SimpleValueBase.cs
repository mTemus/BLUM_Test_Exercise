using System;
using System.Collections.Generic;

public abstract class SimpleValueBase
{
    public object Owner = null;

    public bool CallingEventsEnabled { get; set; }
    
    private bool m_listenerRemovedDuringEvent = false;
    private int m_isCallingCounter = 0;

    private List<Action<SimpleValueBase>> m_onValueChangedEvents = new List<Action<SimpleValueBase>>();

    public SimpleValueBase(bool callingEventsEnabled)
    {
        CallingEventsEnabled = callingEventsEnabled;
    }

    #region Get

    public abstract Type GetValueType();
    public abstract object GetValueAsObject();

    public T GetValueAs<T>() => (T)GetValueAsObject();

    #endregion

    #region Set

    public abstract void SetValueAsObject(object value);

    #endregion

    #region Events

    protected void CallValueChanged()
    {
        m_isCallingCounter++;

        for (var i = 0; i < m_onValueChangedEvents.Count; ++i)
            if (m_onValueChangedEvents[i] != null)
                m_onValueChangedEvents[i].Invoke(this);
        
        m_isCallingCounter--;

        if (m_isCallingCounter > 0) 
            return;

        if (!m_listenerRemovedDuringEvent) 
            return;
        
        m_listenerRemovedDuringEvent = false;

        for (var i = m_onValueChangedEvents.Count - 1; i >= 0; i--)
        {
            if (m_onValueChangedEvents[i] != null)
                continue;

            m_onValueChangedEvents.RemoveAt(i);
        }
    }

    public void CallValueChangedManually()
    {
        CallValueChanged();
    }

    public void AddChangedListener(Action<SimpleValueBase> listener, bool callNow = true)
    {
        m_onValueChangedEvents.Add(listener);

        if (CallingEventsEnabled && callNow)
            listener.Invoke(this);
    }

    public void AddChangedListenerIfNotAdded(Action<SimpleValueBase> listener, bool callNow = true)
    {
        if (m_onValueChangedEvents.Contains(listener))
            return;

        m_onValueChangedEvents.Add(listener);

        if (CallingEventsEnabled && callNow)
            listener.Invoke(this);
    }

    public void RemoveChangedListener(Action<SimpleValueBase> listener)
    {
        for (var i = 0; i < m_onValueChangedEvents.Count; ++i)
        {
            if (m_onValueChangedEvents[i] != listener) 
                continue;

            if (m_isCallingCounter > 0)
            {
                m_onValueChangedEvents[i] = null;
                m_listenerRemovedDuringEvent = true;
            }
            else
            {
                m_onValueChangedEvents.RemoveAt(i);
            }

            return;
        }
    }

    #endregion
}
