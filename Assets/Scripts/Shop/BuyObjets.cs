using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

/// <summary>
/// Allows you to make an item be bought
/// </summary>
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

    /// <summary>
    /// Allows you to buy an item
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Canbuy()
    {
        
        if (input && gameManager.Credits >= Price)
        {
            Destroy(this.gameObject);
            Sell(Price);
        }

        yield return null;
    }

    /// <summary>
    /// Subtract the credits of the player
    /// </summary>
    /// <param name="price"></param>
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

    /// <summary>
    /// Read the input of the interaction button
    /// </summary>
    /// <param name="input"></param>
    private void isBuying(bool input)
    {
        this.input = input;
    }
}
