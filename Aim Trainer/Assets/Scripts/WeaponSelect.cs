using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GunBehaviour gunScript;
    [SerializeField] GameObject[] weaponModels;

    public void ShowInteractText(bool on)
    {
        // Toggles the "Interact" text on the screen
        if (on) gameManager.ToggleInteractText(true);
        else gameManager.ToggleInteractText(false);
    }

    public void SwitchToIndex(int index)
    {
        // Disables the selected weapon model
        foreach (GameObject model in weaponModels) model.SetActive(true);
        if (index < weaponModels.Length) weaponModels[index].SetActive(false);
        // Switches the weapon
        gunScript.SwitchToWeapon(index);
    }
}
