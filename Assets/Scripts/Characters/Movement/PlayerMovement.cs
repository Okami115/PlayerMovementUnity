using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform Pivot;
    [SerializeField] private Rigidbody _rigidBody;

    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float sprintSpeed = 30f;
    [SerializeField] private bool isSprinting = false;
    private RaycastHit hit;
    private Vector2 inputVec2;
    private Vector3 direction;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier = 10;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool readyToJump = true;

    [Header("Ground variables")]
    [SerializeField] private float playerheight;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask stairs;
    [SerializeField] private float groundDrag = 5;
    [SerializeField] private float maxDistanceGround = 0.5f;
    [SerializeField] private bool isGrounded;

    public event EventHandler IsWalking;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        JumpPlayer();
    }

    private void Update()
    {
        transform.rotation = orientation.rotation;
        speedControl();
    }

    public void OnMove(InputValue input)
    {
        inputVec2 = input.Get<Vector2>();
        IsWalking?.Invoke(this, EventArgs.Empty);
    }
    public void OnJump(InputValue input) 
    {
        isJumping = input.isPressed;
        CancelInvoke(nameof(jumpReset));
        Invoke(nameof(jumpReset), jumpCooldown); 
    }

    public void OnSprint(InputValue input)
    {
        isSprinting = input.isPressed;
    }

    private void MovePlayer()
    {
        direction = orientation.forward * inputVec2.y + orientation.right * inputVec2.x;
        if(Vector3.Project(direction, hit.normal) != Vector3.zero)
        {
            direction -=Vector3.Project(direction.normalized, hit.normal);
        }
        _rigidBody.AddForce(direction.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    private void JumpPlayer()
    {
        
        isGrounded = IsFloored(out hit, ground);
        if (isGrounded)
        {
            _rigidBody.drag = groundDrag;

        }
        else
        {
            _rigidBody.AddForce(Vector3.down * airMultiplier, ForceMode.Force);
            _rigidBody.drag = 0;
        }

        if (isJumping && isGrounded && hit.distance <= maxDistanceGround)
        {
            _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }


    }

    private bool IsFloored(out RaycastHit hit, int layerMask)
    {
        return Physics.Raycast(Pivot.position, Vector3.down, out hit, maxDistanceGround, layerMask);
    }

    private void speedControl()
    {
        moveSpeed = isSprinting ? sprintSpeed : walkSpeed;

        Vector3 flatVel = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limit = flatVel.normalized * moveSpeed;
            _rigidBody.velocity = new Vector3(limit.x, _rigidBody.velocity.y, limit.z);
        }

    }
    private void jumpReset()
    {
        isJumping = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(Pivot.position, Vector3.down * maxDistanceGround);
        Gizmos.DrawRay(Pivot.position, direction.normalized);
    }
}
