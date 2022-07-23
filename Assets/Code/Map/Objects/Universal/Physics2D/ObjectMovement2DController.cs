using UnityEngine;

public class ObjectMovement2DController : NestedComponent
{
    public float MovementSpeed;

    private Rigidbody2D m_rigidbody2D;
    private int m_direction = 1;
    private bool m_isTurnedRight = true;

    private float m_goalPosX;
    private float m_goalPosXAbs;

    private void Awake()
    {
        m_rigidbody2D = GetComponentFromRoot<Rigidbody2D>();
    }

    public void Move()
    {
        var velocity = m_rigidbody2D.velocity;
        m_rigidbody2D.velocity = new Vector2(m_direction * MovementSpeed, velocity.y);
    }

    public void PrepareToMoveOnXTo(float xPos)
    {
        var positionX = m_rigidbody2D.transform.position.x;
        m_direction = xPos < positionX ? -1 : 1;
        m_goalPosX = xPos;
        m_goalPosXAbs = Mathf.Abs(m_goalPosX);

        TurnCharacter(m_direction == 1);
    }

    public bool MoveOnXTo(float speed = 0f)
    {
        var movementSpeed = speed == 0 ? MovementSpeed : speed;

        m_rigidbody2D.velocity = new Vector2(m_direction * movementSpeed, m_rigidbody2D.velocity.y);
        var position = m_rigidbody2D.transform.position;
        return Mathf.Abs(position.x) - m_goalPosXAbs > 0f;
    }

    private void TurnCharacter(bool isTurnedRight)
    {
        if (isTurnedRight == m_isTurnedRight)
            return;

        var scale = m_rigidbody2D.transform.localScale;
        m_rigidbody2D.transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
        m_isTurnedRight = isTurnedRight;
    }
}