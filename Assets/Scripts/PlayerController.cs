using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] private float rotationSpeed = 80;
    [SerializeField] private float gravity = 8;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float maxDistanceToGround = 2f;


    private Vector3 moveDir = Vector3.zero;
    private CharacterController controller;
    private Animator animator;
    private float rotation = 0f;

    private float vAxis = 0f;
    private float hAxis = 0f;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetBool("Moving", true);
    }

    private void Update()
    {
        //  Because we are not running backwards.
        vAxis = Mathf.Clamp(Input.GetAxis("Vertical"),-0.5f, 1.0f);
        hAxis = Input.GetAxis("Horizontal");

        Movement();
        MovementAnimation();
    }

    private void LateUpdate()
    {
        
    }

    private void Movement()
    {
        if (controller.isGrounded)
        {
            if (vAxis != 0)
            {
                moveDir = new Vector3(0, 0, vAxis);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
            }
            else
            {
                moveDir = Vector3.zero;
            }
        }

        rotation += hAxis * rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotation, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    private void jump()
    {

    }

    private void MovementAnimation()
    {
        if(controller.isGrounded)
        {
            animator.SetBool("Falling", false);
            if (vAxis != 0 || hAxis != 0)
            {
                animator.SetBool("Moving", true);
                animator.SetFloat("Forward", vAxis);
            }
            else
            {
                animator.SetBool("Moving", false);
            }
        }
        else
        {
            if(!IsGrounded())
            {                
                animator.SetBool("Moving", false);
                animator.SetBool("Falling", true);
            }
        }
    }

    private bool IsGrounded()
    {
        Vector3 direction = Vector3.down;

        Debug.DrawRay(transform.position, direction * maxDistanceToGround, Color.green);

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, maxDistanceToGround, groundLayerMask))
        {
            Debug.Log("Is Grounded");
            return true;
        }
        else
        {
            Debug.Log("Is Not Grounded");
            return false;
        }
    }
}
