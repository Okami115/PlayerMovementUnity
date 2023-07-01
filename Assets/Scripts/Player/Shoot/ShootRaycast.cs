using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ShootRaycast : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform raycastController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI maxBullets;
    [SerializeField] private TextMeshProUGUI currentBullets;
    [SerializeField] Animator shootingAnimator;
    [SerializeField] private float Range;
    [SerializeField] bool isAutomatic;

    [SerializeField] AudioClip ShootSound;
    [SerializeField] AudioClip ReloadSound;

    private bool isShooting;
    private bool canShoot = true;

    private bool reloading = false;
    [SerializeField] private float timeToReload;
    [SerializeField] private float currentTimeToReload;

    [SerializeField] private int points = 10;

    [SerializeField] private int damage;
    [SerializeField]private float fireRate;
    private float timeToShoot;

    [SerializeField] private int bulletsInMagazine;
    [SerializeField] private int maxBulletsInMagazine;


    void Start()
    {
        shootingAnimator = GetComponent<Animator>();

        bulletsInMagazine = maxBulletsInMagazine;
        playerController.Shoot += Shoot;
        playerController.Reload += Reload;
    }

    void Update()
    {
        timeToShoot += Time.deltaTime;

        if(reloading)
        {
            currentTimeToReload -= Time.deltaTime;
        }
        if(currentTimeToReload < 0)
        {
            currentTimeToReload = timeToReload;
            reloading = false;
            //TODO: TP2 - SOLID
            //TODO: Fix - Hardcoded value
            shootingAnimator.SetBool("Reloading", false);
        }


        if(canShoot && isShooting && timeToShoot > fireRate && bulletsInMagazine != 0 && !reloading) 
        {
            //TODO: Fix - Hardcoded value
            shootingAnimator.Play("Shoot");
            soundManager.PlaySound(ShootSound);
            RaycastHit hit;
            if (Physics.Raycast(raycastController.position, raycastController.forward,out hit, Range))
            {
                //TODO: Fix - Hardcoded value
                if(hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Health>().TakeDamage(damage);
                    gameManager.AddPoints(points);
                }
            }

            bulletsInMagazine--;
            timeToShoot = 0;
        }

        if(!isAutomatic && isShooting)
        {
            isShooting = false;
        }

        maxBullets.text = maxBulletsInMagazine.ToString();
        currentBullets.text = bulletsInMagazine.ToString();
    }

    public void Shoot(bool input)
    {
        isShooting = input;
    }

    public void Reload()
    {
        reloading = true;
        bulletsInMagazine = maxBulletsInMagazine;
        shootingAnimator.SetBool("Reloading", true);
        shootingAnimator.Play("Reload");
        soundManager.PlaySound(ReloadSound);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(raycastController.position, raycastController.forward.normalized * Range);
    }
}
