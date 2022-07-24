using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpController : NestedComponent
{
    public string JumpActionName;

    public float JumpSpeed;
    public float JumpSpeedTolerance;

    private Rigidbody2D m_rigidbody2D;


    void Awake()
    {
        m_rigidbody2D = GetComponentFromRoot<Rigidbody2D>();

        GetComponentInRoot<PlayerInput>().actions.FindAction(JumpActionName).performed += Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (Mathf.Abs(m_rigidbody2D.velocity.y) > JumpSpeedTolerance)
            return;

        m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, JumpSpeed);
    }
}