using Zenject;

public class PlayerComponentsInstaller : MonoInstaller
{
    public ObjectHealthState PlayerHealthState;
    public PlayerCoinsState PlayerCoinsState;

    public override void InstallBindings()
    {
        Container.Bind<ObjectHealthState>().FromInstance(PlayerHealthState);
        Container.Bind<PlayerCoinsState>().FromInstance(PlayerCoinsState);
    }
}