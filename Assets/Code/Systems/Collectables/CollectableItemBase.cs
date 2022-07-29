using System.Collections;
using UnityEngine;

public abstract class CollectableItemBase : MonoBehaviour
{
    public struct CollectableSpawnPackage
    {
        public Vector3 SpawnPoint;
        public CollectableType Type;
        public int Direction;
    }

    public enum CollectableType
    {
        Coin,
        HealthPotion
    }

    protected ICollectableRespawnSystem Manager;
    public CollectableType Type { get; protected set; }

    protected int m_direction;
    protected float m_forceTime = 0.5f;
    protected Vector2 m_velocity = new Vector2(1.5f, 1.5f);

    private Rigidbody2D m_rigidbody2D;

    public void ConstructInternal(ICollectableRespawnSystem manager)
    {
        Manager = manager;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public virtual void Collect(GameObject collector)
    {
        Manager.OnItemCollected(this);
    }

    public void Drop(int direction)
    {
        m_direction = direction;
        enabled = true;
        StartCoroutine(ApplyVelocity());
    }

    private void FixedUpdate()
    {
        m_rigidbody2D.velocity = new Vector2(m_velocity.x * m_direction, m_velocity.y);
    }

    private IEnumerator ApplyVelocity()
    {
        yield return new WaitForSeconds(m_forceTime);
        enabled = false;
    }
}
