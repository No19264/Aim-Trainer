using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] TrailRenderer tr;
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] float trailLength;
    float timer;

    void Start()
    {
        tr.time = trailLength;
        timer = lifeTime;
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Destory bullet when lifetime is complete
        if (timer > 0) timer -= Time.deltaTime;
        else Destroy(gameObject);
    }

    void OnCollisionEnter() {
        Destroy(gameObject);
    }
}
