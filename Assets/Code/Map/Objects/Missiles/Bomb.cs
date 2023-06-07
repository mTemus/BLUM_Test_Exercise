using UnityEngine;
using Zenject;

public class Bomb : MonoBehaviour
{
    public class Pool : MemoryPool<Pool, Bomb>
    {
        protected override void Reinitialize(Pool pool, Bomb bomb)
        {
            bomb.m_pool = pool;
        }

        protected override void OnDespawned(Bomb bomb)
        {
            bomb.Clear();
        }

        protected override void OnSpawned(Bomb bomb)
        {
            bomb.Initialize();
        }
    }

    private Pool m_pool;

    private void Initialize()
    {
        GetComponent<ObjectEventsContainer>().SubscribeToEvent(ObjectEvents.OnObjectDeath, OnMissileHit);
    }

    private void Clear()
    {
        GetComponent<ObjectEventsContainer>().UnsubscribeFromEvent(ObjectEvents.OnObjectDeath, OnMissileHit);
        
        transform.position = Vector3.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnMissileHit(string eventName, object data)
    {
        m_pool.Despawn(this);
    }
}