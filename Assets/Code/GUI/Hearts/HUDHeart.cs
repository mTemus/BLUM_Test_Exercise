using UnityEngine;

public class HUDHeart : MonoBehaviour
{
    private Animator m_animator;
    private HUDHeartsController m_controller;

    public void InitializeAsNew(HUDHeartsController controller)
    {
        m_controller = controller;
        m_animator = GetComponent<Animator>();
    }

    public void InitializeAsPooled()
    {
        m_animator.Rebind();
        m_animator.Play("GainHeart", 0);
    }

    public void Lose()
    {
        m_animator.Play("LoseHeart", 0);
    }

    public void OnHeartLost()
    {
        m_controller.OnHeartRemoved(this);
    }
}
