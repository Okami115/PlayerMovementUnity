using System;
using System.Collections;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Contains the logic of physical bullets
/// </summary>
[RequireComponent(typeof(Animator))]
public class Shoot : MonoBehaviour
{
    [SerializeField] private string reloadState = "Reloading";
    [SerializeField] private string reloadAnimation = "Reload";
    [SerializeField] private string shootAnimation = "Shoot";

    [SerializeField] Animator shootingAnimator;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform spawnBullet;
    [SerializeField] private GameObject bullet;

    [SerializeField] private float shootRate;
    [SerializeField] private float shootForce; 
    [SerializeField] bool isAutomatic;

    [SerializeField] private float timeToReload;
    [SerializeField] private float currentTimeToReload;
    private bool reloading = false;

    [SerializeField] private float shootRateTime;

    [SerializeField] private int bulletsInMagazine;
    [SerializeField] private int maxBulletsInMagazine;

    public event Action relaod;
    public event Action shoot;

    public int BulletsInMagazine { get => bulletsInMagazine; set => bulletsInMagazine = value; }
    public int MaxBulletsInMagazine { get => maxBulletsInMagazine; set => maxBulletsInMagazine = value; }

    /// <summary>
    /// Start reload animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReloadAnimation()
    {
        reloading = true;
        shootingAnimator.SetBool(reloadState, true);

        yield return new WaitForSeconds(timeToReload);

        reloading = false;
        shootingAnimator.SetBool(reloadState, false);
    }

    /// <summary>
    /// Start the reload logic
    /// </summary>
    public void Reload()
    {
        if (!reloading && BulletsInMagazine != MaxBulletsInMagazine)
        {
            reloading = true;
            BulletsInMagazine = MaxBulletsInMagazine;
            shootingAnimator.SetBool(reloadState, true);
            shootingAnimator.Play(reloadAnimation);
            relaod.Invoke();

        }
    }


    /// <summary>
    /// Spawn the bullet and execute the firing logic
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootBullet()
    {
        if (Time.time > shootRateTime)
        {
            //TODO: TP2 - SOLID
            shootingAnimator.Play(shootAnimation);
            shoot.Invoke();

            GameObject newBullet = Instantiate(bullet, spawnBullet.position, spawnBullet.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(spawnBullet.forward * shootForce);

            shootRateTime = Time.time + shootRate;

            BulletsInMagazine--;

            Destroy(newBullet, 2);
        }

        yield return null;
    }

    /// <summary>
    /// Start the triggering coroutine
    /// </summary>
    /// <param name="input"></param>
    public void PhisicShoot(bool input)
    {
        if (input && BulletsInMagazine > 0 && !reloading)
        {
            StartCoroutine(ShootBullet());
        }

        if (!isAutomatic)
        {
            StopCoroutine(ShootBullet());
        }
    }

    private void Start()
    {
        shootingAnimator = GetComponent<Animator>();

        BulletsInMagazine = MaxBulletsInMagazine;
        playerController.Shoot += PhisicShoot;
        playerController.Reload += Reload;
    }

    void Update()
    {

        if (reloading)
        {
            currentTimeToReload -= Time.deltaTime;
            if (currentTimeToReload < 0)
            {
                StartCoroutine(ReloadAnimation());
                currentTimeToReload = timeToReload;
            }
        }
    }


}
