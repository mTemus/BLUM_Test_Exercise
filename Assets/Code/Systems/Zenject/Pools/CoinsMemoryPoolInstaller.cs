using UnityEngine;
using Zenject;

public class CoinsMemoryPoolInstaller : MonoInstaller
{
    public GameObject CoinPrefab;

    public override void InstallBindings()
    {
        Container.BindMemoryPool<Coin, Coin.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(CoinPrefab)
            .UnderTransformGroup("Coins");
    }
}