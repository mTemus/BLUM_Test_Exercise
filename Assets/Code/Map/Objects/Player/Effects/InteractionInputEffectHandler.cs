using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InteractionInputEffectHandler : MonoBehaviour
{
    public enum InputControllerType
    {
        Keyboard, Gamepad
    }

    [SerializeField]
    private float m_yOffset;

    private PlayerAccessInterface m_player;
    private InputControllerType m_controller;
    private Animator m_animator;

    [Inject]
    private void Construct(PlayerAccessInterface player)
    {
        m_player = player;
        InputSystem.onDeviceChange += OnDeviceChanged;
        enabled = false;
        m_animator = GetComponent<Animator>();
        m_controller = Gamepad.current != null ? InputControllerType.Gamepad : InputControllerType.Keyboard;
    }

    private void OnDeviceChanged(InputDevice device, InputDeviceChange changeType)
    {

        if (m_controller == InputControllerType.Keyboard)
        {
            if (device is Gamepad && changeType == InputDeviceChange.Added)
            {
                Debug.Log("Keyboard disabled");
                m_controller = InputControllerType.Gamepad;
            }
        }
        else
        {
            if (device is Gamepad && changeType == InputDeviceChange.Removed)
            {
                Debug.Log("Gamepad disabled");
                m_controller = InputControllerType.Keyboard;
            }
        }

        PlaySymbol();
    }

    private void Update()
    {
        var playerPos = m_player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y + m_yOffset, playerPos.z);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        enabled = true;
        PlaySymbol();
    }

    public void Hide()
    {
        enabled = false;
        gameObject.SetActive(false);
    }

    private void PlaySymbol()
    {
        if (!gameObject.activeSelf)
            return;

        m_animator.Play(m_controller == InputControllerType.Keyboard ? "Keyboard" : "Gamepad");
    }
}
