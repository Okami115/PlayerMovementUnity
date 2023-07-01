using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class BuyGuns : BuyObjets
{

    //TODO: TP2 - SOLID
    [SerializeField] private List<GameObject> Guns;
    [SerializeField] private GameObject GunForSell;
    [SerializeField] private string player = "Player";
    private bool hasGun;

    private void Update()
    {
        for (int i = 0; i < Guns.Count; i++)
        {
            if (Guns[i].activeSelf)
            {
                hasGun = Guns[i] == GunForSell;
            }
        }

        //TODO: Fix - Could be a coroutine
        if (canBuy && !hasGun)
        {
            mensages.text = $"press E to buy ({price})";

            if (input && gameManager.GetPoints() >= price)
            {
                soundManager.PlaySound(buySound);

                for (int i = 0; i < Guns.Count; i++)
                {
                    Guns[i].SetActive(false);
                }

                GunForSell.SetActive(true);

                gameManager.AddPoints(-price);
                mensages.text = $" ";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == player)
        {
            canBuy = true;
            PlayerController.Buy += isBuying;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == player)
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
