using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToTarget : MonoBehaviour
{
    [SerializeField] GameObject target;
    void Update()
    {
        // Set position to target position
        transform.position = target.transform.position;
    }
}
