using TMPro;
using UnityEngine;
using Zenject;

public class HUDCoinsController : MonoBehaviour
{
    public TextMeshProUGUI CoinsCount;

    [Inject]
    private void Construct(ObjectCoinsState playerCoinsState)
    {
        playerCoinsState.Coins.AddChangedListener(OnCoinsCountChanged);
    }

    private void OnCoinsCountChanged(SimpleValueBase value)
    {
        var count = value.GetValueAs<int>();
        CoinsCount.text = count < 10 ? $"x0{count}" : $"x{count}";
    }
}
