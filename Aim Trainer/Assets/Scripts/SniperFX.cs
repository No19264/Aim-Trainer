using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperFX : MonoBehaviour
{
    [SerializeField] GunBehaviour gb;

    public void DisplaySniperScreen() {
        gb.SniperAimEffects();
    }
}
