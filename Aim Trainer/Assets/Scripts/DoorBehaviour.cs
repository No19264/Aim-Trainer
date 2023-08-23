using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] GunBehaviour gunBehaviour;
    [SerializeField] GameObject target;
    [SerializeField] float detectRange;
    [SerializeField] bool requiresWeapon;
    Animator animator;
    bool lastStateOpen;
    bool locked;

    // Initiailise Variables
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Open the door if the player is close enough and the door isn't locked
        // lastStateOpen ensures that animation only fires once
        if (!locked) {
            if (Vector3.Distance(transform.position, target.transform.position) < detectRange) {
                if ((requiresWeapon && gunBehaviour.weaponIndex != 999) || !requiresWeapon) {
                    if (!lastStateOpen) {
                        animator.SetTrigger("Open");
                        lastStateOpen = true;
                    }
                }
            } else {
                if (lastStateOpen) {
                    animator.SetTrigger("Close");
                    lastStateOpen = false;
                }
            }
        }
    }

    // Lock the door (for other gameObjects to use)
    public void LockDoor()
    {
        animator.SetTrigger("Close");
        locked = true;
    }

    // Unlock the door (for other gameObjects to use)
    public void UnlockDoor()
    {
        locked = false;
    }
}
