using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    [SerializeField] GameObject modeSelectScreen;
    [SerializeField] GameObject statsScreen; 
    [SerializeField] GameObject settingsScreen; 
    Animator animator;

    void Start() 
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            BackButton();
        }
    }

    void toggleScreen(GameObject screen) 
    {
        // Hide all tabs
        modeSelectScreen.SetActive(false);
        statsScreen.SetActive(false);
        settingsScreen.SetActive(false);

        // Show the required one
        screen.SetActive(true);
    }

    // Menu Buttons
    public void StartButton() 
    {
        animator.SetTrigger("Page Down");
        toggleScreen(modeSelectScreen);
    }

    public void StatsButton() 
    {
        animator.SetTrigger("Page Down");
        toggleScreen(statsScreen);
    }

    public void SettingsButton() 
    {
        animator.SetTrigger("Page Down");
        toggleScreen(settingsScreen);
    }

    public void QuitButton() 
    {
        Application.Quit();
    }

    // Screen Buttons
    public void BackButton()
    {
        animator.SetTrigger("Page Up");
    }

    public void Standard()
    {
        SceneManager.LoadScene("Arena");
    }
}
 