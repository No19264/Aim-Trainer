using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ButtonAnimation : MonoBehaviour
{
    Animator animator;
    
    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Select() {
        animator.SetTrigger("Select");
    }

    public void Deselect() {
        animator.SetTrigger("Deselect");
    }
}
