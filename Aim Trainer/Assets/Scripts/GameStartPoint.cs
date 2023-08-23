using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartPoint : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] float arenaZBound;
    [SerializeField] GameObject player;

    bool inRangeLastFrame = false;

    void Update()
    {
        // If at a specific depth in the arena, allow the use to start the game if they press T
        // inRangeLastFrame ensures that some functions are only fired once
        if (player.transform.position.z >= arenaZBound ) {
            if (gameManager.headerTextState != 3) gameManager.headerTextState = 2;
            
            inRangeLastFrame = true;
            if (Input.GetKeyDown(KeyCode.T) && !gameManager.IsPlaying) {
                gameManager.StartGame();
            }
        } else if (inRangeLastFrame) {
            gameManager.headerTextState = -1;
            inRangeLastFrame = false;
        }
    }
}
