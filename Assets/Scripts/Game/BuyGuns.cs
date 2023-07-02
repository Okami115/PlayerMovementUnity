using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGuns : BuyObjets
{

    //TODO: TP2 - SOLID
    [SerializeField] private List<GameObject> Guns;
    [SerializeField] private GameObject GunForSell;
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

        if (!hasGun && input && gameManager.Credits >= price)
        {
            soundManager.PlaySound(buySound);

            for (int i = 0; i < Guns.Count; i++)
            {
                Guns[i].SetActive(false);
            }

            GunForSell.SetActive(true);

            gameManager.Credits -= price;
            mensages.text = $" ";
        }
        else if (hasGun) 
        {
            mensages.text = $"you already own this weapon";
        }

        yield return null;
    }
}
