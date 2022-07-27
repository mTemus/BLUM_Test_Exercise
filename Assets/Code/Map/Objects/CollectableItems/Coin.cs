using UnityEngine;
using Zenject;

public class Coin : CollectableItemBase
{
    public class Pool : MemoryPool<ICollectableRespawnSystem, Coin>
    {
        protected override void OnCreated(Coin item)
        {
            base.OnCreated(item);
            item.Construct();
        }

        protected override void Reinitialize(ICollectableRespawnSystem manager, Coin item)
        {
            item.ConstructInternal(manager);
            item.ResetCoin();
        }
    }

    private Rigidbody2D m_rigidbody2D;

    private void Construct()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void ResetCoin()
    {
        transform.position = Vector3.zero;
        m_rigidbody2D.velocity = Vector2.zero;
    }

    public override void Collect(GameObject collector)
    {
        collector.GetComponentInChildren<PlayerCoinsState>().Coins.Value++;
        base.Collect(collector);
    }

}
