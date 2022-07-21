using System;
using UnityEngine;

public class SimpleValue<T> : SimpleValueBase
{
    protected T m_value;

    public bool DebugLogValueOnChange;
    public string DebugLogValueName = "";

    public SimpleValue() : base(true)
    {
        Owner = null;
    }

    public SimpleValue(bool callingEventsEnabled, object owner = null) : base(callingEventsEnabled)
    {
        Owner = owner;
    }

    public SimpleValue(bool callingEventsEnabled, T startValue, object owner = null) : base(callingEventsEnabled)
    {
        m_value = startValue;
        Owner = owner;
    }

    #region Set

    public virtual T Value
    {
        get => m_value;

        set
        {
            if (!IsNewValueDifferent(value))
                return;

            if (DebugLogValueOnChange)
                Debug.Log($"{DebugLogValueName} new value: {value}");

            m_value = value;

            if (CallingEventsEnabled)
                CallValueChanged();
        }
    }

    public override void SetValueAsObject(object value) { Value = (T)value; }

    #endregion

    #region Get

    public override Type GetValueType() => typeof(T);
    public override object GetValueAsObject() => Value;

    #endregion

    protected bool IsNewValueDifferent(T newValue)
    {
        var oldValue = GetValueAsObject();

        if (newValue == null && oldValue == null)
            return false;

        if (newValue == null)
            return true;

        return !newValue.Equals(GetValueAsObject());
    }
}