using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : NestedComponent
{
    private PlayerInput m_playerInput;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
    }

    public void BlockInput()
    {
        BlockInputForTimeInternal();
    }

    public void BlockInputForTime(float time)
    {
        BlockInputForTimeInternal(time);
    }

    private void BlockInputForTimeInternal(float time = 0f)
    {
        m_playerInput.currentActionMap.Disable();

        if (time > 0)
            StartCoroutine(UnblockInputAfterTime(time));
    }

    public void UnblockInput()
    {
        m_playerInput.currentActionMap.Enable();
    }

    private IEnumerator UnblockInputAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        UnblockInput();
    }
}