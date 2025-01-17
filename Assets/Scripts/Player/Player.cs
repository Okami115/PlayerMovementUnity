using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/// <summary>
/// Contains the player logic
/// </summary>
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private string Enemy = "Enemy";
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private TextMeshProUGUI healtPoints;
    [SerializeField] private AudioClip hit;
    [SerializeField] private Health health;
    [SerializeField] private float timeToHeal;

    private bool godMode;

    public Health Health { get => health; set => health = value; }
    public bool GodMode { get => godMode; set => godMode = value; }

    /// <summary>
    /// Start player healing
    /// </summary>
    /// <returns></returns>
    private IEnumerator Healing()
    {
        yield return new WaitForSeconds(timeToHeal);

        Health.RestartHP();
    }

    private void Start()
    {
        Health = GetComponent<Health>();
        godMode = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == Enemy && !godMode)
        {
            soundManager.PlaySound(hit);
            Health.TakeDamage(1);
            StartCoroutine(Healing());
        }
    }
}
