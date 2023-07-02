using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private float mouseSensitivity = 100f;
    private Vector2 mouseLooK;

    private float xRotation = 0;
    private float yRotation = 0;

    [SerializeField] private Transform orientation;

    private void Start()
    {
        controller = FindAnyObjectByType<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller.Locking += inputCamera;
    }

    private void Update()
    {
        float mouseX = mouseLooK.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseLooK.y * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void inputCamera(Vector2 input)
    {
        mouseLooK = input;
    }
    
}
