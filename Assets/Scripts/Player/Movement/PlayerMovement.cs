using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem.HID;


/// <summary>
/// Contains the variables and functions of the player's movement
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform pivot;
    [SerializeField] private Rigidbody rigidBody;


    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float sprintSpeed = 30f;
    [SerializeField] public float SpeedConstantMultiplier = 10f;
    [SerializeField] private float speedMultiplier;
    private bool isSprinting = false;
    public bool IsSprinting { get => isSprinting; set => isSprinting = value; }
    public float SpeedMultiplier { get => speedMultiplier; set => speedMultiplier = value; }
    
    private RaycastHit hit;
    private Vector2 moveInputVec2;
    public Vector2 MoveInputVec2 { get => moveInputVec2; set => moveInputVec2 = value; }

    private Vector3 direction;


    [Header("Jump Variables")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float airDrag = 10;
    [SerializeField] private float jumpCooldown;
    public float JumpCooldown { get => jumpCooldown; set => jumpCooldown = value; }
    
    private bool isJumping;
    public bool IsJumping { get => isJumping; set => isJumping = value; }

    [Header("Ground variables")]
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask stairs;
    [SerializeField] private float groundDrag = 5;
    [SerializeField] private float maxDistanceGround = 0.5f;
    [SerializeField] private bool isGrounded;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        speedMultiplier = SpeedConstantMultiplier;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        JumpPlayer();
        LimitMovementSpeed();
    }

    private void Update()
    {
        transform.rotation = orientation.rotation;
    }

    /// <summary>
    /// Update player position based on inputs
    /// </summary>
    private void MovePlayer()
    {
        direction = orientation.forward * MoveInputVec2.y + orientation.right * MoveInputVec2.x;
        if (Vector3.Project(direction, hit.normal) != Vector3.zero)
        {
            direction -= Vector3.Project(direction.normalized, hit.normal);
        }
        rigidBody.AddForce(SpeedMultiplier * moveSpeed * direction.normalized, ForceMode.Force);
    }

    /// <summary>
    /// Manage jump status
    /// </summary>
    private void JumpPlayer()
    {

        isGrounded = IsFloored(out hit, ground);
        if (isGrounded)
        {
            rigidBody.drag = groundDrag;

        }
        else
        {
            rigidBody.AddForce(Vector3.down * airDrag, ForceMode.Force);
            rigidBody.drag = 0;
        }

        if (IsJumping && isGrounded && hit.distance <= maxDistanceGround)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsJumping = false;
        }


    }

    /// <summary>
    /// Check if the player is on the ground
    /// </summary>
    /// <param name="hit"></param>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    private bool IsFloored(out RaycastHit hit, int layerMask)
    {
        return Physics.Raycast(pivot.position, Vector3.down, out hit, maxDistanceGround, layerMask);
    }

    /// <summary>
    /// Limit the maximum speed of the player
    /// </summary>
    private void LimitMovementSpeed()
    {
        moveSpeed = IsSprinting ? sprintSpeed : walkSpeed;

        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limit = flatVel.normalized * moveSpeed;
            rigidBody.velocity = new Vector3(limit.x, rigidBody.velocity.y, limit.z);
        }

    }

}
