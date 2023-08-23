using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecticleManager : MonoBehaviour
{
    [SerializeField] GameObject hitPrefab;
    [SerializeField] GameObject hitHeadPrefab;
    [SerializeField] Transform parent;

    // Spawn a recticle on the canvas when called
    public void CreateHitMarker(bool headshot = false)
    {
        if (!headshot) {
            Instantiate(hitPrefab, parent.transform.position, parent.transform.rotation, parent);
        } else {
            Instantiate(hitHeadPrefab, parent.transform.position, parent.transform.rotation, parent);
        }
    }
}
