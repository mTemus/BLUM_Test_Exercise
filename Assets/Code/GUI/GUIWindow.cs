using UnityEngine;
using Zenject;

public abstract class GUIWindow : MonoBehaviour
{
    public enum GUIWindowType
    {
        TradeWindow,
    }

    public class WindowContext
    {
        public GUIWindowType WindowType;

    }

    [SerializeField] 
    private GUIWindowType m_windowType;

    [Inject] 
    private IEventsManager m_eventsManager;

    protected void Awake()
    {
        m_eventsManager.SubscribeToEvent(GUIEvents.OnWindowOpenRequest, Open);
        m_eventsManager.SubscribeToEvent(GUIEvents.OnWindowCloseRequest, Close);
        AwakeInternal();
    }

    private void Open(string eventName, object data)
    {
        var context = data as WindowContext;

        if (context.WindowType != m_windowType)
            return;

        OpenInternal(context);
    }

    private void Close(string eventName, object data)
    {
        var windowType = (GUIWindowType)data;

        if (windowType != m_windowType)
            return;

        CloseInternal();
    }

    protected abstract void AwakeInternal();
    protected abstract void OpenInternal(WindowContext context);
    protected abstract void CloseInternal();
}
