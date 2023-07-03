using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGuns : BuyObjets
{
    [SerializeField] private List<GameObject> Guns;
    [SerializeField] private GameObject GunForSell;
    private bool hasGun;

    protected override IEnumerator Canbuy()
    {

        for (int i = 0; i < Guns.Count; i++)
        {
            if (Guns[i].activeSelf)
            {
                hasGun = Guns[i] == GunForSell;
            }
        }

        if (!hasGun && input && gameManager.Credits >= Price)
        {

            for (int i = 0; i < Guns.Count; i++)
            {
                Guns[i].SetActive(false);
            }

            GunForSell.SetActive(true);

            Sell(Price);
        }

        yield return null;
    }
}
