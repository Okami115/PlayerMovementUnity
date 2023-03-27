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
    private RaycastHit hit;

    private Vector3 _currentMovement;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); 
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

        transform.Translate(speed * Time.deltaTime * _currentMovement);

    }
    public void OnMove(InputValue input)
    {
        var movement = input.Get<Vector2>();
        _currentMovement = new Vector3(movement.x, _currentMovement.y, movement.y);
    }

    public void OnJump()
    {
        if (Physics.Raycast(feetPivot.position, Vector3.down, out hit, maxDistance) && hit.distance <= minJumpDistance)
        {
            Debug.Log("Jump!");
            _rigidbody.AddForce(transform.up * forceJump, ForceMode.Impulse);
        }
    }

    public void OnSprint(InputValue input)
    {
        sprint = input.isPressed;
    }
    
}
