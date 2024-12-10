using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionPath : MonoBehaviour
{
    [SerializeField] Transform destonation;
    [SerializeField] Transform[] ladder = new Transform[3];
    [SerializeField] Transform[] ladderUpperPoint = new Transform[3];

    [SerializeField] bool onLadder;

    [SerializeField] int destonationFloor;
    [SerializeField] int currentFloor;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
        CheckFloor();
        //if destonation is on the same floor then go to destonation
        if (currentFloor == destonationFloor)
        {
            agent.SetDestination(destonation.position);
        }
        
        else if (onLadder & destonationFloor > currentFloor)
        {
            agent.SetDestination(ladderUpperPoint[currentFloor].position);
        }

        //if it is on different floor then go to closest ladder
        else
        {
            agent.SetDestination(ladder[currentFloor].position);
        }
        float xDiff = Mathf.Abs(transform.position.x - ladder[currentFloor].position.x);

        if (xDiff < 0.5)
        {
            onLadder = true;
        }
    }

    void CheckFloor()
    {
        //if the destonation is at least -0.5 below by agent's Y then destonation floor is below
        if (destonation.position.y < transform.position.y - 0.5f)
        {
            destonationFloor = currentFloor - 1;
        }
        //if the destonation is at least 0.2 above by agent's Y then destonation floor is above
        else if (destonation.position.y > transform.position.y + 0.2f)
        {
            destonationFloor = currentFloor + 1;
        }
        
        else
        {
            destonationFloor = currentFloor;
        }
    }
}
