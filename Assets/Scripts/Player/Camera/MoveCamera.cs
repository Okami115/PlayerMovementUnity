using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{

    //TODO: TP2 - SOLID
    [SerializeField] private Transform cameraPos;
    [SerializeField] private Canvas canva;
    void Update()
    {
        transform.position = cameraPos.position;
    }

    //TODO: Fix - Using Input related logic outside of an input responsible class
    public void OnCloseCanvas(InputValue input)
    {
        canva.gameObject.SetActive(input.isPressed);
    }
}
