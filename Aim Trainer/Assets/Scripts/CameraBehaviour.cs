using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float sensitivityX;
    [SerializeField] float sensitivityY;

    float xRotation;
    float yRotation;
    float recoilRotation;
    float targetRecoilRotation;
    float recoilSpeed = 60f;
    bool recoiling;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        // Defining Rotations
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        // When recoiling, raise the recoil rotation. When not, decrease the recoil rotation.
        if (recoiling) {
            if (recoilRotation < targetRecoilRotation) recoilRotation += recoilSpeed * Time.deltaTime;
            else {
                targetRecoilRotation = 0;
                recoiling = false;
            }
        } else {
            if (recoilRotation > targetRecoilRotation) recoilRotation -= 60 * Time.deltaTime;
            else recoilRotation = 0;
        }
        
        // Rotating the camera and player
        transform.rotation = Quaternion.Euler(xRotation - recoilRotation, yRotation, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    public void ApplyRecoil(float rotation)
    {
        recoiling = true;
        targetRecoilRotation = recoilRotation + rotation;
        recoilSpeed = rotation * 10; // This makes sure that no matter the recoil, it should take 0.1s to reach the endpoint
    }

    public void SetFOV(float FOV) {
        GetComponent<Camera>().fieldOfView = FOV;
    }
}
