using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class HUDHeartsController : MonoBehaviour
{
    public HUDHeart HeartPrefab;
    public Transform HeartsTransform;
    
    private int m_currentHeartCounter;
    private int m_baseHearts;
    private List<HUDHeart> m_hearts;

    [Inject]
    private void Construct(ObjectHealthState playerHealthState)
    {
        m_hearts = new List<HUDHeart>();

        for (var i = 0; i < playerHealthState.BaseHealth; i++)
        {
            var heart = Instantiate(HeartPrefab, HeartsTransform);
            heart.InitializeAsNew(this);
            heart.InitializeAsBase();
            m_hearts.Add(heart);
        }

        m_baseHearts = playerHealthState.BaseHealth;
        m_currentHeartCounter = m_baseHearts - 1;
        playerHealthState.Health.AddChangedListener(OnHealthChanged, false);
    }

    private void OnHealthChanged(SimpleValueBase value)
    {
        var health = value.GetValueAs<int>();

        if (health > m_currentHeartCounter)
            AddHeart();
        else
            RemoveHeart();
    }

    private void RemoveHeart()
    {
        m_hearts[m_currentHeartCounter].LoseHeart();
        m_currentHeartCounter--;
    }

    private void AddHeart()
    {
        if (m_currentHeartCounter - 1 < m_baseHearts - 1)
        {
            m_hearts[m_currentHeartCounter].RestoreHeart();
        }
        else
        {
            var heart = m_hearts.FirstOrDefault(heart => heart.gameObject.activeSelf);

            if (heart == null)
            {
                heart = Instantiate(HeartPrefab, HeartsTransform);
                heart.InitializeAsNew(this);
                m_hearts.Add(heart);
            }
            else
            {
                heart.InitializeAsPooled();
            }
        }

        m_currentHeartCounter++;
    }

    public void OnHeartRemoved(HUDHeart heart)
    {
        if (!heart.IsBase)
            heart.gameObject.SetActive(false);
    }
}
