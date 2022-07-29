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
    private HealthPotion.Pool m_healthPotionPool;

    public Dictionary<CollectableItemBase.CollectableType, IMemoryPool> CollectablePools; 

    [Inject]
    private void Construct(Coin.Pool coinPool, HealthPotion.Pool healthPotionPool, IEventsManager eventsManager)
    {
        m_coinsPool = coinPool;
        m_healthPotionPool = healthPotionPool;

        CollectablePools = new Dictionary<CollectableItemBase.CollectableType, IMemoryPool>
        {
            { CollectableItemBase.CollectableType.Coin, m_coinsPool },
            { CollectableItemBase.CollectableType.HealthPotion, healthPotionPool }
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

            case CollectableItemBase.CollectableType.HealthPotion:
                item = m_healthPotionPool.Spawn(this);
                break;
        }

        item.gameObject.transform.position = package.SpawnPoint + Vector3.back;
        item.gameObject.SetActive(true);
        item.Drop(package.Direction);
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