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
    bool inRangeLastFrame = false;

    void Update()
    {
        // This logic is used for updating the header text in the game manager
        if (Vector3.Distance(player.transform.position, transform.position) <= maxDistanceFromPlayer) {
            inRange = true;
            inRangeLastFrame = true;
        } else {
            inRange = false;
            if (inRangeLastFrame) manager.ShowInteractText(false);
            inRangeLastFrame = false;
        }
    }

    void OnMouseOver()
    {
        // When mouse is on the weapon and F is pressed, switch the current weapon
        if (inRange) {
            manager.ShowInteractText(true);
            if (Input.GetKeyDown(KeyCode.F)) manager.SwitchToIndex(index);
        }
    }

    void OnMouseExit()
    {
        // Update header text in game Manager
        manager.ShowInteractText(false);
    }
}
