using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : NestedComponent
{
    public string MovementActionName;
    public float MovementSpeed;

    private InputAction m_movementAction;
    private Rigidbody2D m_rigidbody2D;
    private ObjectFacing2DState m_facingState;
    private bool m_isStandingStill = true;

    void Awake()
    {
        m_rigidbody2D = GetComponentFromRoot<Rigidbody2D>();
        m_facingState = GetComponentInRoot<ObjectFacing2DState>();
        var playerInput = GetComponentInRoot<PlayerInput>();

        m_movementAction = playerInput.actions.FindAction(MovementActionName);

        if (m_movementAction == null)
            Debug.LogError($"Can't find input action with name: {MovementActionName}!");
    }

    private void FixedUpdate()
    {
        var direction = m_movementAction.ReadValue<float>();
    
        // returning instead setting 0 as velocity continuously
        if (direction == 0f && m_isStandingStill)
            return;
    
        // applying first 0 to velocity
        m_isStandingStill = direction == 0f;
        m_rigidbody2D.velocity = new Vector2(direction * MovementSpeed, m_rigidbody2D.velocity.y);

        if (direction == 0f) 
            return;

        m_facingState.IsFacingRight.Value = direction > 0f;
    }
}
