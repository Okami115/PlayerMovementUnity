using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BuyObjets : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] protected PlayerController PlayerController;
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected GameObject Player;
    [SerializeField] private string playerName = "Player";
    protected bool input;

    public int Price { get => price; set => price = value; }

    public event Action sell;
    public event Action<BuyObjets> onCustomerExit;
    public event Action<BuyObjets> onCustomerEnter;

    protected virtual IEnumerator Canbuy()
    {
        
        if (input && gameManager.Credits >= Price)
        {
            Destroy(this.gameObject);
            Sell(Price);
        }

        yield return null;
    }

    protected virtual void Sell(int price)
    {
        gameManager.Credits -= price;
        sell?.Invoke();
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
            onCustomerEnter.Invoke(this);
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
            onCustomerExit.Invoke(this);
        }
    }

    private void isBuying(bool input)
    {
        this.input = input;
    }
}
