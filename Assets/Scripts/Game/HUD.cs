using TMPro;
using UnityEngine;

/// <summary>
/// Control HUD texts
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private Player player;
    [SerializeField] private ShootRaycast m45A1;
    [SerializeField] private ShootRaycast ppsh;
    [SerializeField] private Shoot m48;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject panel;

    [SerializeField] private TextMeshProUGUI messages;
    [SerializeField] private BuyObjets[] buyObjets;
    [SerializeField] private BuyGuns[] buyGuns;

    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI healtPoints;
    [SerializeField] private TextMeshProUGUI maxBullets;
    [SerializeField] private TextMeshProUGUI currentBullets;


    void Start()
    {
        player = FindAnyObjectByType<Player>();
        controller = FindAnyObjectByType<PlayerController>();

        buyObjets = FindObjectsOfType<BuyObjets>();
        buyGuns = FindObjectsOfType<BuyGuns>();

        for (int i = 0; i < buyObjets.Length; i++)
        {
            buyObjets[i].sell += BuyObjet;
            buyObjets[i].onCustomerExit += ExitToBuyZone;
            buyObjets[i].onCustomerEnter += EnterToBuyZone;
        }

        for (int i = 0; i < buyGuns.Length; i++)
        {
            buyGuns[i].sell += BuyObjet;
            buyGuns[i].onCustomerExit += ExitToBuyZone;
            buyGuns[i].onCustomerEnter += EnterToBuyZone;
        }

        panel.SetActive(false);
        controller.Paused += Pause;
    }

    private void Update()
    {
        pointsText.text = gameManager.Credits.ToString();
        healtPoints.text = player.Health.HPoints.ToString();

        ShowCurrentBullets();
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        panel?.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Continuos the game.
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        panel?.SetActive(false);
    }

    /// <summary>
    /// Clear the text when the player buy the objet.
    /// </summary>
    private void BuyObjet()
    {
        messages.text = " ";
    }

    /// <summary>
    /// Show the text when the player enter the buy zone
    /// </summary>
    private void EnterToBuyZone(BuyObjets buyObjets)
    {
        messages.text = $"press E to buy ({buyObjets.Price})";
    }

    /// <summary>
    /// Clear the text when the player leaves the buy zone
    /// </summary>
    private void ExitToBuyZone(BuyObjets buyObjets)
    {
        messages.text = " ";
    }

    /// <summary>
    /// Show current bullets depending on the current weapon.
    /// </summary>
    private void ShowCurrentBullets()
    {
        if(m45A1.gameObject.activeSelf) 
        { 
            maxBullets.text = m45A1.MaxBulletsInMagazine.ToString();
            currentBullets.text = m45A1.BulletsInMagazine.ToString();
        }
        else if (ppsh.gameObject.activeSelf)
        {
            maxBullets.text = ppsh.MaxBulletsInMagazine.ToString();
            currentBullets.text = ppsh.BulletsInMagazine.ToString();
        }
        else if (m48.gameObject.activeSelf)
        {
            maxBullets.text = m48.MaxBulletsInMagazine.ToString();
            currentBullets.text = m48.BulletsInMagazine.ToString();
        }
    }
}
