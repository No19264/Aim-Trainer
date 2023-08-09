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

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

	void Update()
    {
		rotation.x += Input.GetAxis(xAxis) * pd.horizontalSensitivity * Time.deltaTime; // REMOVE Time.deltaTime if navigation gets too laggy
		rotation.y += Input.GetAxis(yAxis) * pd.verticalSensitivity * Time.deltaTime;
		rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

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

        // Applying Rotations to Camera and Player
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
		var yQuat = Quaternion.AngleAxis(rotation.y + recoilRotation, Vector3.left);
		transform.localRotation = xQuat * yQuat;
        playerTransform.rotation = xQuat;
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

    public void ScaleCameraSensitivity(float percent) {
        sensitivityX = pd.horizontalSensitivity * percent;
        sensitivityY = pd.verticalSensitivity * percent;
    }
}
