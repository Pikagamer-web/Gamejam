using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RewindableObject : MonoBehaviour
{
    [SerializeField] ObjectConfig config;
    [SerializeField] float timer =0, timer2 =0;
    [SerializeField] int index=0, currentTargetIndex = 9;
    [SerializeField]bool firstfill = true;
    [SerializeField] CharacterControllerMine player;
    [SerializeField] float secondsToRewind;
    public bool hasStaretedRewinding = false;
    [SerializeField] int rewindSwitch = 0;
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        _ = TryGetComponent<Rigidbody>(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStaretedRewinding)
        {
            FillData();
            rewindSwitch = 0;
        }
        else
        {
          
            ExecuteRewind();
        }
        
    }

    private void ExecuteRewind()
    {
        rb.isKinematic = true;
        if (rewindSwitch == 0) { currentTargetIndex = 9; rewindSwitch = 1; }
        transform.position = Vector3.MoveTowards(transform.position, config.positions[currentTargetIndex], 10f * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(config.rotations[currentTargetIndex]), 0.2f);
        if(Vector3.Distance(transform.position, config.positions[currentTargetIndex]) < 0.1f)
        { 
            currentTargetIndex--;
            if (currentTargetIndex < 0)
            {
                rewindSwitch = 0;
                hasStaretedRewinding = false;
            }
        }
    }

    private void FillData()
    {
        rb.isKinematic = false;
        timer += Time.deltaTime;

       /* if (timer >= 0.6f && firstfill)         // Values arfe being filled for the first time
        {
            config.positions[index] = transform.position;
            config.rotations[index] = transform.eulerAngles;
            ++index;
            timer = 0;
            if (index > 9) { firstfill = false; timer = 0; }
        }
        else
        {*/
            if (timer >= 0.6f)
            {
                timer = 0;
                
                for (int i = 0; i< 9; i++)
                {
                    config.positions[i] = config.positions[i + 1];
                    config.rotations[i] = config.rotations[i + 1];
                }
                config.positions[9] = transform.position;
                config.rotations[9] = transform.eulerAngles;
            }
        //}
    }
}


