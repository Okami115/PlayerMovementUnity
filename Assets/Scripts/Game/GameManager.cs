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
        shootRaycast.Impact += GetPoints;
    }


    void Update()
    {
        pointsText.text = points.ToString();
    }

    private void GetPoints(object sender, EventArgs e)
    {
        points += 10;
    }
}
