public class ObjectHealthState : NestedComponent
{
    public int BaseHealth = 1;

    public SimpleValue<int> Health = new SimpleValue<int>(true, 1);

    private void Awake()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        if (BaseHealth != 0)
            Health.Value = BaseHealth;
    }

}