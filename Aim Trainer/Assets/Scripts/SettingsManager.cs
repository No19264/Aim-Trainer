using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [Space]
    [SerializeField] Slider[] sliders;
    [SerializeField] TextMeshProUGUI[] values;
    [SerializeField] Toggle rangeToggle;
    [SerializeField] TextMeshProUGUI currentWeaponText;
 
    int currentWeaponIndex = 0;
    Weapon[] weaponStats;
    int maxIndex = 2;

    public void Enable()
    {
        weaponStats = playerData.weaponList;
        LoadValues(true);
    }

    // This function loads the current values into the sliders
    public void LoadValues(bool loadAll = false)
    {
        // There IS NOT a more efficient way to do this as each slider is accessing a different 
        // and independent variable from the player as well as value-slider conversions having different multipliers
        // (To make adjusting values with sliders easier). Mathf.Round eliminates floating point error during calculations
        if (loadAll) {
            sliders[0].value = Mathf.Round(playerData.playerSpeed * 10f);
            values[0].text = Mathf.Round(playerData.playerSpeed * 10f).ToString();
            sliders[1].value = Mathf.Round(playerData.groundDrag * 10f);
            values[1].text = Mathf.Round(playerData.groundDrag * 10f).ToString();
            sliders[2].value = Mathf.Round(playerData.jumpPower * 10f);
            values[2].text = Mathf.Round(playerData.jumpPower * 10f).ToString();
            sliders[3].value = Mathf.Round(playerData.descendingGravityForce * 10f);
            values[3].text = Mathf.Round(playerData.descendingGravityForce * 10f).ToString();
            sliders[4].value = Mathf.Round(playerData.horizontalSensitivity * 0.1f);
            values[4].text = Mathf.Round(playerData.horizontalSensitivity * 0.1f).ToString();
            sliders[5].value = Mathf.Round(playerData.verticalSensitivity * 0.1f);
            values[5].text = Mathf.Round(playerData.verticalSensitivity * 0.1f).ToString();
            sliders[6].value = Mathf.Round(playerData.botSpeed * 10f);
            values[6].text = Mathf.Round(playerData.botSpeed * 10f).ToString();
            sliders[7].value = (int) Mathf.Round(playerData.spawnRange);
            values[7].text = Mathf.Round(playerData.spawnRange).ToString();
            sliders[8].value = Mathf.Round(playerData.roundTime);
            values[8].text = Mathf.Round(playerData.roundTime).ToString();
        }
        sliders[9].value = Mathf.Round(weaponStats[currentWeaponIndex].damage);
        values[9].text = Mathf.Round(weaponStats[currentWeaponIndex].damage).ToString();
        sliders[10].value = Mathf.Round(weaponStats[currentWeaponIndex].clipSize);
        values[10].text = Mathf.Round(weaponStats[currentWeaponIndex].clipSize).ToString();
        sliders[11].value = Mathf.Round(weaponStats[currentWeaponIndex].recoil);
        values[11].text = Mathf.Round(weaponStats[currentWeaponIndex].recoil).ToString();
        currentWeaponText.text = weaponStats[currentWeaponIndex].name;
        rangeToggle.isOn = playerData.constantSpawnRange;
    }

    // Only called when slider value has changed
    public void ChangeValue(int index)
    {
        values[index].text = sliders[index].value.ToString();
    }

    // Stores all slider values into the playerData
    public void ApplyChanges()
    {
        playerData.playerSpeed = Mathf.Round(sliders[0].value * 0.1f);
        playerData.groundDrag = Mathf.Round(sliders[1].value * 0.1f);
        playerData.jumpPower = Mathf.Round(sliders[2].value * 0.1f);
        playerData.descendingGravityForce = Mathf.Round(sliders[3].value * 0.1f);
        playerData.horizontalSensitivity = Mathf.Round(sliders[4].value * 10f);
        playerData.verticalSensitivity = Mathf.Round(sliders[5].value * 10f);
        playerData.botSpeed = Mathf.Round(sliders[6].value * 0.1f);
        playerData.spawnRange = (int) Mathf.Round(sliders[7].value);
        playerData.roundTime = Mathf.Round(sliders[8].value);
        weaponStats[currentWeaponIndex].damage = Mathf.Round(sliders[9].value);
        weaponStats[currentWeaponIndex].clipSize = (int) Mathf.Round(sliders[10].value);
        weaponStats[currentWeaponIndex].recoil = Mathf.Round(sliders[11].value);
        playerData.weaponList = weaponStats;
        playerData.constantSpawnRange = rangeToggle.isOn;
    }

    // Switch the weapon in focus of the settings page
    public void WeaponScrollToLeft(bool left = true)
    {
        // Save the previous weapon settings in a temporary place and load the new data
        // for the new weapon
        weaponStats[currentWeaponIndex].damage = Mathf.Round(sliders[9].value);
        weaponStats[currentWeaponIndex].clipSize = (int) Mathf.Round(sliders[10].value);
        weaponStats[currentWeaponIndex].recoil = Mathf.Round(sliders[11].value);
        if (left) {
            currentWeaponIndex -= 1;
            if (currentWeaponIndex < 0) currentWeaponIndex = maxIndex;
        } else {
            currentWeaponIndex += 1;
            if (currentWeaponIndex > maxIndex) currentWeaponIndex = 0;
        }
        LoadValues();
    }
}
