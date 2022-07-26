using UnityEngine;

public class HUDHeart : MonoBehaviour
{
    [HideInInspector] 
    public bool IsBase;
    private Animator m_animator;
    private HUDHeartsController m_controller;

    public void InitializeAsNew(HUDHeartsController controller)
    {
        m_controller = controller;
        m_animator = GetComponent<Animator>();
    }

    public void InitializeAsBase()
    {
        IsBase = true;
    }

    public void InitializeAsPooled()
    {
        m_animator.Rebind();
    }

    public void RestoreHeart()
    {
        m_animator.Play("GainHeart", 0);
    }

    public void LoseHeart()
    {
        m_animator.Play(IsBase ? "LoseHeartBase" : "LoseHeart", 0);
    }

    public void OnHeartLost()
    {
        m_controller.OnHeartRemoved(this);
    }
}
