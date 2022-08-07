using System.Collections.Generic;
using UnityEngine;

public class ObjectFacing2DHandler : NestedComponent
{
    public List<Transform> PartsToTurn;

    private void Start()
    {
        if (PartsToTurn.Count != 0)
            GetComponent<ObjectFacing2DState>().IsFacingRight.AddChangedListener(OnFacingSideChanged, false);
    }

    private void OnFacingSideChanged(SimpleValueBase value)
    {
        foreach (var part in PartsToTurn)
        {
            var partScale = part.localScale;
            part.localScale = new Vector3(partScale.x * -1, partScale.y, partScale.z);
        }
    }
}