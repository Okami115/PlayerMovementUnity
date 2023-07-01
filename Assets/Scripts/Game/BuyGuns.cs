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

    protected override IEnumerator Canbuy()
    {
        mensages.text = $"press E to buy ({price})";

        for (int i = 0; i < Guns.Count; i++)
        {
            if (Guns[i].activeSelf)
            {
                hasGun = Guns[i] == GunForSell;
            }
        }

        if (!hasGun && input && gameManager.GetPoints() >= price)
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
        else if (hasGun) 
        {
            mensages.text = $"you already own this weapon";
        }

        yield return null;
    }
}
