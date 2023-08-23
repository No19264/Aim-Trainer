using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAfterTime : MonoBehaviour
{
    [SerializeField] float lifeTime;
    float time = 0f;
    
    // Update is called once per frame
    void Update()
    {
        // Destory after time is up
        if (time < lifeTime) time += Time.deltaTime;
        else Destroy(gameObject);
    }
}
