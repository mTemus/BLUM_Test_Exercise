using UnityEngine;
using Zenject;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private PlayerJumpDust m_jumpDustEffect;

    [Inject]
    public void SubscribeToEvents(IEventsManager eventsManager)
    {
        eventsManager.SubscribeToEvent(PlayerObjectEvents.OnJumpStart, OnPlayerJump);
        eventsManager.SubscribeToEvent(PlayerObjectEvents.OnJumpEnd, OnPlayerJump);
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
