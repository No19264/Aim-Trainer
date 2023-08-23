using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] TextMeshProUGUI[] weaponAccuracyText;
    [SerializeField] TextMeshProUGUI[] pistolText;
    [SerializeField] TextMeshProUGUI[] rifleText;
    [SerializeField] TextMeshProUGUI[] sniperText;
    [SerializeField] Image[] medalImages;

    // Fill text and medals values when called
    public void LoadData()
    {
        // Text
        weaponAccuracyText[0].text = playerData.IndexToWeaponName(playerData.roundData.weaponIndex);
        weaponAccuracyText[1].text = playerData.roundData.accuracy.HitPercent.ToString();
        weaponAccuracyText[2].text = playerData.roundData.accuracy.HeadHitPercent.ToString();
        pistolText[0].text = playerData.totalPistolAccuracy.HitPercent.ToString();
        pistolText[1].text = playerData.totalPistolAccuracy.HeadHitPercent.ToString();
        rifleText[0].text = playerData.totalRifleAccuracy.HitPercent.ToString();
        rifleText[1].text = playerData.totalRifleAccuracy.HeadHitPercent.ToString();
        sniperText[0].text = playerData.totalSniperAccuracy.HitPercent.ToString();
        sniperText[1].text = playerData.totalSniperAccuracy.HeadHitPercent.ToString();

        // Medals
        for (int i=0; i < medalImages.Length; i++) {
            medalImages[i].color = playerData.GetMedalColour(i);
        }
    }
}
