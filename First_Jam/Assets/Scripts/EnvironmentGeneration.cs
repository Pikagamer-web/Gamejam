using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGeneration : MonoBehaviour
{
    [SerializeField] GameObject LevelUnit1, LevelUnit2, LevelUnit3;
    public int CurrentlyOnLevel ;
   

   
    // Update is called once per frame
    void Update()
    {
        switch (CurrentlyOnLevel)
        {
            case 1:
                if(!(LevelUnit2.transform.position == LevelUnit1.transform.position + Vector3.forward * 189f))
                LevelUnit2.transform.position = LevelUnit1.transform.position + Vector3.forward* 189f;
                break;
            case 2:
                if (!(LevelUnit3.transform.position == LevelUnit2.transform.position + Vector3.forward * 189f))
                    LevelUnit3.transform.position = LevelUnit2.transform.position + Vector3.forward * 189f;
                break;
            case 3:
                if (!(LevelUnit1.transform.position == LevelUnit3.transform.position + Vector3.forward * 189f))
                    LevelUnit1.transform.position = LevelUnit3.transform.position + Vector3.forward * 189f;
                break;
           
        }
    }

    
}
