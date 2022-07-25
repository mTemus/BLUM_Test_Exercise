using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : NestedComponent
{
    private PlayerInput m_playerInput;

    private int m_blockCounter;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
    }

    public void BlockInput()
    {
        m_blockCounter++;

        if (m_playerInput.currentActionMap.enabled)
            m_playerInput.currentActionMap.Disable();
    }

    public void BlockInputForTime(float time)
    {
        BlockInputForTimeInternal(time);
    }

    private void BlockInputForTimeInternal(float time = 0f)
    {
        m_blockCounter++;
        m_playerInput.currentActionMap.Disable();

        StartCoroutine(UnblockInputAfterTime(time));
    }

    public void UnblockInput()
    {
        m_blockCounter--;

        if (m_blockCounter == 0)
            m_playerInput.currentActionMap.Enable();
    }

    private IEnumerator UnblockInputAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        UnblockInput();
    }
}