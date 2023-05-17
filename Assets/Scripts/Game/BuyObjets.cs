using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuyObjets : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private TextMeshProUGUI mensages;
    [SerializeField] private GameObject Player;
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private GameManager gameManager;
    bool canBuy = false;


    private bool input;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(canBuy)
        {
            mensages.text = $"press E to buy ({price})";

            if(input && gameManager.GetPoints() >= price)
            {

                Destroy(this.gameObject);
                gameManager.AddPoints(gameManager.GetPoints() - price);
                mensages.text = $" ";
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            Debug.Log("Enter");
            canBuy = true;
            PlayerController.Buy += isBuying;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Exit");
            canBuy = false;
            PlayerController.Buy -= isBuying;
            mensages.text = $" ";
        }
    }

    private void isBuying(bool input)
    {
        this.input = input;
    }
}
