using UnityEngine;

public class PlayerKnockbackHandler : NestedComponent
{
    [SerializeField] private LayerMask m_enemyLayer;

    [SerializeField] private float m_knockbackDuration;
    [SerializeField] private float m_knockbackVelocityX;
    [SerializeField] private float m_knockbackVelocityY;

    private Rigidbody2D m_rigidbody2D;

    private float m_knockbackCounter;
    private bool m_isKnocbackFromRight;

    private void Awake()
    {
        GetComponentFromRoot<ObjectEventsContainer>().SubscribeToEvent(PlayerObjectEvents.OnCollisionEnter2D, OnCollisionWithEnemy);
        m_rigidbody2D = GetComponentFromRoot<Rigidbody2D>();
        enabled = false;
    }

    private void OnCollisionWithEnemy(string eventName, object data)
    {
        var enemyCollider = data as Collision2D;

        if (1 << enemyCollider.gameObject.layer != m_enemyLayer.value)
            return;

        if (enabled)
            return;
        
        var enemy = enemyCollider.gameObject;
        var enemyAttackState = enemy.GetComponentInChildren<ObjectAttackState>();

        GetComponentInRoot<PlayerHealthHandler>().GetHurt(enemyAttackState.BaseDamage.Value);
        GetComponentInRoot<PlayerInputController>().BlockInputForTime(m_knockbackDuration);
        
        m_isKnocbackFromRight = (m_rigidbody2D.transform.position.x - enemy.transform.position.x) < 0;
        enabled = true;
    }

    private void FixedUpdate()
    {
        if (m_knockbackCounter <= m_knockbackDuration)
        {
            m_rigidbody2D.velocity = m_isKnocbackFromRight ? 
                new Vector2(-m_knockbackVelocityX, m_knockbackVelocityY) : 
                new Vector2(m_knockbackVelocityX, m_knockbackVelocityY);
            
            m_knockbackCounter += Time.fixedDeltaTime;
        }
        else
        {
            m_knockbackCounter = 0;
            enabled = false;
        }
    }
}