public class ObjectAttackState : NestedComponent
{
    public int BaseAttackDamage = 1;

    public SimpleValue<bool> IsAttacking = new SimpleValue<bool>(true, false);
    public SimpleValue<int> BaseDamage = new SimpleValue<int>(true, 1);

    private void Awake()
    {
        if (BaseAttackDamage != 0)
            BaseDamage.Value = BaseAttackDamage;
    }
}
