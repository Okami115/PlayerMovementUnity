using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

/// <summary>
/// Control the player's camera
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float joystickSensitivity = 1;
    private Vector2 mouseLooK;
    private Vector2 joystickLooK;

    private float xRotation = 0;
    private float yRotation = 0;

    [SerializeField] private Transform orientation;

    private void OnEnable()
    {
        controller = FindAnyObjectByType<PlayerController>();
        controller.Looking += inputCamera;
        controller.JoystickLook += inputJoystick;
    }

    private void OnDisable()
    {
        controller.Looking -= inputCamera;
        controller.JoystickLook -= inputJoystick;
    }

    private void Update()
    {
        if(Time.timeScale != 0)
        {
            float joyX = joystickLooK.x * joystickSensitivity;
            float joyY = joystickLooK.y * joystickSensitivity;

            yRotation += joyX;

            xRotation -= joyY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    /// <summary>
    /// Set camera movement based on input
    /// </summary>
    /// <param name="input"></param>
    private void inputCamera(Vector2 input)
    {
        if(Time.timeScale != 0)
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

    /// <summary>
    /// Set camera movement based on input with joystick
    /// </summary>
    /// <param name="input"></param>
    private void inputJoystick(Vector2 input)
    {
        joystickLooK = input;
    }
}
