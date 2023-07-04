using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Update the new camera position
/// </summary>
public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPos;
    void Update()
    {
        transform.position = cameraPos.position;
    }
}
