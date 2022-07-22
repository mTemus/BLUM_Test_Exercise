using Zenject;

public class ManagersMonoInstaller : MonoInstaller<ManagersMonoInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IEventsManager>().To<EventsManager>().AsSingle();
    }
}