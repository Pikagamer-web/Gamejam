using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
   [SerializeField] CharacterControllerMine me;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider botTriggerCollider)
    {
        if (botTriggerCollider.transform.root.gameObject.CompareTag("Crowd"))
        {
            me.IsHittingWithBots = true;
           transform.rotation = Quaternion.LookRotation((transform.root.transform.position - botTriggerCollider.transform.root.transform.position));
            me.fieldStrength -= 10f;
        }
    }
    private void OnTriggerStay(Collider botTriggerCollider)
    {
        if (botTriggerCollider.transform.root.gameObject.CompareTag("Crowd"))
        {
            me.IsHittingWithBots = true;
            transform.rotation = Quaternion.LookRotation((transform.root.transform.position - botTriggerCollider.transform.root.transform.position));
            me.fieldStrength -= 0.5f;
        }
    }

    private void OnTriggerExit(Collider botTriggerCollider)
    {
        if (botTriggerCollider.transform.root.gameObject.CompareTag("Crowd"))
        {
            me.IsHittingWithBots = false;
            transform.rotation = Quaternion.LookRotation((transform.root.transform.position - botTriggerCollider.transform.root.transform.position));
            
        }
    }
}
