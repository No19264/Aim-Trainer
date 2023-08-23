using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] GunBehaviour gunBehaviour;
    [SerializeField] DoorBehaviour entryDoor;
    [SerializeField] SpawnManager spawner;
    [SerializeField] TextMeshProUGUI headerText;
    public Round roundData;
    public bool canStart = true;
    public int eliminations = 0;
    public bool playing;
    public bool selectedWeapon = false;
    public int headerTextState;

    float timer;
    int roundNumber;
    bool cancelled;

    void Start()
    {
        playing = false;
        timer = 0f;
        roundNumber = 0;
        headerTextState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Show timer when in game, and show different texts when not
        // These texts vary depending on what the player is doing
        if (cancelled) {
            playing = false;
            cancelled = false;
        }

        if (playing) {
            headerText.text = string.Format("{0:00}:{1:00}", TimeSpan.FromSeconds(timer).Minutes, TimeSpan.FromSeconds(timer).Seconds);
            
            // Tick down the time
            if (timer <= 0f) {
                StopGame();
            } else {
                timer -= Time.deltaTime;
            }

            // If T is pressed while playing, stop the game
            if (Input.GetKeyDown(KeyCode.T)) {
                CancelGame();
            }
        } else {
            switch (headerTextState) {
                case 0:
                    headerText.text = "SELECT A WEAPON TO ENTER THE ARENA";
                    break;
                case 1:
                    headerText.text = "PRESS F TO SWITCH TO WEAPON";
                    break;
                case 2:
                    headerText.text = "PRESS T TO START THE ROUND";
                    break;
                case 3:
                    headerText.text = "ROUND COMPLETE\nPRESS T TO BEGIN AGAIN";
                    break;
                default: 
                    headerText.text = "";
                    break;
            }
        }
    }

    // Refresh round data and set up arena
    public void StartGame()
    {
        if (!playing && canStart) {
            playing = true;
            timer = playerData.roundTime;
            playerData.RefreshRoundData(gunBehaviour.weaponIndex);
            spawner.StartSpawning();
            entryDoor.LockDoor();
        }
    }

    // Set down arena
    public void StopGame()
    {
        if (playing) {
            playing = false;
            timer = 0;
            roundNumber += 1;
            playerData.ApplyRoundData();
            spawner.StopSpawning();
            entryDoor.UnlockDoor();
            headerTextState = 3;
        }
    }

    // Cancel the game without saving data
    public void CancelGame()
    {
        if (playing) {
            cancelled = true;
            timer = 0;
            spawner.StopSpawning();
            entryDoor.UnlockDoor();
            headerTextState = 3;
        }
    }

    // Used by other objects to change text state
    public void ToggleInteractText(bool on)
    {
        if (on) {
            headerTextState = 1;
        } else {
            if (selectedWeapon) headerTextState = -1;
            else headerTextState= 0;
        }
    }
    
    public bool IsPlaying {
        get { return playing; }
    }
}
