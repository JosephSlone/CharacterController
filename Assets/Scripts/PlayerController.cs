using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float rotationSpeed = 15;
    [SerializeField] public float gravity = 20;
    [SerializeField] private LayerMask groundLayerMask = 0;
    [SerializeField] private float maxDistanceToGround = 2f;
    [SerializeField] private Vector3 moveDir = Vector3.zero;

    private CharacterController controller;
    private Animator animator;
    private new Camera camera;
    private Vector3 movement;
    private Vector3 worldPosition = Vector3.zero;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        camera = Camera.main;
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            movement = worldPosition;

            Vector3 right = camera.transform.right;
            Vector3 forward = Vector3.Cross(right, Vector3.up);

            movement += right * (Input.GetAxis("Horizontal"));
            movement += forward * (Input.GetAxis("Vertical"));
            movement *= speed;

            if (controller.velocity.magnitude > 0)
            {
                Vector3 velocity = controller.velocity;
                velocity.y = 0;
                Quaternion rotation = Quaternion.LookRotation(velocity);

                controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation,
                    rotation, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetButton("Jump"))
            {
                movement.y = jumpSpeed;
            }
        }
        
        movement.y -= gravity * Time.deltaTime;
        controller.Move(movement * Time.deltaTime);
    }  
     
    public void SetPosition(Vector3 position)
    {
        controller.Move(position);
    }

    private bool IsGrounded()
    {
        Vector3 direction = Vector3.down;

        Debug.DrawRay(transform.position, direction * maxDistanceToGround, Color.green);

        if (Physics.Raycast(transform.position, direction, 
            out RaycastHit hit, maxDistanceToGround, groundLayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
     
}
