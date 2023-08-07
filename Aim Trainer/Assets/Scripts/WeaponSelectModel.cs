using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectModel : MonoBehaviour
{   
    [SerializeField] WeaponSelect manager;
    [SerializeField] GameObject player;
    [SerializeField] int index;
    [SerializeField] float maxDistanceFromPlayer;
    bool inRange;

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= maxDistanceFromPlayer) inRange = true;
        else {
            inRange = false;
            manager.ShowInteractText(false);
        }
    }

    void OnMouseOver()
    {
        if (inRange) {
            manager.ShowInteractText(true);
            if (Input.GetKeyDown(KeyCode.F)) manager.SwitchToIndex(index);
        }
    }

    void OnMouseExit()
    {
        manager.ShowInteractText(false);
    }
}
