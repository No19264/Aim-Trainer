using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string name;
    public float damage;
    public int clipSize;
    public float rpm;
    public float recoil;
    public float range;
    public GameObject bullet;
    [Space]
    public int currentAmmo;
}

[System.Serializable]
public class Accuracy
{
    public int shotCount;
    public int hitCount;
    public int headHitCount;

    public float HitPercent {
        get {
            return shotCount > 0 ? Mathf.Round(((float)hitCount / (float)shotCount) * 1000f) / 10f : 0f;
        }
    }
    
    public float HeadHitPercent {
        get {
            return shotCount > 0 ? Mathf.Round(((float)headHitCount / (float)hitCount) * 1000f) / 10f : 0f;
        }
    }
}

// A Data struct for round data (makes it easier to create achievements)
[System.Serializable]
public class Round
{
    public int weaponIndex;
    public Accuracy accuracy;

    public int spawnRange;
    public int eliminations;
    public float botSpeed;
    public float roundTime;

    public Round(int _weaponIndex, int _spawnRange, float _botSpeed, float _roundTime) {
        weaponIndex = _weaponIndex;
        spawnRange = _spawnRange;
        botSpeed = _botSpeed;
        roundTime = _roundTime;
        accuracy = new Accuracy();
        accuracy.shotCount = 0;
        accuracy.hitCount = 0;
        accuracy.headHitCount = 0;
        eliminations = 0;
    }
}
