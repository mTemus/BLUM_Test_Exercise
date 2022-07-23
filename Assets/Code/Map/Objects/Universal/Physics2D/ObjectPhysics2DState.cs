using UnityEngine;

public class ObjectPhysics2DState : NestedComponent
{
    public SimpleValue<bool> IsGrounded = new SimpleValue<bool>(true, false);
    public SimpleValue<Vector2> Velocity = new SimpleValue<Vector2>(true, Vector2.zero);
}