using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

/// <summary>
/// Contains the actions based on the inputs
/// </summary>
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    
    public event Action<bool> Buy;
    public event Action<bool> Shoot;
    public event Action Reload;
    public event Action Moving;
    public event Action<Vector2> Looking;
    public event Action<Vector2> JoystickLook;
    public event Action Paused;

    public event Action NextLevel;
    public event Action GodMode;
    public event Action Flash;
    public event Action Nuke;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void OnMove(InputValue input)
    {
        playerMovement.MoveInputVec2 = input.Get<Vector2>();
        Moving?.Invoke();
    }
    public void OnJump(InputValue input) 
    {
        playerMovement.IsJumping = input.isPressed;
        CancelInvoke(nameof(ResetJumpState));
        Invoke(nameof(ResetJumpState), playerMovement.JumpCooldown); 
    }
    public void OnSprint(InputValue input)
    {
        playerMovement.IsSprinting = input.isPressed;
    }
    public void OnShoot(InputValue input)
    {
        Shoot?.Invoke(input.isPressed);
    }
    public void OnCamera(InputValue input)
    {
        Looking?.Invoke(input.Get<Vector2>());
    }

    public void OnNextLevel()
    {
        NextLevel?.Invoke();
    }
    public void OnGodMode()
    {
        GodMode?.Invoke();
    }
    public void OnFlash()
    {
        Flash?.Invoke();
    }
    public void OnNuke()
    {
        Nuke?.Invoke();
    }

    public void OnJoystickLook(InputValue input)
    {
        JoystickLook?.Invoke(input.Get<Vector2>());
    }
    public void OnReload()
    {
        Reload?.Invoke();
    }
    public void OnInteractive(InputValue input)
    {
        Buy?.Invoke(input.isPressed);
    }
    public void OnPause()
    {
        Paused?.Invoke();
    }

    /// <summary>
    /// reset jump state
    /// </summary>
    private void ResetJumpState()
    {
        playerMovement.IsJumping = false;
    }

}
