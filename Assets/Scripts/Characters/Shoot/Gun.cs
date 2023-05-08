using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Gun : MonoBehaviour
{
    [SerializeField] private bool canShoot = true;
    [SerializeField] private Transform GunPos;
    void Awake()
    {
        GunPos = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canShoot)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else
        {
            transform.rotation = GunPos.rotation;
        }
    }

}
