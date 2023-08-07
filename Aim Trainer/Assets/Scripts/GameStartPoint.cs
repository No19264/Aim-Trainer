using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartPoint : MonoBehaviour
{
    [SerializeField] GameManager gm;
    
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.T) && !gm.IsPlaying) {
            gm.StartGame();
        }
    }
}
