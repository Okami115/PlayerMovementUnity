using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ShootRaycast : MonoBehaviour
{
    [SerializeField] private Transform RaycastController;
    [SerializeField] private float Range = 10;

    private bool isShooting;

    [SerializeField]private float fireRate = 0.0f;
    private float timeToShoot;

    [SerializeField] private int bulletsInMagazine;
    [SerializeField] private int maxBulletsInMagazine = 71;


    // Start is called before the first frame update
    void Start()
    {
        bulletsInMagazine = maxBulletsInMagazine;
    }

    // Update is called once per frame
    void Update()
    {
        timeToShoot += Time.deltaTime;

        if(isShooting && timeToShoot > fireRate && bulletsInMagazine != 0) 
        {
            RaycastHit hit;            
            if(Physics.Raycast(RaycastController.position, RaycastController.forward,out hit, Range))
            {
                if(hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Enemy>().GetDamage(2);
                }
            }

            bulletsInMagazine--;
            timeToShoot = 0;
        }
    }

    public int getCurrentBullets()
    {
        return bulletsInMagazine;
    }
    public int getMaxBullets()
    {
        return maxBulletsInMagazine;
    }

    public void OnShoot(InputValue input)
    {
        isShooting = input.isPressed;
        Debug.Log($"{isShooting}");
    }

    public void OnReload()
    {
        bulletsInMagazine = maxBulletsInMagazine;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(RaycastController.position, RaycastController.forward.normalized * Range);
    }
}
