using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPos;
    void Update()
    {
        transform.position = cameraPos.position;
    }
}
