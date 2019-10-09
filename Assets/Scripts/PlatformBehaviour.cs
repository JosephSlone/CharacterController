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
        if(other.tag == "StickyCube")
        {
            other.transform.parent = parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited Trigger");
        if(other.tag == "StickyCube")
        {
            other.transform.parent = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Staying in  Trigger");
        if (other.tag == "StickyCube")
        {
        }
    }
}
