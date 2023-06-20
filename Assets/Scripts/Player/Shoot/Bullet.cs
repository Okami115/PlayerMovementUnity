using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private int damage;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO: Fix - Hardcoded value
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage(100);
            manager.AddPoints(damage);
            Destroy(this.gameObject);
        }
    }
}
