using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperFX : MonoBehaviour
{
    [SerializeField] GunBehaviour gb;

    public void DisplaySniperScreen() {
        Debug.Log("Done");
        gb.SniperAimEffects();
    }
}
