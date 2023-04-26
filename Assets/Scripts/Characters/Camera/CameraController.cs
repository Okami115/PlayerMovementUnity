using Inputs;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;

public class CameraController : MonoBehaviour
{

    private @PlayerInputs controls;

    [SerializeField] private float mouseSensitivity = 100f;
    private Vector2 mouseLooK;

    private float xRotation = 0;
    private float yRotation = 0;

    [SerializeField] private Transform orientation;


    private void Awake()
    {
        controls = new @PlayerInputs();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
 

    private void Update()
    {
        OnCamera();
    }


    public void OnCamera()
    {
        mouseLooK = controls.World.Camera.ReadValue<Vector2>();

        float mouseX = mouseLooK.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseLooK.y * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        //GetComponentInParent<Transform>().rotation = Quaternion.Euler(0, yRotation, 0);
    }
    
}
