using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ObjectItemDropHandler : NestedComponent
{
    [Serializable]
    public struct ItemToDrop
    {
        public CollectableItemBase.CollectableType Type;

        [Range(0, 100)]
        public float Chance;

        public int MinAmount;
        public int MaxAmount;
    }

    public List<ItemToDrop> Items;

    [Inject]
    private IEventsManager m_eventsManager;

    private void Awake()
    {
        GetComponentFromRoot<ObjectEventsContainer>().SubscribeToEvent(ObjectEvents.BeforeObjectDeath, DropItems);
    }

    private void DropItems(string eventName, object data)
    {
        foreach (var item in Items)
        {
            var chance = Random.Range(0f, 100f);

            if (!(chance <= item.Chance)) 
                continue;

            var amount = Random.Range(item.MinAmount, item.MaxAmount);

            for (var i = 0; i < amount; i++)
            {
                var package = new CollectableItemBase.CollectableSpawnPackage
                {
                    Type = item.Type,
                    SpawnPoint = transform.parent.position
                };

                m_eventsManager.CallEvent(WorldEvents.OnCollectableItemSpawnRequest, package);
            }
        }
    }
}