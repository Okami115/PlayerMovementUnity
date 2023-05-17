using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    [SerializeField] private int points;
    [SerializeField] private ShootRaycast shootRaycast;


    void Start()
    {

    }


    void Update()
    {
        pointsText.text = points.ToString();
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

    public int GetPoints()
    {
        return points;
    }
}
