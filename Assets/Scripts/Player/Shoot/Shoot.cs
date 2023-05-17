using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator shootingAnimator;
    [SerializeField] private Transform spawnBullet;
    [SerializeField] private GameObject bullet;

    [SerializeField] private float shootRate;
    [SerializeField] private float shootForce; 
    [SerializeField] bool isAutomatic;

    private bool reloading = false;
    [SerializeField] private float timeToReload;
    [SerializeField] private float currentTimeToReload;

    [SerializeField] private float shootRateTime;

    [SerializeField] private bool isShooting = false;

    [SerializeField] private int bulletsInMagazine;
    [SerializeField] private int maxBulletsInMagazine;


    void Update()
    {
        if (reloading)
        {
            currentTimeToReload -= Time.deltaTime;
        }
        if (currentTimeToReload < 0)
        {
            currentTimeToReload = timeToReload;
            reloading = false;
            shootingAnimator.SetBool("Reloading", false);
        }

        if (Time.time > shootRateTime && isShooting && bulletsInMagazine > 0 && !reloading)
        {
            shootingAnimator.Play("Shoot");

            GameObject newBullet;

            newBullet = Instantiate(bullet, spawnBullet.position, spawnBullet.rotation);

            newBullet.GetComponent<Rigidbody>().AddForce(spawnBullet.forward * shootForce);

            shootRateTime = Time.time + shootRate;

            bulletsInMagazine--;

            Destroy(newBullet, 2);
        }

        if (!isAutomatic && isShooting)
        {
            isShooting = false;
        }

    }

    void Start()
    {
        shootingAnimator = GetComponent<Animator>();

        bulletsInMagazine = maxBulletsInMagazine;
        playerController.Shoot += PhisicShoot;
        playerController.Reload += Reload;
        playerController.Moving += MovingAnimation;
    }

    public int getCurrentBullets()
    {
        return bulletsInMagazine;
    }
    public int getMaxBullets()
    {
        return maxBulletsInMagazine;
    }

    public void PhisicShoot(bool input)
    {
        isShooting = input;
    }

    public void Reload()
    {
        if (!reloading)
        {
            reloading = true;
            bulletsInMagazine = maxBulletsInMagazine;
            shootingAnimator.SetBool("Reloading", true);
            shootingAnimator.Play("Reload");

        }
    }

    private void MovingAnimation()
    {

    }
}
