using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinMovementController : EnemyMovementController
{
    [HideInInspector] public float stoppingdistance;

    [SerializeField] private bool willRun = false;
    [SerializeField] private float fleeDistance = 3f;
    public bool isfleeing = false;

    // void LateStart(){
    //     nav.stoppingDistance = stoppingdistance;
    // }

    void Update(){
        float distance = Vector3.Distance(transform.position, target.position);

        if (isfleeing) {
            Invoke(nameof(Run),Random.Range(2, 5));
            if (distance > stoppingdistance) isfleeing = false;
            return;
        }

        nav.speed = movementSpeed;
        nav.SetDestination(target.position);

        if(distance <= fleeDistance && willRun) {
            Run();
            isfleeing = true;
        } 

    }

    void Run(){
        Vector3 runTo = transform.position + ((transform.position - target.position) * 10f) + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        nav.speed = Random.Range(movementSpeed, movementSpeed * 1.5f);
        nav.isStopped = false;
        nav.SetDestination(runTo);
    }
}
