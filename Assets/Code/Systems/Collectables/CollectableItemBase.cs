using UnityEngine;

public abstract class CollectableItemBase : MonoBehaviour
{
    public struct CollectableSpawnPackage
    {
        public Vector3 SpawnPoint;
        public CollectableType Type;
    }

    public enum CollectableType
    {
        Coin,
        Health
    }

    protected ICollectableRespawnSystem Manager;
    public CollectableType Type { get; protected set; }

    public void ConstructInternal(ICollectableRespawnSystem manager)
    {
        Manager = manager;
    }

    public virtual void Collect()
    {
        Manager.OnItemCollected(this);
    }
}
