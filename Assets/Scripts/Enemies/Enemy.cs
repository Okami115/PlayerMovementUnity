using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Documentation - Add summary
public class Enemy : MonoBehaviour
{
    //TODO: Fix - Health
    [SerializeField] private int healt;
    [SerializeField] private int damage;

    //TODO: Fix - Should be native Setter/Getter
    public int GetHealt()
    {
        return healt;
    }

    //TODO: Fix - Should be native Setter/Getter
    public void SetHealt(int healt) 
    { 
        this.healt = healt;
    }

    //TODO: Fix - Unclear name
    public void GetDamage(int damage)
    {
        healt -= damage;
    }
}
