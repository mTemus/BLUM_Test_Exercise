public class ObjectCoinsState : NestedComponent
{
    public int StartCoins = 0;

    public SimpleValue<int> Coins = new SimpleValue<int>(true, 0);

    private void Awake()
    {
        Coins.Value = StartCoins;
    }

}
