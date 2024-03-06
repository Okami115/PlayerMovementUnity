using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play the sounds corresponding to each weapon
/// </summary>
public class ShootSFX : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;

    [SerializeField] private Shoot m48;
    [SerializeField] private AudioClip m48Shoot;
    [SerializeField] private AudioClip m48Reload;

    [SerializeField] private ShootRaycast m45a1;
    [SerializeField] private AudioClip m45a1Shoot;
    [SerializeField] private AudioClip m45a1Reload;

    [SerializeField] private ShootRaycast ppsh;
    [SerializeField] private AudioClip ppshShoot;
    [SerializeField] private AudioClip ppshReload;


    private void OnEnable()
    {
        soundManager = FindAnyObjectByType<SoundManager>();

        m48.relaod += m48ReloadSound;
        m48.shoot += m48ShootSound;

        m45a1.relaod += m45a1ReloadSound;
        m45a1.shoot += m45a1ShootSound;

        ppsh.relaod += ppshReloadSound;
        ppsh.shoot += ppshShootSound;
    }

    private void OnDisable()
    {
        m48.relaod -= m48ReloadSound;
        m48.shoot -= m48ShootSound;

        m45a1.relaod -= m45a1ReloadSound;
        m45a1.shoot -= m45a1ShootSound;

        ppsh.relaod -= ppshReloadSound;
        ppsh.shoot -= ppshShootSound;
    }

    private void m48ReloadSound()
    {
        soundManager.PlaySound(m48Reload);
    }

    private void m48ShootSound()
    {
        soundManager.PlaySound(m48Shoot);
    }

    private void m45a1ReloadSound()
    {
        soundManager.PlaySound(m45a1Reload);
    }

    private void m45a1ShootSound()
    {
        soundManager.PlaySound(m45a1Shoot);
    }

    private void ppshReloadSound()
    {
        soundManager.PlaySound(ppshReload);
    }

    private void ppshShootSound()
    {
        soundManager.PlaySound(ppshShoot);
    }
}
