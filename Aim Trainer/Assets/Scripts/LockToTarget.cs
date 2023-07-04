using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToTarget : MonoBehaviour
{
    [SerializeField] GameObject target;
    void Update()
    {
        transform.position = target.transform.position;
    }
}
