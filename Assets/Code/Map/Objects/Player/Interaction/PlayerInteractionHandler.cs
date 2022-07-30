using UnityEngine;

public class PlayerInteractionHandler : ObjectInteractionHandler
{
    public InteractionInputEffectHandler InteractionSymbol;

    protected override void OnSelectInternal()
    {
        InteractionSymbol.Show();
    }

    protected override void OnDeselectInternal()
    {
        InteractionSymbol.Hide();
    }
}
