using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RewindableObject : MonoBehaviour
{
    [SerializeField] ObjectConfig config;
    [SerializeField] float timer =0, timer2 =0;
    [SerializeField] int index=0, currentTargetIndex = 49;
    [SerializeField]bool firstfill = true;
    [SerializeField] CharacterControllerMine player;
    [SerializeField] float secondsToRewind;
    [SerializeField] bool hasStaretedRewinding = false;
    [SerializeField] int rewindSwitch = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if (!player.IsRewinding)
       // {
            FillData();
       // }
       // else
       // {
       //    
       //     ExecuteRewind();
       // }
        
    }

    private void ExecuteRewind()
    {
        currentTargetIndex = (int)((1 / 0.6f) * secondsToRewind);
        transform.position = Vector3.MoveTowards(transform.position, config.positions[currentTargetIndex], 10f * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(config.rotations[currentTargetIndex]), 0.2f);
        if(Vector3.Distance(transform.position, config.positions[currentTargetIndex]) < 0.1f) { currentTargetIndex--; }
    }

    private void FillData()
    {
        timer += Time.deltaTime;

        if (timer >= 0.6f && firstfill)         // Values arfe being filled for the first time
        {
            config.positions[index] = transform.position;
            config.rotations[index] = transform.eulerAngles;
            ++index;
            timer = 0;
            if (index > 49) { firstfill = false; timer = 0; }
        }
        else
        {
            if (timer >= 0.6f)
            {
                timer = 0;
                Vector3 pos = Vector3.zero;
                Vector3 rot = transform.eulerAngles; 
                for (int i = 49; i >= 0; i--)
                {

                    if (i != 0)
                    {
                        pos = config.positions[i - 1];
                        rot = config.rotations[i - 1];
                        config.positions[i - 1] = config.positions[i];
                        config.rotations[i - 1] = config.rotations[i];
                        if (i == 49)
                        {
                            config.positions[i] = transform.position;
                            config.rotations[i] = transform.eulerAngles;
                        }
                    }
                  
                }
                
               


            }
        }
    }
}


