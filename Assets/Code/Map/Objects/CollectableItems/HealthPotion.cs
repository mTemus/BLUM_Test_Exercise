using UnityEngine;
using Zenject;

public class HealthPotion : CollectableItemBase
{
    public class Pool : MemoryPool<ICollectableRespawnSystem, HealthPotion>
    {
        protected override void OnCreated(HealthPotion item)
        {
            item.Type = CollectableType.HealthPotion;
        }

        protected override void Reinitialize(ICollectableRespawnSystem manager, HealthPotion item)
        {
            item.ConstructInternal(manager);
            item.ResetItem();
        }
    }

    private void ResetItem()
    {
        enabled = false;
        transform.position = Vector3.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public override void Collect(GameObject collector)
    {
        collector.GetComponentInChildren<ObjectHealthState>().Health.Value++;
        base.Collect(collector);
    }
}