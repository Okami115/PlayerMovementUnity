using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int healt;
    [SerializeField] private int damage;

    public int GetHealt()
    {
        return healt;
    }

    public void GetDamage(int damage)
    {
        healt -= damage;
    }
}
