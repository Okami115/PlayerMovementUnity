using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play shopping sounds
/// </summary>
public class ShopSFX : MonoBehaviour
{
    [SerializeField] protected SoundManager soundManager;
    [SerializeField] private BuyObjets[] buyObjets;
    [SerializeField] private BuyGuns[] buyGuns;
    [SerializeField] protected AudioClip buySound;

    private void OnEnable()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
        buyObjets = FindObjectsOfType<BuyObjets>();
        buyGuns = FindObjectsOfType<BuyGuns>();

        for (int i = 0; i < buyObjets.Length; i++)
        {
            buyObjets[i].sell += playSFXSell;
        }

        for (int i = 0; i < buyGuns.Length; i++)
        {
            buyGuns[i].sell += playSFXSell;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < buyObjets.Length; i++)
        {
            buyObjets[i].sell -= playSFXSell;
        }

        for (int i = 0; i < buyGuns.Length; i++)
        {
            buyGuns[i].sell -= playSFXSell;
        }
    }

    private void playSFXSell()
    {
        soundManager.PlaySound(buySound);
    }
}
