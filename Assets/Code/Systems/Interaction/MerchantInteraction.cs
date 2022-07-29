using UnityEngine;
using Zenject;

public class MerchantInteraction : NestedComponent, IInteractable
{
    [SerializeField]
    private Transform m_itemSpawnPoint;

    private SellableItemBase m_itemToSell;

    [Inject]
    private IEventsManager m_eventsManager;

    private void Awake()
    {
        m_itemToSell = GetComponent<SellableItemBase>();
    }

    public void OnSelect()
    {
        var context = new TradeWindowController.TradeWindowContext
        {
            ItemName = m_itemToSell.CollectableType.ToString(),
            ItemPrice = m_itemToSell.Price.ToString(),
            WindowType = GUIWindow.GUIWindowType.TradeWindow
        };

        m_eventsManager.CallEvent(GUIEvents.OnWindowOpenRequest, context);
    }

    public void OnDeselect()
    {
        m_eventsManager.CallEvent(GUIEvents.OnWindowCloseRequest, GUIWindow.GUIWindowType.TradeWindow);
    }

    public void Interact(GameObject interactionInitiator)
    {
        var coinState = interactionInitiator.GetComponentInChildren<ObjectCoinsState>();

        if (coinState == null)
        {
            Debug.LogError($"Can't get 'ObjectCoinState' component from {interactionInitiator.name}!");
            return;
        }

        if (!m_itemToSell.BuyItem(coinState)) 
            return;

        var directionVector = transform.parent.position - interactionInitiator.transform.position;


        var package = new CollectableItemBase.CollectableSpawnPackage
        {
            SpawnPoint = m_itemSpawnPoint.position,
            Type = m_itemToSell.CollectableType,
            Direction = directionVector.x < 0 ? 1 : -1,
        };

        m_eventsManager.CallEvent(WorldEvents.OnCollectableItemSpawnRequest, package);
    }
}
