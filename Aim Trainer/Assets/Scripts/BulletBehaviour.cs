using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] float trailLength;
    float timer;

    // Initialise variables
    void Start()
    {
        trailRenderer.time = trailLength;
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

    // Destory bullet on impact
    void OnColliderEnter(Collider other) 
    {
        if (other.tag != "BulletIgnore") Destroy(gameObject);
    }
}
