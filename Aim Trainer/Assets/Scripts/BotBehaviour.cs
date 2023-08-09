using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviour : MonoBehaviour
{
    [SerializeField] PlayerData pd;
    [Space]
    [SerializeField] float maxDistance;
    [SerializeField] float rotationSpeed;

    Rigidbody rb;
    Animator anim;
    float health;
    bool moving = true;
    int crouchState = 0;
    float distanceTravelled = 0;
    Vector3 posLastFrame;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        health = 100f;
        crouchState = Random.Range(0, 4);
        anim.SetBool("Crouched", (crouchState == 0) ? true : false);
        anim.SetBool("Moving", moving);
        posLastFrame = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Make the bot move slower if crouching (crouching when crouchstate == 0)
        float speedMultiplier = (crouchState == 0) ? 1f : 0f;
        rb.velocity = transform.forward * (pd.botSpeed - (pd.botSpeed * 0.3f * speedMultiplier));

        // Measure the distance the bot has walked. Destory bot once travelled that distance
        distanceTravelled += Mathf.Abs((transform.position - posLastFrame).magnitude);
        if (distanceTravelled >= maxDistance) { Destroy(gameObject); }
        posLastFrame = transform.position;

        // Rotate to face direction bot is moving -- KEEP FOR POSSIBLE FUTURE USE
        /*
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.y);
        if (moving && flatVelocity.magnitude != 0) {
            Quaternion targetRotation = Quaternion.LookRotation(flatVelocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        */
    }

    public void DamageBot(float damage) 
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}
