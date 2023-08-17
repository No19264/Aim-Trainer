using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] PlayerData pd;
    [SerializeField] GameManager gm;
    [SerializeField] GameObject mainCamera;
    [SerializeField] Animator gunAnim;
    [SerializeField] Animator gunPosAnim;
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
    [HideInInspector] public int weaponIndex = 999;
    float timeToNextShot;
    bool aiming;
    bool reloading;
    
    void Awake()
    {
        cameraScript = mainCamera.GetComponent<CameraBehaviour>();
        raycastIgnore = ~raycastIgnore;
        SwitchToWeapon();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (weaponIndex != 999)
        {
            if (!reloading) {
                // Shoot when left click
                if (Input.GetMouseButton(0) && timeToNextShot <= 0)
                {
                    if (pd.weaponList[weaponIndex].currentAmmo > 0) {
                        ShootBullet();
                        timeToNextShot = 60 / pd.weaponList[weaponIndex].rpm;
                    } else {
                        Reload();
                    }
                }

                // Reloading
                if (Input.GetKeyDown(KeyCode.R)) {
                    Reload();
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
                    ResetAiming();
                }

            }
            // Time down the shot timer
            if (timeToNextShot > 0) timeToNextShot -= Time.deltaTime;
        }
    }

    void ResetAiming()
    {
        aiming = false;
        gunPosAnim.SetBool("Aiming", false);
        cameraScript.SetFOV(70f);
        cameraScript.ScaleCameraSensitivity(1f);
        fullGun.SetActive(true);
        SniperScreen.SetActive(false);
    }

    // Spawn bullet and call the ApplyRecoil() function in the camera script
    void ShootBullet()
    {
        // Cast the ray
        RaycastHit hit;
        Vector3 point;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, pd.weaponList[weaponIndex].range, raycastIgnore)) {
            point = hit.point;
            BotBehaviour botHit = hit.collider.transform.root.GetComponent<BotBehaviour>();
            // Damage the enemy if they are hit
            if (hit.collider.tag == "Body") { 
                if (botHit.health <= pd.weaponList[weaponIndex].damage) pd.roundData.eliminations += 1;
                botHit.DamageBot(pd.weaponList[weaponIndex].damage);
                if (gm.playing) pd.roundData.accuracy.hitCount += 1;
                rm.CreateHitMarker();
            }
            if (hit.collider.tag == "Head") {
                if (botHit.health <= pd.weaponList[weaponIndex].damage * 1.5f) pd.roundData.eliminations += 1;
                botHit.DamageBot(pd.weaponList[weaponIndex].damage * 1.5f);
                if (gm.playing) 
                    pd.roundData.accuracy.hitCount += 1;
                    pd.roundData.accuracy.headHitCount += 1;
                rm.CreateHitMarker(true);
            }
        } else {
            point = mainCamera.transform.forward * pd.weaponList[weaponIndex].range + mainCamera.transform.position;
        }
        
        // Spawn the bullet visual, apply recoil and reduce ammo
        Instantiate(pd.weaponList[weaponIndex].bullet, barrelTransformList[weaponIndex].position, Quaternion.LookRotation(point - barrelTransformList[weaponIndex].position));
        mainCamera.GetComponent<CameraBehaviour>().ApplyRecoil(pd.weaponList[weaponIndex].recoil);
        pd.weaponList[weaponIndex].currentAmmo -= 1;
        if (gm.playing) pd.roundData.accuracy.shotCount += 1;

        // Play gun animation
        if (weaponIndex == 0) gunAnim.SetTrigger("pistolShoot");
        else if (weaponIndex == 1) gunAnim.SetTrigger("rifleShoot");
        else if (weaponIndex == 2) gunAnim.SetTrigger("sniperShoot");

        // Update Text
        gu.UpdateText();
    }

    void Reload()
    {
        reloading = true;
        DoReloadSuspension();
        gunAnim.SetTrigger("reload");
    }

    // Switch weapon stats, what parts are displayed, if the weapon index exists
    public void SwitchToWeapon(int index = 999)
    {
        if (!aiming) {
            gu.UpdateEquipedIcon(index);
            if (index != 999) {
                // Check to see if the weapon is in pd.weaponList
                if (index < pd.weaponList.Length) {
                    // Check to see if you are not already selecting weapon
                    if (weaponIndex != index) {
                        // Change weapon stats
                        weaponIndex = index;
                        // Change Gun visual
                        fullGun.SetActive(true);
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
            } else {
                weaponIndex = 999;
                fullGun.SetActive(false);
            }
        }
        // Restore Ammo
        if (index != 999) pd.weaponList[weaponIndex].currentAmmo = pd.weaponList[weaponIndex].clipSize;
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
        ResetAiming();
        await Task.Delay(1500);
        pd.weaponList[weaponIndex].currentAmmo = pd.weaponList[weaponIndex].clipSize;
        reloading = false;
        gu.UpdateText();
    }

    public int GetAmmoCount {
        get {return (weaponIndex != 999) ? pd.weaponList[weaponIndex].currentAmmo : 0;}
    }

    public int GetClipSize {
        get {return (weaponIndex != 999) ? pd.weaponList[weaponIndex].clipSize : 0;}
    }
}
