using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float rotationSpeed = 20.0f;

    private Vector3 velocity;

    private Rigidbody rb;
    private Vector3 movement;
    private new Camera camera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
    }

    void Update()
    {
        Vector3 right = camera.transform.right;
        Vector3 forward = Vector3.Cross(right, Vector3.up);

        movement += right * (Input.GetAxis("Horizontal"));
        movement += forward * (Input.GetAxis("Vertical"));
        movement.y = 0;
        movement *= speed;

    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    private void moveCharacter(Vector3 input)
    {
        velocity = rb.velocity;
        velocity.y = 0;


        rb.MovePosition(transform.position + (input * Time.deltaTime));
    }
}
