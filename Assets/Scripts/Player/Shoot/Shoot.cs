using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class Shoot : MonoBehaviour
{
    [SerializeField]private string reloadingState = "Reloading";
    [SerializeField]private string reloadAnimation = "Reload";
    [SerializeField]private string shootAnimation = "Shoot";
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator shootingAnimator;
    [SerializeField] private Transform spawnBullet;
    [SerializeField] private GameObject bullet;
    [SerializeField] AudioClip ShootSound;
    [SerializeField] AudioClip ReloadSound;

    [SerializeField] private float shootRate;
    [SerializeField] private float shootForce; 
    [SerializeField] bool isAutomatic;

    //TODO: TP2 - Syntax - Fix declaration order
    private bool reloading = false;
    [SerializeField] private float timeToReload;
    [SerializeField] private float currentTimeToReload;

    [SerializeField] private float shootRateTime;

    [SerializeField] private int bulletsInMagazine;
    [SerializeField] private int maxBulletsInMagazine;

    public int BulletsInMagazine { get => bulletsInMagazine; set => bulletsInMagazine = value; }
    public int MaxBulletsInMagazine { get => maxBulletsInMagazine; set => maxBulletsInMagazine = value; }

    private IEnumerator ReloadAnimation()
    {
        reloading = true;
        shootingAnimator.SetBool(reloadingState, true);

        yield return new WaitForSeconds(timeToReload);

        reloading = false;
        shootingAnimator.SetBool(reloadingState, false);
    }
    public void Reload()
    {
        if (!reloading && BulletsInMagazine != MaxBulletsInMagazine)
        {
            reloading = true;
            BulletsInMagazine = MaxBulletsInMagazine;
            shootingAnimator.SetBool(reloadingState, true);
            shootingAnimator.Play(reloadAnimation);
            soundManager.PlaySound(ReloadSound);

        }
    }

    private IEnumerator ShootBullet()
    {
        if (Time.time > shootRateTime)
        {
            //TODO: TP2 - SOLID
            shootingAnimator.Play(shootAnimation);
            soundManager.PlaySound(ShootSound);

            GameObject newBullet = Instantiate(bullet, spawnBullet.position, spawnBullet.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(spawnBullet.forward * shootForce);

            shootRateTime = Time.time + shootRate;

            BulletsInMagazine--;

            Destroy(newBullet, 2);
        }

        yield return null;
    }
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
