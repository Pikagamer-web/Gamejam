using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool IsGrounded = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.root.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }
}
