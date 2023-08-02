using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    [SerializeField] GameObject target;
    [SerializeField] float detectRange;
    Animator animator;
    bool lastStateOpen;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < detectRange) {
            if (!lastStateOpen) {
                animator.SetTrigger("Open");
                lastStateOpen = true;
            }
        } else {
            if (lastStateOpen) {
                animator.SetTrigger("Close");
                lastStateOpen = false;
            }
        }
    }
}
