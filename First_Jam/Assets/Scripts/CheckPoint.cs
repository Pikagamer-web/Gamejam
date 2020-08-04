using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Vector3 position;
    public float c_FieldStrength;
    public float c_RewindTimeBar;
    public Vector3 playerPos;
    bool checkPointRegistered = false;
    int i = 0;
    CharacterControllerMine player;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
       mat = GetComponent<Renderer>().material;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerMine>();
        position = transform.position;
    }
    private void Update()
    {
      
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.CompareTag("Player") && !checkPointRegistered)
        {
            player.lastCheckpoint = this;
            c_FieldStrength = player.fieldStrength;
            c_RewindTimeBar = player.RewindTimeBar;
            playerPos = player.transform.position;
            checkPointRegistered = true;
            
        }
    }
}
