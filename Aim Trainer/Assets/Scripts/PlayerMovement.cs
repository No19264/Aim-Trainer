using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController cc;
    [SerializeField] GameObject groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float speed;
    [SerializeField] float accelerationTime;
    [SerializeField] float decelerationTime;
    [SerializeField] int maxJumpCount;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravity;

    Vector3 velocity;
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

        // 2D MOVEMENT
        // Move the Character Controller
        if (HInput != 0 && velocity.x < speed)
        {
            velocity.x += Time.deltaTime;
        }
        if (HInput == 0 && velocity.x > 0)
        {
            velocity.x += Time.deltaTime;
        }
        /*
        // Accelerate player if moving and not reached max speed yet
        if ((HInput != 0 || VInput != 0) && actualSpeed < speed)
        {
            actualSpeed = Lerp(0, speed, speedValue += (Time.deltaTime / accelerationTime));
        }
        // Decelerate player if there are no inputs and player hasn't decelerated yet
        if ((HInput == 0 && VInput == 0) && actualSpeed > 0.05f)
        {
            actualSpeed = Lerp(0, speed, speedValue += (Time.deltaTime / accelerationTime));
            if (actualSpeed != 0)
            {
                actualSpeed = 0;
            }
        }
        */

        Vector3 move = transform.right * HInput + transform.forward * VInput;
        cc.Move(move * speed * Time.deltaTime);


        // VERTICAL MOVEMENT
        // Implementing Gravity
        velocity.y -= gravity * Time.deltaTime;

        // Jump the player if they can do so
        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0)
        {
            velocity.y = jumpHeight;
            jumps -= 1;
        }

        // Enable jumping and reset gravity if grounded while falling
        if (isGrounded)
        {
            if (velocity.y < 0f)
            {
                velocity.y = 0f;
            }
            jumps = maxJumpCount;
        }
        cc.Move(velocity * Time.deltaTime);
    }
}
