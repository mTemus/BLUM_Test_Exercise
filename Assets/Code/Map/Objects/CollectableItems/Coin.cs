using UnityEngine;
using Zenject;

public class Coin : CollectableItemBase
{
    public class Pool : MemoryPool<ICollectableRespawnSystem, Coin>
    {
        protected override void Reinitialize(ICollectableRespawnSystem manager, Coin item)
        {
            item.ConstructInternal(manager);
            item.ResetCoin();
        }
    }

    private Rigidbody2D m_rigidbody2D;
    private PlayerCoinsState m_coinState;

    [Inject]
    private void Construct(PlayerCoinsState state)
    {
        m_coinState = state;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void ResetCoin()
    {
        transform.position = Vector3.zero;
        m_rigidbody2D.velocity = Vector2.zero;
    }

    public override void Collect()
    {
        base.Collect();
        m_coinState.Coins.Value++;
    }

}
