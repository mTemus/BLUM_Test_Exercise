using UnityEngine;

public class PlayerCollision2DReceiver : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<ObjectEventsContainer>().CallEvent(PlayerObjectEvents.OnCollisionEnter2D, other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        GetComponent<ObjectEventsContainer>().CallEvent(PlayerObjectEvents.OnCollisionExit2D, other);
    }
}