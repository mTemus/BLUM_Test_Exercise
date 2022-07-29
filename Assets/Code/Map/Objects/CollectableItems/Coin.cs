using UnityEngine;
using Zenject;

public class Coin : CollectableItemBase
{
    public class Pool : MemoryPool<ICollectableRespawnSystem, Coin>
    {
        protected override void OnCreated(Coin item)
        {
            item.Type = CollectableType.Coin;
        }

        protected override void Reinitialize(ICollectableRespawnSystem manager, Coin item)
        {
            item.ConstructInternal(manager);
            item.ResetItem();
        }
    }

    private void ResetItem()
    {
        transform.position = Vector3.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public override void Collect(GameObject collector)
    {
        collector.GetComponentInChildren<ObjectCoinsState>().Coins.Value++;
        base.Collect(collector);
    }

}
