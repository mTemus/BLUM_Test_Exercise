using UnityEngine;
using Zenject;

public class HealthPotionsMemoryPoolInstaller : MonoInstaller
{
    public GameObject PotionPrefab;

    public override void InstallBindings()
    {
        Container.BindMemoryPool<HealthPotion, HealthPotion.Pool>()
            .WithInitialSize(5)
            .FromComponentInNewPrefab(PotionPrefab)
            .UnderTransformGroup("Potions");
    }
}