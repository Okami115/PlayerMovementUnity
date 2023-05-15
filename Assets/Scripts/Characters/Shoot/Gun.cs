using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Gun : MonoBehaviour
{
    [SerializeField] private List<GameObject> Guns;

    [SerializeField] private PlayerMovement playerMovement;
    float time = 0.0001f; 

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerMovement.IsWalking += isMoving;
    }


    private void isMoving(object sender, EventArgs e)
    {

        for (int i = 0; i < Guns.Count; i++)
        {
            
            if(time < 0)
            {
                time = 0.0001f;
            }
            else
            {
                time = -0.0001f;
            }

            Debug.Log("Moving");
            Guns[i].transform.localPosition = new Vector3(Guns[i].transform.position.x, Guns[i].transform.position.y, Guns[i].transform.position.z + (Mathf.Sin(time)));
        }
    }
}
