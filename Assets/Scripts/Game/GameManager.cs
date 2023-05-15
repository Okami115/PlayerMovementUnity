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
        shootRaycast = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootRaycast>();
        shootRaycast.Impact += AddPoints;
    }


    void Update()
    {
        pointsText.text = points.ToString();
    }

    private void AddPoints(object sender, EventArgs e)
    {
        points += 10;
    }

    public void SetPoints(int points)
    {
        this.points += points;
    }

    public int GetPoints()
    {
        return points;
    }
}
