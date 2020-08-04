using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGeneration : MonoBehaviour
{
    public Transform RoadSet1, RoadSet2, RoadSet3, RoadSet4;
    Vector3 instantiationPos = Vector3.zero;
    Transform currentInstantiatedTransform;
    // Start is called before the first frame update
    void Start()
    {
       // GenerateEnvt();
    }

    private void GenerateEnvt()
    {
        for(int i =0; i<15; i++)
        {
            int val = (int)UnityEngine.Random.value * 3;  // 0-3
            switch (val)
            {
                case 0: Instantiate(RoadSet1.gameObject, instantiationPos, Quaternion.identity); currentInstantiatedTransform = RoadSet1; break;
                case 1: Instantiate(RoadSet2.gameObject, instantiationPos, Quaternion.identity); currentInstantiatedTransform = RoadSet2; break;
                case 2: Instantiate(RoadSet3.gameObject, instantiationPos, Quaternion.identity); currentInstantiatedTransform = RoadSet3; break;
                case 3: Instantiate(RoadSet4.gameObject, instantiationPos, Quaternion.identity); currentInstantiatedTransform = RoadSet4; break;
            }
            instantiationPos = currentInstantiatedTransform.position + Vector3.forward*15.99f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
