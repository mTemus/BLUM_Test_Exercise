using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private PlayerJumpDust m_jumpDustEffect;

    private void Start()
    {
        EventsManager.Instance.SubscribeToEvent(PlayerObjectEvents.OnJumpStart, OnPlayerJump);
        EventsManager.Instance.SubscribeToEvent(PlayerObjectEvents.OnJumpEnd, OnPlayerJump);
    }

    private void OnPlayerJump(string eventName, object data)
    {
        var position = (Vector3)data;

        m_jumpDustEffect.transform.position = position;
        m_jumpDustEffect.gameObject.SetActive(true);

        if (eventName == PlayerObjectEvents.OnJumpStart)
            m_jumpDustEffect.PlayAnimation(PlayerJumpDust.AnimationName_DustStart);
        else if (eventName == PlayerObjectEvents.OnJumpEnd)
            m_jumpDustEffect.PlayAnimation(PlayerJumpDust.AnimationName_DustEnd);
    }
}
