using UnityEngine;

public class SellableItemHealthPotion : SellableItemBase
{
    public override bool BuyItem(ObjectCoinsState coinsState)
    {
        if (coinsState.Coins.Value - Price < 0) 
            return false;
        
        coinsState.Coins.Value -= Price;
        return true;
    }
}
