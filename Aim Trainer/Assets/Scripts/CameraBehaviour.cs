using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    [SerializeField] PlayerData pd;
    [Space]
    [SerializeField] Transform playerTransform;
	[Range(45f, 90f)][SerializeField] float yRotationLimit = 88f;

	Vector2 rotation = Vector2.zero;
	const string xAxis = "Mouse X";
	const string yAxis = "Mouse Y";
    float sensitivityX;
    float sensitivityY;
    float recoilRotation;
    float targetRecoilRotation;
    float recoilSpeed = 60f;
    bool recoiling;

    // Initialise Variables
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

	void Update()
    {
        // Caluclate screen movement by mouse movement
		rotation.x += Input.GetAxis(xAxis) * pd.horizontalSensitivity * Time.deltaTime; // REMOVE Time.deltaTime if navigation gets too laggy
		rotation.y += Input.GetAxis(yAxis) * pd.verticalSensitivity * Time.deltaTime;
		rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

        // Clamping target Recoil rotation
        if (targetRecoilRotation + rotation.y > yRotationLimit) targetRecoilRotation = yRotationLimit - rotation.y;

        // When recoiling, raise the recoil rotation. When not, decrease the recoil rotation.
        if (recoiling) {
            if (recoilRotation < targetRecoilRotation) {
                recoilRotation += recoilSpeed * Time.deltaTime;
            } 
            else {
                targetRecoilRotation = 0;
                recoiling = false;
            }
        } else {
            if (recoilRotation > targetRecoilRotation) recoilRotation -= 90 * Time.deltaTime;
            else recoilRotation = 0;
        }

        // Clamping recoil Rotation
        if (rotation.y + recoilRotation > yRotationLimit) recoilRotation = yRotationLimit - rotation.y;

        // Applying Rotations to Camera and Player
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
		var yQuat = Quaternion.AngleAxis(Mathf.Clamp(rotation.y + recoilRotation, -yRotationLimit, yRotationLimit), Vector3.left);
		transform.localRotation = xQuat * yQuat;
        playerTransform.rotation = xQuat;
	}

    // Apply recoil to camera (for othe objects to use)
    public void ApplyRecoil(float _rotation)
    {
        recoiling = true;
        targetRecoilRotation = Mathf.Clamp(recoilRotation + _rotation, -yRotationLimit, yRotationLimit);
        recoilSpeed = _rotation * 10; // This makes sure that no matter the recoil, it should take 0.1s to reach the endpoint
    }

    // Apply Feild of View
    public void SetFOV(float FOV) {
        GetComponent<Camera>().fieldOfView = FOV;
    }

    // Scale camera sensitivity when aimimg (make it easier to aim)
    public void ScaleCameraSensitivity(float percent) {
        sensitivityX = pd.horizontalSensitivity * percent;
        sensitivityY = pd.verticalSensitivity * percent;
    }
}
