using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
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
            healt--;
            timerOn = true;
        }
    }
}
