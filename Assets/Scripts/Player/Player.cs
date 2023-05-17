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

        healtPoints.text = healt.ToString();

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
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            soundManager.PlaySound(hit);
            healt--;
            timerOn = true;
        }
    }
}
