using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField]private float _mouseSensitivity=100f;
    [SerializeField] private Transform _playerBody;
    private float _rotateX = 0f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float mouseX=Input.GetAxis("Mouse X")*_mouseSensitivity*Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y")*_mouseSensitivity*Time.deltaTime;

        _rotateX -= mouseY;

        _rotateX = Mathf.Clamp(_rotateX,-90f,90f);

        transform.localRotation=Quaternion.Euler(_rotateX,0f,0f);

        _playerBody.Rotate(Vector3.up * mouseX);
    }
}
