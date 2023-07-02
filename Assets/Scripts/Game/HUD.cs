using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private Player player;
    [SerializeField] private ShootRaycast m45A1;
    [SerializeField] private ShootRaycast ppsh;
    [SerializeField] private Shoot m48;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject panel;

    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI healtPoints;
    [SerializeField] private TextMeshProUGUI maxBullets;
    [SerializeField] private TextMeshProUGUI currentBullets;


    void Start()
    {
        player = FindAnyObjectByType<Player>();
        controller = FindAnyObjectByType<PlayerController>();

        panel.SetActive(false);
        controller.Paused += Pause;
    }

    private void Update()
    {
        pointsText.text = gameManager.Credits.ToString();
        healtPoints.text = player.Health.HPoints.ToString();

        ShowCurrentBullets();
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        panel?.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        panel?.SetActive(false);
    }

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
