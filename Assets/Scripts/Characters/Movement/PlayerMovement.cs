using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform Pivot;
    [SerializeField] private Rigidbody _rigidBody;

    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float sprintSpeed = 30f;
    [SerializeField] private bool isSprinting = false;
    private Vector2 inputVec2;
    private Vector3 direction;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool readyToJump = true;

    [Header("Ground variables")]
    [SerializeField] private float playerheight;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float groundDrag;
    [SerializeField] private float minDistanceGround = 0.1f;
    [SerializeField] private bool isGrounded;

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
        speedControl();
    }

    public void OnMove(InputValue input)
    {
        inputVec2 = input.Get<Vector2>();
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
        Debug.Log($"SI.");
    }

    private void MovePlayer()
    {
        direction = orientation.forward * inputVec2.y + orientation.right * inputVec2.x;

        _rigidBody.AddForce(direction.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    private void JumpPlayer()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(Pivot.position, Vector3.down, out hit, ground);
        if (isGrounded) { _rigidBody.drag = groundDrag; }
        else { _rigidBody.drag = 0; }

        if (isJumping && isGrounded && hit.distance <= minDistanceGround)
        {
            _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }
    }
    private void speedControl()
    {
        if (isGrounded && isSprinting)
        {
            moveSpeed = sprintSpeed;
            isSprinting = false;
        }
        else if (isGrounded)
        {
            moveSpeed = walkSpeed;
        }

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
}
