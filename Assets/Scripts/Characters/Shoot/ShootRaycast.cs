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

    [SerializeField]private float fireRate = 0.1f;
    private float timeToShoot;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToShoot += Time.deltaTime;

        if(isShooting && timeToShoot > fireRate) 
        {
            RaycastHit hit;            
            if(Physics.Raycast(RaycastController.position, RaycastController.forward,out hit, Range))
            {
                if(hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Impacto");
                }
            }
            timeToShoot = 0;
        }
    }

    public void OnShoot(InputValue input)
    {
        isShooting = input.isPressed;
        Debug.Log($"{isShooting}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(RaycastController.position, RaycastController.forward.normalized * Range);
    }
}
