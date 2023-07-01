using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuyObjets : MonoBehaviour
{
    //TODO: TP2 - SOLID
    [SerializeField] protected SoundManager soundManager;
    [SerializeField] protected AudioClip buySound;
    [SerializeField] protected int price;
    [SerializeField] protected TextMeshProUGUI mensages;
    [SerializeField] protected PlayerController PlayerController;
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected GameObject Player;
    protected bool canBuy = false;
    protected bool input;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if(canBuy)
        {
            mensages.text = $"press E to buy ({price})";

            if(input && gameManager.GetPoints() >= price)
            {
                soundManager.PlaySound(buySound);
                Destroy(this.gameObject);
                gameManager.AddPoints(- price);
                mensages.text = $" ";
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canBuy = true;
            PlayerController.Buy += isBuying;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
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
