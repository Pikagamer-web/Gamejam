using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTarget : MonoBehaviour
{
    [SerializeField] CharacterControllerMine player;
    [SerializeField] Camera cam;
    public bool ShowRewindTarget = false;
    [SerializeField] SwitchDeactivator sd;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerMine>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if(Physics.SphereCast(ray, 0.5f, out RaycastHit hitInfo, 1000f))
        {
            Debug.Log("Hit" + hitInfo.collider.gameObject.name);
            
            if (hitInfo.transform.gameObject.CompareTag("RewindableObject"))
            {
                Debug.Log("FOUNNNDD SUSTBIINNNNNN");
                player.currentREwindTarget = hitInfo.collider.gameObject.GetComponent<RewindableObject>();
                ShowRewindTarget = true;
            }
            else
            {
                player.currentREwindTarget = null;
                ShowRewindTarget = false;
            }

            if (hitInfo.collider.transform.gameObject.CompareTag("Switch"))
            {
                sd.CanDeactiveSwitch = true;
            }
            else
            {
                sd.CanDeactiveSwitch = false;
            }
        }

      
    }
}
