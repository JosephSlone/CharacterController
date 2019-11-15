using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float travelTime;

    private Rigidbody rb;
    private Vector3 currentPos;
    private Transform parent;

    CharacterController controller;
    PlayerController player;
    float gravity;

    public Vector3 rb_velocity;
    public Vector3 cc_velocity;
    public Vector3 cc_position;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        parent = transform;
    }

    private void FixedUpdate()
    {
        currentPos = Vector3.Lerp(startPoint.position, endPoint.position,
            Mathf.Cos(Time.time / travelTime * Mathf.PI * 2) * -0.5f + 0.5f);
        rb.MovePosition(currentPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger");
 
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited Trigger");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Staying in  Trigger");
        if (other.tag == "Player")
        {
            rb_velocity = rb.velocity;
            rb_velocity.y = 0;
            cc_position = controller.transform.position;
            cc_velocity = controller.velocity.normalized;
            cc_velocity.y = -1;
            controller.SimpleMove(rb_velocity);
            controller.Move(cc_velocity);               
        }
    }
}
