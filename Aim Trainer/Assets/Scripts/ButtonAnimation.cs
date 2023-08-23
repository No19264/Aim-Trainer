using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ButtonAnimation : MonoBehaviour
{
    Animator animator;
    
    // Initialisation
    void Awake() {
        animator = GetComponent<Animator>();
    }

    // Animate button select
    public void Select() {
        animator.SetTrigger("Select");
    }

    // Animate button deselect
    public void Deselect() {
        animator.SetTrigger("Deselect");
    }
}
