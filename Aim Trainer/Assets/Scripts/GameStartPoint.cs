using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartPoint : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] float arenaZBound;
    [SerializeField] GameObject player;

    void Update()
    {
        if (player.transform.position.z >= arenaZBound && Input.GetKeyDown(KeyCode.T) && !gm.IsPlaying) {
            gm.StartGame();
        }
    }
}
