using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private NavMeshAgent nav;


    [SerializeField] private Transform target;

    void Start(){
        nav = GetComponent<NavMeshAgent>();
    }
    
    void Update(){
        nav.SetDestination(target.position);
    }

    public void setTarget(Transform _target){
        target = _target;
    }
}
