using System;
using UnityEngine;

/// <summary>
/// This is a component to add Health on the Objets
/// </summary>
public class Health : MonoBehaviour
{
    [SerializeField] private int hPoints;
    [SerializeField] private int maxHealth;
    public event Action<Health> wasDefeated;
    public int HPoints { get => hPoints; set => hPoints = value; }

    /// <summary>
    /// Takes damage and subtracts it from HP
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        hPoints -= damage;

        if (HPoints <= 0)
        {
            wasDefeated?.Invoke(this);
        }
    }

    /// <summary>
    /// Restore HP to max
    /// </summary>
    public void RestartHP() 
    {
        HPoints = maxHealth;
    }
}
