using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScifiClock : MonoBehaviour
{
    public float lavitationHeight;
    public float LavitationAmplitude;
    public float frequency;
    public CharacterControllerMine player;
    public float RewindTime = 3f;
    public float PlayerRewinderBarSpeed = 0.3f;
    public float RewindBarFillingAmount = 20f;
    float currentfilling = 0;

    float timer =0;
    float initialRewind;
    private void Start()
    {
        Vector3 pos = transform.position;
        pos.y = lavitationHeight;
        transform.position = pos;
    }
    // Update is called once per frame
    void Update()
    {
        Lavitate();
       
    }

    private void Lavitate()
    {
        float amplY = LavitationAmplitude * Mathf.Sin(frequency * Time.timeSinceLevelLoad);
        Vector3 pos = transform.position;
        pos.y += amplY;
        transform.position = pos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.transform.gameObject.CompareTag("Player"))
        {
            initialRewind = player.RewindTimeBar;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.transform.root.transform.gameObject.CompareTag("Player"))
        {
            
            timer += Time.deltaTime;
            if (timer < RewindTime)
            {
                //RewindingClock
                Debug.Log("RewindingClock");
            }
            else
            {

                currentfilling += Time.deltaTime * PlayerRewinderBarSpeed;
                if (player.RewindTimeBar < RewindBarFillingAmount+initialRewind && player.RewindTimeBar<=100)
                {
                   
                    
                        player.RewindTimeBar += currentfilling;
                   
                    
                }
            }
        }
    }
}
