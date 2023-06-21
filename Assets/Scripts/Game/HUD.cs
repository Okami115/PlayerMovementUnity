using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private GameObject panel; 


    void Start()
    {
        panel.SetActive(false);
        controller.Paused += Pause;
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
}
