using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the logic of the bullet
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private string enemy = "Enemy";
    [SerializeField] private int damage;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(enemy))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(100);
            manager.Credits += damage;
            Destroy(this.gameObject);
        }
    }
}
