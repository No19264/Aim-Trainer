using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] Animator gunAnim;
    [SerializeField] Animator gunPosAnim;
    [SerializeField] Weapon[] weaponList;
    [SerializeField] Transform[] barrelTransformList;
    [SerializeField] LayerMask raycastIgnore;
    [SerializeField] RecticleManager rm;
    [SerializeField] GameUI gu;
    [Space]
    [SerializeField] GameObject fullGun;
    [SerializeField] GameObject rifleAttachment;
    [SerializeField] GameObject rifleStock;
    [SerializeField] GameObject sniperAttachment;
    [SerializeField] GameObject sniperStock;
    [SerializeField] GameObject scope;
    [SerializeField] GameObject markerPrefab;
    [Space]
    [SerializeField] GameObject SniperScreen;

    CameraBehaviour cameraScript;
    int weaponIndex = 0;
    float timeToNextShot;
    bool aiming;
    bool reloading;
    
    void Awake()
    {
        cameraScript = mainCamera.GetComponent<CameraBehaviour>();
        raycastIgnore = ~raycastIgnore;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!reloading) {
            // Shoot when left click
            if (Input.GetMouseButton(0) && timeToNextShot <= 0)
            {
                if (weaponList[weaponIndex].currentAmmo > 0) {
                    ShootBullet();
                    timeToNextShot = 60 / weaponList[weaponIndex].rpm;
                } else {
                    // Flash the ammo screen
                }
            }

            // Reloading
            if (Input.GetKeyDown(KeyCode.R)) {
                reloading = true;
                DoReloadSuspension();
                gunAnim.SetTrigger("reload");
            }

            // Aiming 
            if (Input.GetMouseButtonDown(1)) {
                aiming = true;
                gunPosAnim.SetBool("Aiming", true);
                if (weaponIndex != 2) {
                    cameraScript.SetFOV(70f - (float)(weaponIndex + 1) * 10f);
                    cameraScript.ScaleCameraSensitivity(1f - (float)(weaponIndex + 1) * 0.1f);
                }
            }
            if (Input.GetMouseButtonUp(1)) { 
                aiming = false;
                gunPosAnim.SetBool("Aiming", false);
                cameraScript.SetFOV(70f);
                cameraScript.ScaleCameraSensitivity(1f);
                fullGun.SetActive(true);
                SniperScreen.SetActive(false);
            }

            // Switching weapons
            if (!aiming) {
                if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchToWeapon(0);
                if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchToWeapon(1);
                if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchToWeapon(2);
            }
        }
        // Time down the shot timer
        if (timeToNextShot > 0) timeToNextShot -= Time.deltaTime;
    }

    // Spawn bullet and call the ApplyRecoil() function in the camera script
    void ShootBullet()
    {
        // Cast the ray
        RaycastHit hit;
        Vector3 point;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, weaponList[weaponIndex].range, raycastIgnore)) {
            point = hit.point;
            
            // Damage the enemy if they are hit
            if (hit.collider.tag == "Body") { 
                hit.collider.transform.root.GetComponent<BotBehaviour>().DamageBot(weaponList[weaponIndex].damage);
                weaponList[weaponIndex].hitCount += 1;
                rm.CreateHitMarker();
            }
            if (hit.collider.tag == "Head") {
                hit.collider.transform.root.GetComponent<BotBehaviour>().DamageBot(weaponList[weaponIndex].damage * 1.5f);
                weaponList[weaponIndex].hitCount += 1;
                weaponList[weaponIndex].headHitCount += 1;
                rm.CreateHitMarker(true);
            }
        } else {
            point = mainCamera.transform.forward * weaponList[weaponIndex].range + mainCamera.transform.position;
        }
        
        // Spawn the bullet visual, apply recoil and reduce ammo
        Instantiate(weaponList[weaponIndex].bullet, barrelTransformList[weaponIndex].position, Quaternion.LookRotation(point - barrelTransformList[weaponIndex].position));
        mainCamera.GetComponent<CameraBehaviour>().ApplyRecoil(weaponList[weaponIndex].recoil);
        weaponList[weaponIndex].currentAmmo -= 1;
        weaponList[weaponIndex].shotCount += 1;

        // Play gun animation
        if (weaponIndex == 0) gunAnim.SetTrigger("pistolShoot");
        else if (weaponIndex == 1) gunAnim.SetTrigger("rifleShoot");
        else if (weaponIndex == 2) gunAnim.SetTrigger("sniperShoot");

        // Update Text
        gu.UpdateText();
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
                // Change Gun visual
                switch (weaponIndex) {
                    case 0:
                        rifleAttachment.SetActive(false);
                        rifleStock.SetActive(false);
                        sniperAttachment.SetActive(false);
                        sniperStock.SetActive(false);
                        scope.SetActive(false);
                        gunPosAnim.SetBool("Using Sniper", false);
                        break;
                    case 1:
                        rifleAttachment.SetActive(true);
                        rifleStock.SetActive(true);
                        sniperAttachment.SetActive(false);
                        sniperStock.SetActive(false);
                        scope.SetActive(false);
                        gunPosAnim.SetBool("Using Sniper", false);
                        break;
                    case 2:
                        rifleAttachment.SetActive(false);
                        rifleStock.SetActive(false);
                        sniperAttachment.SetActive(true);
                        sniperStock.SetActive(true);
                        scope.SetActive(true);
                        gunPosAnim.SetBool("Using Sniper", true);
                        break;
                    default:
                        Debug.Log("Gun model gameobject not callibrated");
                        break;
                }
            }
        } else Debug.Log("Weapon Index does not exist");
        gu.UpdateText();
    }

    public void SniperAimEffects()
    {
        cameraScript.SetFOV(35f); 
        cameraScript.ScaleCameraSensitivity(0.5f);
        fullGun.SetActive(false);
        SniperScreen.SetActive(true);
    }

    async void DoReloadSuspension() 
    {
        await Task.Delay(1500);
        weaponList[weaponIndex].currentAmmo = weaponList[weaponIndex].clipSize;
        reloading = false;
        gu.UpdateText();
    }

    public int GetAmmoCount {
        get {return weaponList[weaponIndex].currentAmmo;}
    }

    public int GetClipSize {
        get {return weaponList[weaponIndex].clipSize;}
    }

    // Returns the accuracy decimal to 2DP is it is not NaN (Not A Number)
    public float[] GetWeaponAccuracy {
        get{
            float first = (!float.IsNaN(weaponList[weaponIndex].hitPercent)) ? weaponList[weaponIndex].hitPercent: 0f;
            float second = (!float.IsNaN(weaponList[weaponIndex].headHitPercent)) ? weaponList[weaponIndex].headHitPercent : 0f;
            float[] accuracy = new float[] {first, second};
            return accuracy;
        }
    }

    // Finds the mean accuracy of all of the weapons (ignores NaN's)
    public float[] GetTotalAccuracy {
        get{
            float hit = 0f;
            float head = 0f;
            int i = 0;
            foreach (Weapon weapon in weaponList) {
                if (!float.IsNaN(weapon.hitPercent)) {
                    hit += weapon.hitPercent;
                    if (!float.IsNaN(weapon.headHitPercent)) head += weapon.headHitPercent;
                    i += 1;
                } 
            }
            float first = (hit != 0) ? Mathf.Round(hit / (float)i * 10f) / 10f : 0f;
            float second = (hit != 0) ? Mathf.Round(head / (float)i * 10f) / 10f : 0f;
            float[] accuracy = new float[] {first, second};
            return accuracy;
        }
    }
}
