using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // For Debugging
    public float currentSpeed = 0;

    [SerializeField] private float speed = 12;
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
    private Vector3 rotation;
    private float verticalVelocity = 0;
    private float boost = 1f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        camera = Camera.main;
    }

    private void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        CharacterAnimation();
    }

    

    private void CharacterAnimation()
    {

        if (!isGrounded)
        {

        }

        if(controller.velocity.magnitude > 0.1)
        {
            animator.SetBool("Moving", true);

            currentSpeed = controller.velocity.magnitude;
            float normal = Mathf.InverseLerp(0, 6.8f, currentSpeed);
            currentSpeed = Mathf.Lerp(0, 1, normal);

            animator.SetFloat("Forward", currentSpeed);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    private void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 right = camera.transform.right;
        Vector3 forward = Vector3.Cross(right, Vector3.up);
        Vector3 move = right * x + forward * z;
        if (isGrounded)
        {
            verticalVelocity = -2f;

            if (Input.GetAxis("Left Trigger") > 0.5f)
            {
                boost = dashMultiplier;
            }
            else
            {
                boost = 1;
            }

            if (Input.GetButton("Jump"))
            {
                verticalVelocity += jumpStrength;
            }
        }


        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;
        controller.Move(move * speed * boost * Time.deltaTime);

        if (controller.velocity.magnitude > 0.5f)
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

        Debug.DrawRay(transform.position, transform.forward, Color.black);
        Debug.DrawRay(transform.position, controller.velocity.normalized, Color.blue);
    }
}
