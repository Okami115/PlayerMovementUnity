using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuyObjets : MonoBehaviour
{
    [SerializeField] private float distToBuy;
    [SerializeField] private int price;
    [SerializeField] private TextMeshProUGUI mensages;
    [SerializeField] private GameObject Player;
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private GameManager gameManager;


    private bool input;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();
        PlayerController.Buy += isBuying;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, this.transform.position);

        if(distance < distToBuy )
        {
            mensages.text = $"press E to buy ({price})";

            if(input && gameManager.GetPoints() >= price)
            {

                Destroy(this.gameObject);
                gameManager.SetPoints(gameManager.GetPoints() - price);
                mensages.text = $" ";
            }
        }
        else
        {
            mensages.text = $" ";
        }
    }

    private void isBuying(bool input)
    {
        this.input = input;
    }
}
