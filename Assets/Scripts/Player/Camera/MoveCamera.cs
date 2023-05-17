using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{

    [SerializeField] private Transform cameraPos;
    [SerializeField] private Canvas canva;
    void Update()
    {
        transform.position = cameraPos.position;
    }

    public void OnCloseCanvas(InputValue input)
    {
        canva.gameObject.SetActive(input.isPressed);
    }
}
