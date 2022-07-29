using Zenject;

public class PlayerComponentsInstaller : MonoInstaller
{
    public ObjectHealthState PlayerHealthState;
    public ObjectCoinsState PlayerCoinsState;

    public override void InstallBindings()
    {
        Container.Bind<ObjectHealthState>().FromInstance(PlayerHealthState);
        Container.Bind<ObjectCoinsState>().FromInstance(PlayerCoinsState);
    }
}