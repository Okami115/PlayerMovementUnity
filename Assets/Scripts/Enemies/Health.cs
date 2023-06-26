using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a component to add Health on the Objets
/// </summary>
public class Health : MonoBehaviour
{

    [SerializeField] private int hPoints;

    public int HPoints { get => hPoints; set => hPoints = value; }

    public void TakeDamage(int damage)
    {
        hPoints -= damage;
    }
}
