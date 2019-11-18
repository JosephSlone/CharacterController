using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // For Debugging
    public float currentSpeed = 0;

    [SerializeField] private float speed = 12;
    [SerializeField] private float braking = -1;
    [SerializeField] private float rotationSpeed = 15;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float dashMultiplier = 1.5f;
    [SerializeField] private float jumpStrength = 5f;
    [SerializeField] private LayerMask groundLayerMask = 0;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    
    private CharacterController controller;
    private Animator animator;
    private new Camera camera;

    private Vector3 velocity;
    private bool isGrounded;
    private bool jump = false;
    private Vector3 rotation;
    private float verticalVelocity = 0;
    private float boost = 1f;
    private float x, z;

    private Vector3 move = Vector3.zero;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        camera = Camera.main;
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        currentSpeed = controller.velocity.magnitude;

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }


    }

    private void FixedUpdate()
    {
        Movement();
        CharacterAnimation();
    }

    private void LateUpdate()
    {
        
    }



    private void CharacterAnimation()
    {
        animator.SetFloat("Forward", currentSpeed);
    }

    private void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Vector3 right = camera.transform.right;
        Vector3 forward = Vector3.Cross(right, Vector3.up);        

        if (isGrounded)
        {
            move = forward * z + right * x;
            verticalVelocity = -2f;

            if (Input.GetAxis("Left Trigger") > 0.5f)
            {
                boost = dashMultiplier;
            }
            else
            {
                boost = 1;
            }

            if (jump)
            {
                verticalVelocity += jumpStrength;
                if(currentSpeed > 0.1)
                {
                    Debug.Log("Running Jump!");
                    animator.SetTrigger("RunningJump");
                }
            }

            if (currentSpeed > 0.5f)
            {
                Vector3 targetVector = controller.velocity.normalized;

                targetVector.y = 0;
                if (targetVector != Vector3.zero)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(targetVector);
                    transform.rotation = Quaternion.Lerp(transform.rotation,
                        desiredRotation, Time.deltaTime * rotationSpeed);
                }
            }

        }

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        controller.Move(move * speed * boost * Time.deltaTime);
        jump = false;

        Debug.DrawRay(transform.position, transform.forward, Color.black);
        Debug.DrawRay(transform.position, controller.velocity.normalized, Color.blue);
    }
}
