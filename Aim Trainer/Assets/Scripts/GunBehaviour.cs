using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] Animator gunAnim;
    [SerializeField] Animator gunPosAnim;
    [SerializeField] Weapon[] weaponList;
    [SerializeField] Transform[] barrelTransformList;
    [Space]
    [SerializeField] GameObject rifleAttachment;
    [SerializeField] GameObject rifleStock;
    [SerializeField] GameObject sniperAttachment;
    [SerializeField] GameObject sniperStock;
    [SerializeField] GameObject scope;

    CameraBehaviour cameraScript;
    Weapon selectedWeapon;
    int weaponIndex = 0;
    float timeToNextShot;
    bool aiming;
    
    void Awake()
    {
        cameraScript = mainCamera.GetComponent<CameraBehaviour>();
        selectedWeapon = weaponList[weaponIndex];
    }
    
    // Update is called once per frame
    void Update()
    {
        // Shoot when left click
        if (Input.GetMouseButton(0) && timeToNextShot <= 0)
        {
            ShootBullet();
            timeToNextShot = 60 / selectedWeapon.rpm;
        }
        // Time down the shot timer
        if (timeToNextShot > 0) timeToNextShot -= Time.deltaTime;

        // Aiming 
        if (Input.GetMouseButtonDown(1)) {
            aiming = true;
            gunPosAnim.SetBool("Aiming", true);
            gunPosAnim.SetFloat("Gun State", weaponIndex == 2 ? 1 : 0);
            cameraScript.SetFOV((float)(70 - (weaponIndex + 1) * 10)); 
        }
        if (Input.GetMouseButtonUp(1)) { 
            aiming = false;
            gunPosAnim.SetBool("Aiming", false);
            cameraScript.SetFOV(70f);
        }

        // Switching weapons
        if (!aiming) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchToWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchToWeapon(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchToWeapon(2);
        }
    }

    // Spawn bullet and call the ApplyRecoil() function in the camera script
    void ShootBullet()
    {
        RaycastHit hit;
        Vector3 point;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, selectedWeapon.range)) {
            point = hit.point;
        } else {
            point = mainCamera.transform.forward * selectedWeapon.range + mainCamera.transform.position;
        }
        Instantiate(selectedWeapon.bullet, barrelTransformList[weaponIndex].position, Quaternion.LookRotation(point - barrelTransformList[weaponIndex].position));
        mainCamera.GetComponent<CameraBehaviour>().ApplyRecoil(selectedWeapon.recoil);

        // Play gun animation
        if (weaponIndex == 0) gunAnim.SetTrigger("pistolShoot");
        else if (weaponIndex == 1) gunAnim.SetTrigger("rifleShoot");
        else if (weaponIndex == 2) gunAnim.SetTrigger("sniperShoot");
    }

    // Switch weapon stats, what parts are displayed, if the weapon index exists
    void SwitchToWeapon(int index)
    {
        // Check to see if the weapon is in weaponList
        if (index < weaponList.Length) {
            // Check to see if you are not already selecting weapon
            if (weaponIndex != index) {
                // Change weapon stats
                weaponIndex = index;
                selectedWeapon = weaponList[weaponIndex];
                // Change Gun visual
                switch (weaponIndex) {
                    case 0:
                        rifleAttachment.SetActive(false);
                        rifleStock.SetActive(false);
                        sniperAttachment.SetActive(false);
                        sniperStock.SetActive(false);
                        scope.SetActive(false);
                        break;
                    case 1:
                        rifleAttachment.SetActive(true);
                        rifleStock.SetActive(true);
                        sniperAttachment.SetActive(false);
                        sniperStock.SetActive(false);
                        scope.SetActive(false);
                        break;
                    case 2:
                        rifleAttachment.SetActive(false);
                        rifleStock.SetActive(false);
                        sniperAttachment.SetActive(true);
                        sniperStock.SetActive(true);
                        scope.SetActive(true);
                        break;
                    default:
                        Debug.Log("Gun model gameobject not callibrated");
                        break;
                }
            }
        } else Debug.Log("Weapon Index does not exist");
    }
}
