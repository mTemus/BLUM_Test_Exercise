using Zenject;

public class SystemsMonoInstaller : MonoInstaller
{
    public CollectableRespawnSystem CollectableRespawnSystem;

    public override void InstallBindings()
    {
        Container.Bind<ICollectableRespawnSystem>()
            .To<CollectableRespawnSystem>()
            .FromInstance(CollectableRespawnSystem)
            .AsSingle();
    }
}