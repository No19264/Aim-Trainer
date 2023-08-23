using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperFX : MonoBehaviour
{
    [SerializeField] GunBehaviour gunBehaviour;

    // Used in animation - Just triggers gunBehaviour function
    public void DisplaySniperScreen() {
        if (gunBehaviour.aiming) gunBehaviour.SniperAimEffects();
    }
}
