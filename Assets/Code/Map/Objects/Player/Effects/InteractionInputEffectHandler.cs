using UnityEngine;
using Zenject;

public class InteractionInputEffectHandler : MonoBehaviour
{
    [SerializeField]
    private float m_yOffset;

    private PlayerAccessInterface m_player;

    [Inject]
    private void Construct(PlayerAccessInterface player)
    {
        m_player = player;
        enabled = false;
    }

    private void Awake()
    {
        enabled = false;
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
    }

    public void Hide()
    {
        enabled = false;
        gameObject.SetActive(false);
    }
}
