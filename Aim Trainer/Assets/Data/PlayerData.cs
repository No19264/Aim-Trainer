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

    public void RefreshRoundData(int weaponIndex) {
        roundData = new Round(weaponIndex, spawnRange, botSpeed, roundTime);
    }

    public void ApplyRoundData() {
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
    }
}