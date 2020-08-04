using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScifiClock : MonoBehaviour
{
    public float lavitationHeight;
    public float LavitationAmplitude;
    public float frequency;

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

    
}
