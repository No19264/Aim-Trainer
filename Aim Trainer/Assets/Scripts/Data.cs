using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public float damage;
    public int clipSize;
    public float rpm;
    public float recoil;
    public float range;
    public GameObject bullet;
    [Space]
    public int currentAmmo;

    [HideInInspector] public int shotCount;
    [HideInInspector] public int hitCount;
    [HideInInspector] public int headHitCount;

    public float hitPercent {
        get {return Mathf.Round(((float)hitCount / (float)shotCount) * 1000f) / 10f;}
    }
    
    public float headHitPercent {
        get {return Mathf.Round(((float)headHitCount / (float)hitCount) * 1000f) / 10f;}
    }
}
