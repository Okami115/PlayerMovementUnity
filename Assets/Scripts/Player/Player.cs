using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;
    [SerializeField] TextMeshProUGUI healtPoints;
    [SerializeField] private AudioClip hit;
    [SerializeField] private int maxHealt;
    [SerializeField] private int healt;
    [SerializeField] private int timeToHealt;
    private bool timerOn;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        healt = maxHealt;
    }

    // Update is called once per frame
    void Update()
    {

        //TODO: TP2 - SOLID
        healtPoints.text = healt.ToString();

        //TODO: Fix - Could be a coroutine
        if(timerOn)
        {
            timer += Time.deltaTime;

            if(timer >= timeToHealt)
            {
                timer = 0;
                timerOn = false;
                healt = maxHealt;
            }
        }

        if(healt <= 0) 
        {
            //TODO: TP2 - SOLID
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO: Fix - Hardcoded value
        if(collision.gameObject.tag == "Enemy")
        {
            soundManager.PlaySound(hit);
            healt--;
            timerOn = true;
        }
    }
}
