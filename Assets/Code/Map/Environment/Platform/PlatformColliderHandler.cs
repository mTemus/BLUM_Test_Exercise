using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColliderHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ObjectEventsContainer>().SubscribeToEvent(PlatformEvents.OnPlayerFallThrough, OnPlayerFallThrough);
    }

    private void OnPlayerFallThrough(string eventName, object data)
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(ActivatePlatform());
    }

    private IEnumerator ActivatePlatform()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Collider2D>().enabled = true;
    }
}
