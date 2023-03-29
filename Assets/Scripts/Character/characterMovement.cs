using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterMovement : MonoBehaviour
{

    [SerializeField] private float speed = 10;
    
    [SerializeField] private float forceJump = 10;
    [SerializeField] private float minJumpDistance = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float coyoteTime = 0.1f;
    private const float maxDistance = 10;

    private Vector3 coyotePivot;
    private bool sprint = false;
    private RaycastHit hit;

    [SerializeField] private Vector3 _currentMovement;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform feetPivot;
    [SerializeField] Coroutine _jumpCoroutine;

    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(feetPivot.position, new Vector3 (feetPivot.position.x, feetPivot.position.y + minJumpDistance, feetPivot.position.z));
    }

    private void Start()
    {
        if(!_rigidbody)
        {
            Debug.LogError("No Rigidbody");
        }

        if(!feetPivot)
        {
            Debug.LogWarning("No pivot");
        }
    }

    private void FixedUpdate()
    {

        
        _rigidbody.velocity = _currentMovement * speed + Vector3.up * _rigidbody.velocity.y;
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

    }
    public void OnMove(InputValue input)
    {
        var movement = input.Get<Vector2>();
        _currentMovement = new Vector3(movement.x, _currentMovement.y, movement.y);
    }

    public void OnJump(InputValue input)
    {
        //CancelInvoke(nameof(CancelJumpInput));
        //Invoke(nameof(CancelJumpInput), jumpBufferTime);

        if (_jumpCoroutine != null)
            StopCoroutine(jumpCoroutine(jumpBufferTime));

       _jumpCoroutine = StartCoroutine(jumpCoroutine(jumpBufferTime));
    }

    public void OnSprint(InputValue input)
    {
        sprint = input.isPressed;
    }

    private IEnumerator jumpCoroutine(float bufferTime)
    {

        if (!feetPivot)
            yield break;

        float timeElapsed = 0; ;

        while (timeElapsed <= bufferTime)
        {
            yield return new WaitForFixedUpdate();

            if (Physics.Raycast(feetPivot.position, Vector3.down, out hit, maxDistance) && hit.distance <= minJumpDistance)
            {
                _rigidbody.velocity = new Vector3 (_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
                _rigidbody.AddForce(transform.up * forceJump, ForceMode.Impulse);

                if(timeElapsed > 0)
                {
                    Debug.Log($"<color=grey>{name}: buffered jump for {timeElapsed} seconds </color>");
                }

                yield break;
            }

            timeElapsed += Time.fixedDeltaTime;
        }
    }
}
