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

    CharacterController controller;
    float gravity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        currentPos = Vector3.Lerp(startPoint.position, endPoint.position,
            Mathf.Cos(Time.time / travelTime * Mathf.PI * 2) * -0.5f + 0.5f);
        rb.MovePosition(currentPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            controller = other.GetComponent<CharacterController>();
            other.transform.parent = transform.parent.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.transform.localPosition);
        }
    }
}
