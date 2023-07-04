using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject groundCheck;
    [SerializeField] Transform orientation;
    [SerializeField] LayerMask groundLayer;
    [Space]
    [SerializeField] float speed;
    [SerializeField] float groundDrag;
    [SerializeField] float jumpPower;
    [SerializeField] int maxJumpCount;

    Vector3 moveDirection;
    float actualSpeed;
    float speedValue;
    int jumps;

    // Update is called once per frame
    void Update()
    {
        // Temporary Variables
        float HInput = Input.GetAxisRaw("Horizontal");
        float VInput = Input.GetAxisRaw("Vertical");
        bool isGrounded = Physics.CheckSphere(groundCheck.transform.position, 0.25f, groundLayer);

        // Get the direction the player wants to move
        moveDirection = orientation.forward * VInput + orientation.right * HInput;
        
        // Horizontal Movement Constraints
        Vector3 HVelocity = new Vector3(moveDirection.x, 0f, moveDirection.z);
        if (HVelocity.magnitude > speed) {
            HVelocity = HVelocity.normalized;
            rb.velocity = new Vector3(HVelocity.x * speed, rb.velocity.y, HVelocity.z * speed);
        }
        
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0) {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumps -= 1;
        }

        // Ground conditionals
        if (isGrounded) {
            rb.drag = groundDrag;
            jumps = maxJumpCount;
        } else {
            rb.drag = groundDrag / 2;
        }
    }

    void FixedUpdate() 
    {
        // Add force if player is moving
        rb.AddForce(moveDirection.normalized * speed * 10f);
    }
}
