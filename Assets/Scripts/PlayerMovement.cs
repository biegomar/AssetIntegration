using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int speed;
    [SerializeField] private int rotationSpeed;
    
    //new input system
    private Vector2 moveInputValue = Vector2.zero;
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Attack01 = Animator.StringToHash("Attack01");
    private static readonly int Attack02 = Animator.StringToHash("Attack02");
    private static readonly int Attack03 = Animator.StringToHash("Attack03");
    private static readonly int Defend = Animator.StringToHash("Defend");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Die = Animator.StringToHash("Die");
    
    void Start()
    {
        
    }
    
    void Update()
    {
        CalculateNewPosition();
    }
    
    private void CalculateNewPosition()
    {
        var movementDirection = new Vector3(
            CalculateNewXPosition(),
            0,
            CalculateNewZPosition());
        
        movementDirection.Normalize();

        var rayCastHit = Physics.Raycast(transform.position, movementDirection, 1f);
        DrawRayCast(movementDirection, rayCastHit);

        if (!rayCastHit)
        {
            transform.Translate(movementDirection * (this.speed * Time.deltaTime), Space.World);

            if (movementDirection != Vector3.zero)
            {
                var toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, toRotation, this.rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            Debug.Log("RayCastHit!");
        }
    }

    private float CalculateNewXPosition()
    {       
        return this.moveInputValue.x;
    }
        
    private float CalculateNewZPosition()
    {       
        return this.moveInputValue.y;
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            this.moveInputValue = context.ReadValue<Vector2>();
            this.animator.SetBool(Walk, true);
        }

        if (context.canceled)
        {
            this.moveInputValue = Vector2.zero;
            this.animator.SetBool(Walk, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider");
    }

    private void DrawRayCast(Vector3 moveDirection, bool isRayCastHit)
    {
        Debug.DrawRay(transform.position, 10 * moveDirection, isRayCastHit ? Color.red : Color.blue);
    }
}
