using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuyObjets : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioClip buySound;
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
