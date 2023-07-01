using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private string Enemy = "Enemy";
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private TextMeshProUGUI healtPoints;
    [SerializeField] private AudioClip hit;
    [SerializeField] private Health health;
    [SerializeField]private float timeToHeal;

    private IEnumerator Healing()
    {
        yield return new WaitForSeconds(timeToHeal);

        health.RestartHP();
    }

    private void Start()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        //TODO: TP2 - SOLID
        healtPoints.text = health.HPoints.ToString();

        if(health.HPoints <= 0) 
        {
            //TODO: TP2 - SOLID
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == Enemy)
        {
            soundManager.PlaySound(hit);
            health.TakeDamage(1);
            StartCoroutine(Healing());
        }
    }
}
