using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackInputHandler : NestedComponent
{
    public string AttackActionName;
    public float AttackCooldownInSeconds;

    private ObjectAttackState m_attackState;
    private bool m_canAttack = true;

    void Awake()
    {
        GetComponentInRoot<PlayerInput>().actions.FindAction(AttackActionName).performed += Attack;

        m_attackState = GetComponentInRoot<ObjectAttackState>();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (!m_canAttack)
            return;

        m_attackState.IsAttacking.Value = true;
        m_canAttack = false;
        StartCoroutine(WaitForAttackCooldownEnd());
    }

    private IEnumerator WaitForAttackCooldownEnd()
    {
        yield return new WaitForSeconds(AttackCooldownInSeconds);
        m_canAttack = true;
    }

}