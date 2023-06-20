using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //TODO: TP2 - SOLID
    [SerializeField] private TextMeshProUGUI pointsText;

    [SerializeField] private int points;

    void Update()
    {
        pointsText.text = points.ToString();
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

    //TODO: Fix - Should be native Setter/Getter
    public int GetPoints()
    {
        return points;
    }
}
