using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public interface ICollectableRespawnSystem
{
    public void OnItemCollected(CollectableItemBase item);
}

public class CollectableRespawnSystem : MonoBehaviour, ICollectableRespawnSystem
{
    private Coin.Pool m_coinsPool;

    public Dictionary<CollectableItemBase.CollectableType, IMemoryPool> CollectablePools; 

    [Inject]
    private void Construct(Coin.Pool coinPool, IEventsManager eventsManager)
    {
        m_coinsPool = coinPool;

        CollectablePools = new Dictionary<CollectableItemBase.CollectableType, IMemoryPool>
        {
            { CollectableItemBase.CollectableType.Coin, m_coinsPool }
        };


        eventsManager.SubscribeToEvent(WorldEvents.OnCollectableItemSpawnRequest, SpawnCollectable);
    }

    private void SpawnCollectable(string eventName, object data)
    {
        var package = (CollectableItemBase.CollectableSpawnPackage)data;

        CollectableItemBase item = null;

        switch (package.Type)
        {
            case CollectableItemBase.CollectableType.Coin:
                item = m_coinsPool.Spawn(this);
                break;

            case CollectableItemBase.CollectableType.Health:
                break;
        }

        item.gameObject.transform.position = package.SpawnPoint + Vector3.back;
        item.gameObject.SetActive(true);
    }

    public void OnItemCollected(CollectableItemBase item)
    {
        var pool = CollectablePools.FirstOrDefault(availablePool => availablePool.Key == item.Type).Value;

        if (pool == null)
        {
            Debug.LogError($"Can't get pool of respawnable item type: {item.Type}!");
            return;
        }

        item.gameObject.SetActive(false);
        pool.Despawn(item);
    }
}