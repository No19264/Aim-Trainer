using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    // I am using a scriptable object to store data for my game across scenes and to keep it all in one place

    // Player Movement
    public float playerSpeed;
    public float groundDrag;
    public float jumpPower;
    public float descendingGravityForce;

    // Camera
    [Range(100f, 1000f)] public float horizontalSensitivity;
    [Range(100f, 1000f)] public float verticalSensitivity;

    // Gun
    public Weapon[] weaponList;

    // Game
    public float botSpeed;
    public bool constantSpawnRange;
    [Range(0, 4)] public int spawnRange;
    public float roundTime;
}
