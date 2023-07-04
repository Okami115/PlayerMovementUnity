using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Control the player's camera
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private float mouseSensitivity = 0.1f;
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

    /// <summary>
    /// Set camera movement based on input
    /// </summary>
    /// <param name="input"></param>
    private void inputCamera(Vector2 input)
    {
        mouseLooK = input;
        float mouseX = mouseLooK.x * mouseSensitivity;
        float mouseY = mouseLooK.y * mouseSensitivity;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    
}
