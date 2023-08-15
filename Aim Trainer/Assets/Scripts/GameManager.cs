using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerData pd;
    [SerializeField] GunBehaviour gb;
    [SerializeField] DoorBehaviour entryDoor;
    [SerializeField] SpawnManager spawner;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject interactText;
    public Round roundData;
    public bool canStart = true;
    public int eliminations = 0;
    
    public bool playing;
    float timer;
    int roundNumber;

    void Start()
    {
        playing = false;
        timer = 0f;
        roundNumber = 0;
    }

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
            playing = true;
            timer = pd.roundTime;
            pd.RefreshRoundData(gb.weaponIndex);
            spawner.StartSpawning();
            entryDoor.LockDoor();
        }
    }

    public void StopGame()
    {
        if (playing) {
            playing = false;
            timer = 0;
            roundNumber += 1;
            pd.ApplyRoundData();
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
