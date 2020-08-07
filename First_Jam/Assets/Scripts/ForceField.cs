using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
   [SerializeField] CharacterControllerMine me;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider botTriggerCollider)
    {
        if (botTriggerCollider.transform.gameObject.CompareTag("Crowd") && !me.IsRewinding)
        {
            Debug.Log("HITTTTTING FFFFFFOOOOORRRCCCEEFIEKD");
            AudioManager.instance.PlayShieldErrorSound(transform.position);
            me.IsHittingWithBots = true;
           transform.rotation = Quaternion.LookRotation((transform.position - botTriggerCollider.transform.position));
            me.fieldStrength -= 10f;
        }
    }
    private void OnTriggerStay(Collider botTriggerCollider)
    {
        if (botTriggerCollider.transform.gameObject.CompareTag("Crowd") && !me.IsRewinding)
        {
            me.IsHittingWithBots = true;
            transform.rotation = Quaternion.LookRotation((transform.position - botTriggerCollider.transform.position));
            me.fieldStrength -= 0.5f;
        }
    }

    private void OnTriggerExit(Collider botTriggerCollider)
    {
        if (botTriggerCollider.transform.gameObject.CompareTag("Crowd") && !me.IsRewinding)
        {
            AudioManager.instance.StopShieldErrorSound();
            me.IsHittingWithBots = false;
            transform.rotation = Quaternion.LookRotation((transform.position - botTriggerCollider.transform.position));
            
        }
    }
}
