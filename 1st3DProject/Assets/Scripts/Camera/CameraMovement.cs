using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;         // Rotates around Y
    [SerializeField] private Transform aimPivot;           // Rotates around X (for up/down aim)

    private float pitch = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Yaw rotation (left/right) - player body
        playerBody.Rotate(Vector3.up * mouseX);

        // Pitch rotation (up/down) - aim pivot
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -20f, 70f); // limit how far you can look up/down
        aimPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}

