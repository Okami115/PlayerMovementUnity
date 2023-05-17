using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class BuyGuns : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Guns.Count; i++)
        {
            if (Guns[i].activeSelf)
            {
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
            controller.Buy += isBuying;
        }
    }
    private void OnTriggerExit(Collider other)
    {
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
