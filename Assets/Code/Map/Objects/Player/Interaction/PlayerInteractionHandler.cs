using UnityEngine;

public class PlayerInteractionHandler : ObjectInteractionHandler
{
    public GameObject InteractionSymbol;

    protected override void OnSelectInternal()
    {
        InteractionSymbol.SetActive(true);
    }

    protected override void OnDeselectInternal()
    {
        InteractionSymbol.SetActive(false);
    }
}
