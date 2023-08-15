using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [SerializeField] PlayerData pd;
    [SerializeField] TextMeshProUGUI[] weaponAccuracyText;
    [SerializeField] TextMeshProUGUI[] pistolText;
    [SerializeField] TextMeshProUGUI[] rifleText;
    [SerializeField] TextMeshProUGUI[] sniperText;

    public void LoadData()
    {
        switch (pd.roundData.weaponIndex) {
            case 0:
                weaponAccuracyText[0].text = "PISTOL";
                break;
            case 1:
                weaponAccuracyText[0].text = "RIFLE";
                break;
            case 2:
                weaponAccuracyText[0].text = "SNIPER";
                break;
            default:
                break;
        }
        weaponAccuracyText[1].text = pd.roundData.accuracy.HitPercent.ToString();
        weaponAccuracyText[2].text = pd.roundData.accuracy.HeadHitPercent.ToString();
        pistolText[0].text = pd.totalPistolAccuracy.HitPercent.ToString();
        pistolText[1].text = pd.totalPistolAccuracy.HeadHitPercent.ToString();
        rifleText[0].text = pd.totalRifleAccuracy.HitPercent.ToString();
        rifleText[1].text = pd.totalRifleAccuracy.HeadHitPercent.ToString();
        sniperText[0].text = pd.totalSniperAccuracy.HitPercent.ToString();
        sniperText[1].text = pd.totalSniperAccuracy.HeadHitPercent.ToString();
    }
}
