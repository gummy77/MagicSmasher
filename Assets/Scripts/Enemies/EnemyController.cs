using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent nav;
    [HideInInspector] public Transform target;
    protected Animator anim;

    [Header("Generic Stats")]
    [SerializeField] private int maxHealth;
    [SerializeField] protected float movementSpeed;
    [SerializeField] private float fleeModifier = 1;

    [Header("Generic Particles")]
    [SerializeField] private ParticleSystem damageParticles;
    [SerializeField] private GameObject deathParticles;

    protected int health;
    protected Vector3 fleeingFrom;
    protected bool fleeing;
    protected float fleeTimer;

    //fleeing stuff
    private float steering;
    private float acc;

    void Start(){
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        nav.speed = movementSpeed;
        health = maxHealth;
    }

    virtual public void Update() {
        if (fleeing) {
            Flee();
        }
        Idle();
    }

    virtual protected void Idle() {
        if(nav.remainingDistance <= nav.stoppingDistance) {
            if(Random.Range(0.0f, 1.0f) <= 0.01f) {
                setWanderTarget();
            }
        }
    }

    private void setWanderTarget() {
        nav.SetDestination(new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10)));
    }

    protected void Flee(){
        if(nav.remainingDistance <= nav.stoppingDistance) {
            Vector3 runTo = transform.position + (Vector3.Normalize(transform.position - fleeingFrom) * 1) + new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));
            nav.speed = Random.Range(movementSpeed * (fleeModifier/2), movementSpeed * fleeModifier);
            nav.SetDestination(runTo);
        }
        fleeTimer -= Time.deltaTime;

        if(fleeTimer <= 0) {
            nav.speed = movementSpeed;
            fleeing = false;

            nav.angularSpeed = steering;
            nav.angularSpeed *= 10;
        }
    }
    

    virtual public void Wound(int damage, Vector3 AttackerPosition) {
        fleeing = true;
        fleeingFrom = AttackerPosition;
        fleeTimer = Random.Range(3.0f, 6.0f);

        acc = nav.acceleration;
        nav.acceleration *= 10;

        steering = nav.angularSpeed;
        nav.angularSpeed *= 10;

        Vector3 runTo = transform.position + (Vector3.Normalize(transform.position - fleeingFrom) * 1) + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        nav.speed = Random.Range(movementSpeed * (fleeModifier/2), movementSpeed * fleeModifier);
        nav.SetDestination(runTo);
    }

    // -- Public Functions --

    public void TakeDamage(int damage, Vector3 AttackerPosition){
        health -= damage;
        Wound(damage, AttackerPosition);
        damageParticles.Emit(20);
        

        if(health <= 0){
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void setTarget(Transform _target) {
        target = _target;
    }
}
