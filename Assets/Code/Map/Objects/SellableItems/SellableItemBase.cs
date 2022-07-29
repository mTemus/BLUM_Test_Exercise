using UnityEngine;

public abstract class SellableItemBase : MonoBehaviour
{
    public int Price;
    public CollectableItemBase.CollectableType CollectableType;
    public abstract bool BuyItem(ObjectCoinsState coinsState);
}