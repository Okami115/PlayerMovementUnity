using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform spawnBullet;
    [SerializeField] private GameObject bullet;

    [SerializeField] private float shootRate = 1.0f;
    [SerializeField] private float shootForce = 1000f;

    [SerializeField] private float shootRateTime = 0;

    [SerializeField] private bool isShooting = false;


    void Update()
    {
        if(Time.time > shootRateTime && isShooting)
        {
            GameObject newBullet;

            newBullet = Instantiate(bullet, spawnBullet.position, spawnBullet.rotation);

            newBullet.GetComponent<Rigidbody>().AddForce(spawnBullet.forward * shootForce);

            shootRateTime = Time.time + shootRate;


            Destroy(newBullet, 4);
        }

    }

    public void OnShoot(InputValue input)
    {
        isShooting = input.isPressed;

        Debug.Log($"{isShooting}");
    }
}
