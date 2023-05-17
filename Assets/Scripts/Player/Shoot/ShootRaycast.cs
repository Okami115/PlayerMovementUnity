using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ShootRaycast : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform RaycastController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI maxBullets;
    [SerializeField] private TextMeshProUGUI currentBullets;
    [SerializeField] Animator shootingAnimator;
    [SerializeField] private float Range;
    [SerializeField] bool isAutomatic;

    private bool isShooting;
    private bool canShoot = true;

    private bool reloading = false;
    [SerializeField] private float timeToReload;
    [SerializeField] private float currentTimeToReload;

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
            shootingAnimator.SetBool("Reloading", false);
        }


        if(canShoot && isShooting && timeToShoot > fireRate && bulletsInMagazine != 0 && !reloading) 
        {
            shootingAnimator.Play("Shoot");

            RaycastHit hit;
            if (Physics.Raycast(RaycastController.position, RaycastController.forward,out hit, Range))
            {
                if(hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Enemy>().GetDamage(damage);
                    gameManager.AddPoints(10);
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
        if(!reloading)
        {
            reloading = true;
            bulletsInMagazine = maxBulletsInMagazine;
            shootingAnimator.SetBool("Reloading", true);
            shootingAnimator.Play("Reload");

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(RaycastController.position, RaycastController.forward.normalized * Range);
    }
}
