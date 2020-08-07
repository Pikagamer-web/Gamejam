using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LevelSpecific

public class Bomber : MonoBehaviour
{
    [SerializeField] Transform BombingSite;
    public GameObject BombingObjects;
    public int totalBombingObjects;
    public GameObject currentBombingObject;
    public bool registerTrigger = true;
    [SerializeField] CharacterControllerMine player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerMine>();
      
        currentBombingObject = BombingObjects;
        BombingObjects.SetActive(false);
       
    }
    private void Update()
    {
        if(player.transform.position.z> transform.position.z && registerTrigger)
        {
            currentBombingObject.SetActive(true);
            Rigidbody rb = currentBombingObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            Vector3 pos = BombingSite.position;
            pos.y = 60f;
            currentBombingObject.transform.position = pos;
            rb.isKinematic = false;
            registerTrigger = false;
        }
    }
   /* private void OnTriggerEnter(Collider other)
    {
        var g = other.transform.root.gameObject;
        if (g.CompareTag("Player") && registerTrigger)
        {
            currentBombingObject.SetActive(true);
            Rigidbody rb = currentBombingObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            Vector3 pos = BombingSite.position;
            pos.y = 60f;
            currentBombingObject.transform.position = pos;
            rb.isKinematic = false;
            registerTrigger = false;
        }
    }*/
}
