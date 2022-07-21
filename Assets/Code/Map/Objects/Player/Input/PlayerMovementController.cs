using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : NestedComponent
{
    public string MovementActionName;
    public float MovementSpeed;

    private InputAction m_movementAction;
    private Rigidbody2D m_rigidbody2D;
    private bool m_isStandingStill = true;
    private bool m_isTurnedRight;

    private GameObject m_currentPlatform;

    // Start is called before the first frame update
    void Awake()
    {
        m_rigidbody2D = GetComponentFromRoot<Rigidbody2D>();
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

        if (direction != 0)
            TurnCharacter(direction < 1f);
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
