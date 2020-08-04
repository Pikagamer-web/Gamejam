using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotsAI : MonoBehaviour
{
    public Transform wayPoint1, wayPoint2;
    NavMeshAgent agent;
    Transform currentWayPoint;
    CharacterControllerMine player;
    int flag = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerMine>();
        agent = GetComponent<NavMeshAgent>();
        currentWayPoint = wayPoint2;
        agent.SetDestination(currentWayPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.HasCorruptionStarted)
        {
            if(flag != 0) { agent.SetDestination(wayPoint1.position); currentWayPoint = wayPoint1; flag = 0; }
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
            new Vector2(currentWayPoint.position.x, currentWayPoint.position.z)) <= agent.stoppingDistance)
            {
                if (currentWayPoint == wayPoint2) { currentWayPoint = wayPoint1; }
                else { currentWayPoint = wayPoint2; }
                agent.SetDestination(currentWayPoint.position);
            }
        }
        else
        {
            agent.SetDestination(player.transform.position);
            flag = 1;
        }
        


    }
}
