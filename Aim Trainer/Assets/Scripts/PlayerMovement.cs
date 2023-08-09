using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerData pd;
    [SerializeField] GameObject groundCheck;
    [SerializeField] Transform orientation;
    [SerializeField] Animator cameraAnim;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] int maxJumpCount;

    Rigidbody rb;
    Vector3 moveDirection;
    float actualSpeed;
    float speedValue;
    int jumps;
    bool crouching;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

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
        if (HVelocity.magnitude > pd.playerSpeed) {
            HVelocity = HVelocity.normalized;
            rb.velocity = new Vector3(HVelocity.x * pd.playerSpeed, rb.velocity.y, HVelocity.z * pd.playerSpeed);
        }
        
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0) {
            rb.AddForce(Vector3.up * pd.jumpPower, ForceMode.Impulse);
            jumps -= 1;
        }

        // Force player down when descending (extra gravity) 
        // Clamp the players falling speed as well
        if (rb.velocity.y < 0.1f && !isGrounded)
        {
            if (rb.velocity.y < -20f) rb.velocity = new Vector3(rb.velocity.x, -20f, rb.velocity.z); 
            else rb.AddForce(Vector3.down * pd.descendingGravityForce);
        }

        // Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl)) crouching = true;
        if (Input.GetKeyUp(KeyCode.LeftControl)) crouching = false;
        cameraAnim.SetBool("Crouching", crouching);

        // Ground conditionals
        if (isGrounded) {
            rb.drag = pd.groundDrag;
            jumps = maxJumpCount;
        } else {
            rb.drag = pd.groundDrag / 2;
        }
    }

    void FixedUpdate() 
    {
        // Add force in the direction the player wants to move
        if (!crouching) rb.AddForce(moveDirection.normalized * pd.playerSpeed * 10f);
        else rb.AddForce(moveDirection.normalized * pd.playerSpeed / 2 * 10f);
    }
}
