using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

public class HUDHeartsController : MonoBehaviour
{
    public HUDHeart HeartPrefab;
    public Transform HeartsTransform;
    public TextMeshProUGUI AdditionalHeartsCounter;
    public int MaxVisibleHearts;

    private int m_currentHeartCounter;
    private List<HUDHeart> m_hearts;

    [Inject]
    private void Construct(ObjectHealthState playerHealthState)
    {
        m_hearts = new List<HUDHeart>();

        for (var i = 0; i < playerHealthState.BaseHealth; i++)
            AddHeart();

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
        if (m_currentHeartCounter < MaxVisibleHearts - 1)
        {
            m_hearts[m_currentHeartCounter].Lose();
        }
        else
        {
            var additionalHearts = m_currentHeartCounter - MaxVisibleHearts - 1;

        }
        m_currentHeartCounter--;
    }

    private void AddHeart()
    {
        if (m_currentHeartCounter < MaxVisibleHearts)
        {
            var heart = m_hearts.FirstOrDefault(heart => !heart.gameObject.activeSelf);

            if (heart == null)
            {
                heart = Instantiate(HeartPrefab, HeartsTransform);
                heart.InitializeAsNew(this);
                m_hearts.Add(heart);
            }
            else
            {
                heart.InitializeAsPooled();
                heart.gameObject.SetActive(true);
            }
        }
        else
        {
            var additionalHearts = m_currentHeartCounter - MaxVisibleHearts;

            if (additionalHearts == 0)
            {
                AdditionalHeartsCounter.transform.SetSiblingIndex(MaxVisibleHearts);
                AdditionalHeartsCounter.gameObject.SetActive(true);
            }

            AdditionalHeartsCounter.text = $"+{++additionalHearts}";
        }

        m_currentHeartCounter++;
    }

    public void OnHeartRemoved(HUDHeart heart)
    {
        heart.gameObject.SetActive(false);
    }
}
