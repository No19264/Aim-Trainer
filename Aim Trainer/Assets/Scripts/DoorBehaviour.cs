using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] GunBehaviour gb;
    [SerializeField] GameObject target;
    [SerializeField] float detectRange;
    [SerializeField] bool requiresWeapon;
    Animator animator;
    bool lastStateOpen;
    bool locked;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!locked) {
            if (Vector3.Distance(transform.position, target.transform.position) < detectRange) {
                if ((requiresWeapon && gb.weaponIndex != 999) || !requiresWeapon) {
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

    public void LockDoor()
    {
        animator.SetTrigger("Close");
        locked = true;
    }

    public void UnlockDoor()
    {
        locked = false;
    }
}
