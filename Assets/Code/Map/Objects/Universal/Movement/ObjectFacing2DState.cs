using UnityEngine;

public class ObjectFacing2DState : NestedComponent
{
    [SerializeField] 
    private bool m_isFacingRightDefault = true;

    public SimpleValue<bool> IsFacingRight = new SimpleValue<bool>(false, true);

    private void Awake()
    {
        IsFacingRight.Value = m_isFacingRightDefault;
        IsFacingRight.CallingEventsEnabled = true;
    }
}