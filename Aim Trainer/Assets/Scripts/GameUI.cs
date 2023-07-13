using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] GunBehaviour gb;
    [SerializeField] GameObject escScreen;
    [Space]
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI clipText;
    [Space]
    [SerializeField] TextMeshProUGUI weaponHitText;
    [SerializeField] TextMeshProUGUI weaponHeadText;
    [SerializeField] TextMeshProUGUI totalHitText;
    [SerializeField] TextMeshProUGUI totalHeadText;

    void Start()
    {
        escScreen.SetActive(false);
        UpdateText();
    }

    void Update()
    {
        // Pause game on Escape
        if (Input.GetKeyDown(KeyCode.Escape)) {
            escScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Update is called once per frame
    public void UpdateText()
    {
        ammoText.text = "" + gb.GetAmmoCount;
        clipText.text = "" + gb.GetClipSize;

        float[] weaponAccuracy = gb.GetWeaponAccuracy;
        float[] totalAccuracy = gb.GetTotalAccuracy;
        weaponHitText.text = "Hit: " + weaponAccuracy[0] + "%";
        weaponHeadText.text = "Headshot: " + weaponAccuracy[1] + "%";
        totalHitText.text = "Hit: " + totalAccuracy[0] + "%";
        totalHeadText.text = "Headshot: " + totalAccuracy[1] + "%";
    }

    public void ContinueButton() 
    {
        escScreen.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LeaveButton()
    {
        Debug.Log("Return to home page :)");
    }
}
