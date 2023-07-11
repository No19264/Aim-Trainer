using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float moveTime;
    [SerializeField] float rotationSpeed;

    Rigidbody rb;
    Animator anim;
    float health;
    bool moving = true;
    float direction = 1;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        // Temporary Movement
        if (time <= 0) {
            direction = -direction;
            time = moveTime;
        } else {
            time -= Time.deltaTime;
        }
        rb.velocity = new Vector3(speed * direction, rb.velocity.y, rb.velocity.z);

        // Rotate to face direction bot is moving
        Vector3 flatVelocity =  new Vector3(rb.velocity.x, 0f, rb.velocity.y);
        if (moving && flatVelocity.magnitude != 0) {
            Quaternion targetRotation = Quaternion.LookRotation(flatVelocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        // Animations
        anim.SetBool("Moving", true);
        anim.SetBool("Crouched", false);
    }

    public void DamageBot(float damage) 
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}
