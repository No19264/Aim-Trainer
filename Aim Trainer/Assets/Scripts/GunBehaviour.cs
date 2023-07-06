using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] Transform barrelTransform;
    [SerializeField] float rpm;
    [SerializeField] float recoil;
    [SerializeField] float range;
    [SerializeField] GameObject bullet;

    float timeToNextShot;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && timeToNextShot <= 0)
        {
            ShootBullet();
            timeToNextShot = 60 / rpm;
        }
        if (timeToNextShot > 0) timeToNextShot -= Time.deltaTime;
    }

    void ShootBullet()
    {
        RaycastHit hit;
        Vector3 point;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range)) {
            Debug.Log("In Range");
            point = hit.point;
        } else {
            Debug.Log("Out of Range");
            point = mainCamera.transform.forward * range + mainCamera.transform.position;
        }
        Instantiate(bullet, barrelTransform.position, Quaternion.LookRotation(point - barrelTransform.position));
        mainCamera.GetComponent<CameraBehaviour>().ApplyRecoil(recoil);
    }
}
