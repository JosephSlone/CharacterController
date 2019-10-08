using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBehaviour : MonoBehaviour
{
    [SerializeField] private float pushForce = 10.0f;

    private Rigidbody rb;
    private CharacterController cc;
    private bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(isColliding)
        {
            cc.Move(rb.velocity * pushForce * Time.fixedDeltaTime);
            isColliding = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collided with player");
            cc = other.GetComponent<CharacterController>();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isColliding = true;
        }

    }
}
