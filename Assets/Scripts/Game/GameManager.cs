using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //TODO: TP2 - SOLID
    [SerializeField] private int credits;
    [SerializeField] private TextMeshProUGUI pointsText;

    public int Credits { get => credits; set => credits = value; }

    void Update()
    {
        pointsText.text = credits.ToString();
    }
}
