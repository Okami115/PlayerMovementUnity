using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI maxBullets;
    [SerializeField] private TextMeshProUGUI currentBullets;
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

    [SerializeField] private bool isShooting = false;

    [SerializeField] private int bulletsInMagazine;
    [SerializeField] private int maxBulletsInMagazine;


    void Update()
    {
        //TODO: Fix - Should be event based
        maxBullets.text = maxBulletsInMagazine.ToString();
        currentBullets.text = bulletsInMagazine.ToString();

        //TODO: Fix - Could be a coroutine
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

        //TODO: Fix - Could be a coroutine
        if (Time.time > shootRateTime && isShooting && bulletsInMagazine > 0 && !reloading)
        {
            //TODO: TP2 - SOLID
            //TODO: Fix - Hardcoded value
            shootingAnimator.Play("Shoot");
            soundManager.PlaySound(ShootSound);

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

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    void Start()
    {
        shootingAnimator = GetComponent<Animator>();

        bulletsInMagazine = maxBulletsInMagazine;
        playerController.Shoot += PhisicShoot;
        playerController.Reload += Reload;
        playerController.Moving += MovingAnimation;
    }

    //TODO: Fix - Should be native Setter/Getter
    public int getCurrentBullets()
    {
        return bulletsInMagazine;
    }
    //TODO: Fix - Should be native Setter/Getter
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
        if (!reloading && bulletsInMagazine != maxBulletsInMagazine)
        {
            reloading = true;
            bulletsInMagazine = maxBulletsInMagazine;
            //TODO: Fix - Hardcoded value
            shootingAnimator.SetBool("Reloading", true);
            //TODO: Fix - Hardcoded value
            shootingAnimator.Play("Reload");
            soundManager.PlaySound(ReloadSound);

        }
    }

    //TODO: TP2 - Remove unused methods/variables/classes
    private void MovingAnimation()
    {

    }
}
