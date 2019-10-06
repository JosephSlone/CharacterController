using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildEnterExit : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Platform")
        {
            transform.SetParent(other.transform, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
