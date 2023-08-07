using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] float roundLength;
    [SerializeField] DoorBehaviour entryDoor;
    [SerializeField] SpawnManager spawner;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject interactText;
    public bool canStart = true;
    
    bool playing = false;
    float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        // Changing the timer text
        if (playing) {
            timeText.text = string.Format("{0:00}:{1:00}", TimeSpan.FromSeconds(timer).Minutes, TimeSpan.FromSeconds(timer).Seconds);
        } else {
            timeText.text = "Press T while in Arena to Begin";
        }

        // Tick down the timer when playing
        if (playing) {
            if (timer <= 0f) {
                StopGame();
            } else {
                timer -= Time.deltaTime;
            }
        }
    }

    public void StartGame()
    {
        if (!playing && canStart) {
            Debug.Log("Round has started");
            playing = true;
            timer = roundLength;
            spawner.StartSpawning();
            entryDoor.LockDoor();
        }
        
    }

    public void StopGame()
    {
        if (playing) {
            Debug.Log("Round has ended");
            playing = false;
            timer = 0;
            spawner.StopSpawning();
            entryDoor.UnlockDoor();
        }
    }

    public void ToggleInteractText(bool on)
    {
        if (on) {
            interactText.SetActive(true);
        } else {
            interactText.SetActive(false);
        }
    }

    public bool IsPlaying {
        get { return playing; }
    }
}
