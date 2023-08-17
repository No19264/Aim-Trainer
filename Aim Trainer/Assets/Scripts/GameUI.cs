using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] PlayerData pd;
    [SerializeField] GunBehaviour gb;
    [SerializeField] GameObject escScreen;
    [Space]
    [SerializeField] GameObject noGunText;
    [SerializeField] Image gunIcon;
    [SerializeField] Sprite[] iconSprites;
    [Space]
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI clipText;
    [Space]
    [SerializeField] TextMeshProUGUI[] weaponText;
    [SerializeField] TextMeshProUGUI[] weaponHitText;
    [SerializeField] TextMeshProUGUI[] weaponHeadText;

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

        weaponText[0].text = weaponText[1].text = pd.IndexToWeaponName(pd.roundData.weaponIndex);
        weaponHitText[0].text = weaponHitText[1].text = "HIT: " + pd.roundData.accuracy.HitPercent + "%";
        weaponHeadText[0].text = weaponHeadText[1].text = "HEADSHOT: " + pd.roundData.accuracy.HeadHitPercent + "%";
    }

    public void UpdateEquipedIcon(int index)
    {
        switch (index) {
            case 0: 
                noGunText.SetActive(false);
                gunIcon.gameObject.SetActive(true);
                gunIcon.sprite = iconSprites[0];
                break;
            case 1:
                noGunText.SetActive(false);
                gunIcon.gameObject.SetActive(true);
                gunIcon.sprite = iconSprites[1];
                break;
            case 2:
                noGunText.SetActive(false);
                gunIcon.gameObject.SetActive(true);
                gunIcon.sprite = iconSprites[2];
                break;
            default:
                noGunText.SetActive(true);
                gunIcon.gameObject.SetActive(false);
                break;
        }
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
}
