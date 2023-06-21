using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

//TODO: OOP - Should inherit from BuyObjects
public class BuyGuns : MonoBehaviour
{
    //TODO: TP2 - SOLID
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioClip buySound;
    [SerializeField] private List<GameObject> Guns;
    [SerializeField] private GameObject GunForSell;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerController controller;
    [SerializeField] private TextMeshProUGUI mensages;
    [SerializeField] private int price;
    private bool input;
    private bool canBuy;
    bool hasGun;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Guns.Count; i++)
        {
            if (Guns[i].activeSelf)
            {
                //TODO: Fix - hasGun = Guns[i] == GunForSell
                if (Guns[i] == GunForSell)
                {
                    hasGun = true;
                }
                else
                {
                    hasGun = false;
                }
            }
        }

        //TODO: Fix - Could be a coroutine
        if (canBuy && !hasGun)
        {
            mensages.text = $"press E to buy ({price})";

            if (input && gameManager.GetPoints() >= price)
            {
                soundManager.PlaySound(buySound);
                //TODO: Fix - Why is this code here? You're not activating the other guns anywhere
                for (int i = 0; i < Guns.Count; i++)
                {
                    Guns[i].SetActive(false);
                }

                GunForSell.SetActive(true);

                gameManager.AddPoints(- price);
                mensages.text = $" ";
            }
        }
    }

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    private void OnTriggerEnter(Collider other)
    {
        //TODO: Fix - Hardcoded value
        if (other.tag == "Player")
        {
            canBuy = true;
            controller.Buy += isBuying;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //TODO: Fix - Hardcoded value
        if (other.tag == "Player")
        {
            canBuy = false;
            controller.Buy -= isBuying;
            mensages.text = $" ";
        }
    }

    private void isBuying(bool input)
    {
        this.input = input;
    }
}
