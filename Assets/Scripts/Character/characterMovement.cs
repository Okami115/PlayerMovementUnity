using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterMovement : MonoBehaviour
{
    [SerializeField] private Transform feetPivot;

    [SerializeField] private float speed = 10;
    [SerializeField] private float forceJump = 10;
    [SerializeField]private float minJumpDistance = 0.1f;
    private const float maxDistance = 10;

    private bool sprint = false;
    private bool isJummping = false;
    private RaycastHit hit;

    private Vector3 _currentMovement;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); 
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(feetPivot.position, new Vector3 (feetPivot.position.x, feetPivot.position.y + minJumpDistance, feetPivot.position.z));
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = _currentMovement * speed + Vector3.up * _rigidbody.velocity.y;
        if (isJummping && Physics.Raycast(feetPivot.position, Vector3.down, out hit, maxDistance) && hit.distance <= minJumpDistance)
        {
            _rigidbody.AddForce(transform.up * forceJump, ForceMode.Impulse);
            isJummping = false;
        }
    }

    private void Update()
    {
        if(sprint)
        {
            speed = 30;
        }
        else
        {
            speed = 10;
        }

        //transform.Translate(speed * Time.deltaTime * _currentMovement);

    }
    public void OnMove(InputValue input)
    {
        var movement = input.Get<Vector2>();
        _currentMovement = new Vector3(movement.x, _currentMovement.y, movement.y);
    }

    public void OnJump(InputValue input)
    {
        isJummping = input.isPressed;
    }

    public void OnSprint(InputValue input)
    {
        sprint = input.isPressed;
    }
    
}
