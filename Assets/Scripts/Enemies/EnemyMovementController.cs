using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovementController : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent nav;
    [HideInInspector] public Transform target;
    protected Animator anim;

    [SerializeField] protected float movementSpeed;

    void Awake(){
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    public void setTarget(Transform _target){
        target = _target;
    }
}
