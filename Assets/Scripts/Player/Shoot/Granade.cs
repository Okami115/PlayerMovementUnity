using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

/// <summary>
/// Contains the logic of the bullet
/// </summary>
public class Granade : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private int damage;
    [SerializeField] private int range;
    [SerializeField] private MeshRenderer granadeMesh;

    [SerializeField] private ParticleSystem PSystem;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        PSystem.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(explocion());
    }

    IEnumerator explocion()
    {
        granadeMesh.enabled = false;

        this.GetComponent<Rigidbody>().isKinematic = true;
        PSystem.Play();

        Collider[] hitCollider = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in hitCollider) 
        { 
            if(collider.GetComponent<Health>() != null)
            {
                collider.gameObject.GetComponent<Health>().TakeDamage(damage);
                manager.Credits += damage;
            }
        }

        Destroy(this.gameObject, 3);
        yield return null;
    }

}
