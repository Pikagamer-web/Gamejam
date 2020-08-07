using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotsAI : MonoBehaviour
{
    public Transform wayPoint1, wayPoint2;
    public float speed = 10f, rotationDelta = 10f;
    float speed2;
    Transform currentWayPoint;
    CharacterControllerMine player;
    int flag = 0;
    [SerializeField]GamePlayUIManager gum;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerMine>();
        gum = GameObject.FindGameObjectWithTag("Manager").GetComponent<GamePlayUIManager>();
        currentWayPoint = wayPoint2;
        speed2 = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gum.score > 500)
        {
            speed2 = (gum.score / 1000) * 35f;
        }
         if (!player.HasCorruptionStarted)
        {
            Vector3 pos = transform.position;
            pos.y = 1f;
            transform.position = pos;
            if (flag != 0) {currentWayPoint = wayPoint1; flag = 0; }
             transform.position = Vector3.MoveTowards(transform.position, currentWayPoint.position, speed * Time.deltaTime);
            // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-transform.position + currentWayPoint.position), rotationDelta * Time.deltaTime);
            Quaternion targetRot = Quaternion.LookRotation(-transform.position + currentWayPoint.position);
            targetRot = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation,targetRot, rotationDelta * Time.deltaTime);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(currentWayPoint.position.x, currentWayPoint.position.z)) < 0.1f)
             {
                 if (currentWayPoint == wayPoint2) { currentWayPoint = wayPoint1; }
                 else { currentWayPoint = wayPoint2; }
             }
         }
         else
         {

             currentWayPoint = player.transform;
            Vector3 pos = transform.position;
            pos.y = 7f;
            transform.position = pos;
             transform.position = Vector3.MoveTowards(transform.position, currentWayPoint.position, speed * Time.deltaTime);
            // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-transform.position + currentWayPoint.position), rotationDelta * Time.deltaTime);
            Quaternion targetRot = Quaternion.LookRotation(-transform.position + currentWayPoint.position);
            targetRot = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotationDelta * Time.deltaTime);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(currentWayPoint.position.x, currentWayPoint.position.z)) < 2f)
             {
                 speed = 0;
             }
             else
             {
                 speed = speed2;
             }
             flag = 1;
         }

        


    }

    
}
