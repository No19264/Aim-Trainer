using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    // I am using a scriptable object to store data for my game across scenes and to keep it all in one place

    // Player Movement
    [Header("Player Movement")]
    public float playerSpeed;
    public float groundDrag;
    public float jumpPower;
    public float descendingGravityForce;

    // Camera
    [Header("Camera")]
    [Range(100f, 1000f)] public float horizontalSensitivity;
    [Range(100f, 1000f)] public float verticalSensitivity;

    // Gun
    [Header("Gun")]
    public Weapon[] weaponList;
    public float[] permAccuracy = new float[6];

    // Game
    [Header("Game")]
    public float botSpeed;
    public bool constantSpawnRange;
    [Range(0, 4)] public int spawnRange;
    public float roundTime;
    
    // Data
    [Header("Data")]
    public Accuracy totalPistolAccuracy;
    public Accuracy totalRifleAccuracy;
    public Accuracy totalSniperAccuracy;
    public Round roundData;

    // Achievements
    [Header("Medals")]
    public int[] medalData = new int[4];
    public Color[] medalColours = new Color[4];

    // Functions
    public void RefreshRoundData(int weaponIndex) {
        roundData = new Round(weaponIndex, spawnRange, botSpeed, roundTime);
    }

    public void ApplyRoundData() {
        // Save the data
        switch (roundData.weaponIndex) {
            case 0:
                totalPistolAccuracy.shotCount += roundData.accuracy.shotCount;
                totalPistolAccuracy.hitCount += roundData.accuracy.hitCount;
                totalPistolAccuracy.headHitCount += roundData.accuracy.headHitCount;
                break;
            case 1:
                totalRifleAccuracy.shotCount += roundData.accuracy.shotCount;
                totalRifleAccuracy.hitCount += roundData.accuracy.hitCount;
                totalRifleAccuracy.headHitCount += roundData.accuracy.headHitCount;
                break;
            case 2:
                totalSniperAccuracy.shotCount += roundData.accuracy.shotCount;
                totalSniperAccuracy.hitCount += roundData.accuracy.hitCount;
                totalSniperAccuracy.headHitCount += roundData.accuracy.headHitCount;
                break;
            default:
                Debug.Log("No stats were applied");
                break;
        }
        
        // Check for achievements (using a rolling switch method)
        if (roundData.eliminations >= 4) {
            // Medal 1
            Debug.Log(roundData.accuracy.HitPercent);
            switch (medalData[0]) {
                case 0:
                    if (roundData.accuracy.HitPercent >= 50f) {
                        medalData[0] = 1;
                        Debug.Log("     Medal 1: Bronze");
                        goto case 1;
                    }
                    break;
                case 1:
                    if (roundData.accuracy.HitPercent >= 75f) {
                        medalData[0] = 2;
                        Debug.Log("     Medal 1: Silver");
                        goto case 2;
                    }
                    break;
                case 2:
                    if (roundData.accuracy.HitPercent >= 90f) {
                        medalData[0] = 3;
                        Debug.Log("     Medal 1: Gold");
                    }
                    break;
                default:
                    break;
            }

            // Medal 2
            Debug.Log(roundData.accuracy.HeadHitPercent);
            switch (medalData[1]) {
                case 0:
                    if (roundData.accuracy.HeadHitPercent >= 30f) {
                        medalData[1] = 1;
                        Debug.Log("     Medal 2: Bronze");
                        goto case 1;
                    }
                    break;
                case 1:
                    if (roundData.accuracy.HeadHitPercent >= 50f) {
                        medalData[1] = 2;
                        Debug.Log("     Medal 2: Silver");
                        goto case 2;
                    }
                    break;
                case 2:
                    if (roundData.accuracy.HeadHitPercent >= 75f) {
                        medalData[1] = 3;
                        Debug.Log("     Medal 2: Gold");
                    }
                    break;
                default:
                    break;
            }

            // Medal 3
            Debug.Log(roundData.botSpeed);
            switch (medalData[2]) {
                case 0:
                    if (roundData.botSpeed >= 6f) {
                        medalData[2] = 1;
                        Debug.Log("     Medal 3: Bronze");
                        goto case 1;
                    }
                    break;
                case 1:
                    if (roundData.botSpeed >= 8f) {
                        medalData[2] = 2;
                        Debug.Log("     Medal 3: Silver");
                        goto case 2;
                    } 
                    break;
                case 2:
                    if (roundData.botSpeed >= 10f) {
                        medalData[2] = 3;
                        Debug.Log("     Medal 3: Gold");
                    }
                    break;
                default:
                    break;
            }

            // Medal 3
            Debug.Log(roundData.spawnRange);
            switch (medalData[3]) {
                case 0:
                    if (roundData.spawnRange >= 2) {
                        medalData[3] = 1;
                        Debug.Log("     Medal 4: Bronze");
                        goto case 1;
                    } 
                    break;
                case 1:
                    if (roundData.spawnRange >= 3) {
                        medalData[3] = 2;
                        Debug.Log("     Medal 4: Silver");
                        goto case 2;
                    }
                    break;
                case 2:
                    if (roundData.spawnRange >= 4) {
                        medalData[3] = 3;
                        Debug.Log("     Medal 4: Gold");
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public Color GetMedalColour(int medalIndex)
    {
        return medalColours[medalData[medalIndex]];
    }

    public string IndexToWeaponName(int i) 
    {
        string name = "<No Weapon>";
        switch (i) {
            case 0:
                name = "PISTOL";
                break;
            case 1:
                name = "RIFLE";
                break;
            case 2:
                name = "SNIPER";
                break;
            default:
                break;
        }
        return name;
    }
}