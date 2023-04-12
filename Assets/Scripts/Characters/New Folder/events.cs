using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class events : MonoBehaviour
{
    delegate void parameters();

    
    private Action<int> onHeal;

    [ContextMenu("Heal")]
    void Heal()
    {
        if (onHeal != null)
        {
            onHeal(5);
        }
    }

    private void Awake()
    {
        onHeal += healing;
    }

    void healing(int heal)
    {
        Debug.Log($"Se curo: {heal}");
    }
}