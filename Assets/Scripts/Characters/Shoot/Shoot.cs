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

    [SerializeField] private int bulletsInMagazine;
    [SerializeField] private int maxBulletsInMagazine = 6;



    void Update()
    {
        if(Time.time > shootRateTime && isShooting && bulletsInMagazine > 0)
        {
            GameObject newBullet;

            newBullet = Instantiate(bullet, spawnBullet.position, spawnBullet.rotation);

            newBullet.GetComponent<Rigidbody>().AddForce(spawnBullet.forward * shootForce);

            shootRateTime = Time.time + shootRate;

            bulletsInMagazine--;

            Destroy(newBullet, 4);
        }

    }

    public void OnShoot(InputValue input)
    {
        isShooting = input.isPressed;
    }

    public void OnReload()
    {
        bulletsInMagazine = maxBulletsInMagazine;
    }
}
