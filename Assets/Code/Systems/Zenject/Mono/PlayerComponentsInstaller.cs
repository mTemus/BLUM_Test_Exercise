using Zenject;

public class PlayerComponentsInstaller : MonoInstaller
{
    public PlayerAccessInterface Player;
    public ObjectHealthState PlayerHealthState;
    public ObjectCoinsState PlayerCoinsState;

    public override void InstallBindings()
    {
        Container.Bind<ObjectHealthState>().FromInstance(PlayerHealthState);
        Container.Bind<ObjectCoinsState>().FromInstance(PlayerCoinsState);
        Container.Bind<PlayerAccessInterface>().FromInstance(Player);
    }
}