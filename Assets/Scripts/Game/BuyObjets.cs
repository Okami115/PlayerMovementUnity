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
    [SerializeField] private string playerName = "Player";
    protected bool input;

    protected virtual IEnumerator Canbuy()
    {
        mensages.text = $"press E to buy ({price})";
        Debug.Log(input);
        if (input && gameManager.Credits >= price)
        {
            soundManager.PlaySound(buySound);
            Destroy(this.gameObject);
            gameManager.Credits -= price;
            mensages.text = $" ";
        }

        yield return null;
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag(playerName);
        PlayerController = Player.GetComponent<PlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == playerName)
        {
            StartCoroutine(Canbuy());
            PlayerController.Buy += isBuying;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerName)
        {
            StopCoroutine(Canbuy());
            PlayerController.Buy -= isBuying;
            mensages.text = $" ";
        }
    }

    private void isBuying(bool input)
    {
        this.input = input;
    }
}
