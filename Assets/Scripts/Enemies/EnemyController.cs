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

    public int health;
    private Vector3 fleeingFrom;
    private bool fleeing;
    private float fleeTimer;

    void Start(){
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        health = maxHealth;
    }

    public void Update() {
        if (fleeing) {
            Flee();
        }
        Idle();
    }

    private void Idle() {
        if(nav.remainingDistance <= nav.stoppingDistance) {
            if(Random.Range(0.0f, 1.0f) <= 0.01f) {
                setWanderTarget();
            }
        }
    }

    private void setWanderTarget() {
        nav.SetDestination(new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10)));
    }

    private void Flee(){
        if(nav.remainingDistance <= nav.stoppingDistance) {
            Vector3 runTo = transform.position + ((transform.position - fleeingFrom) * 10) + new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            nav.speed = Random.Range(movementSpeed * (fleeModifier/2), movementSpeed * fleeModifier);
            nav.SetDestination(runTo);
        }
        fleeTimer -= Time.deltaTime;

        if(fleeTimer <= 0) {
            fleeing = false;
        }
    }
    

    public void Wound(int damage, Vector3 AttackerPosition) {
        fleeTimer = Random.Range(3.0f, 6.0f);
        fleeing = true;
        fleeingFrom = AttackerPosition;
    }

    // -- Public Functions --

    public void TakeDamage(int damage, Vector3 AttackerPosition){
        health -= damage;
        damageParticles.Emit(20);
        Wound(damage, AttackerPosition);

        if(health <= 0){
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void setTarget(Transform _target) {
        target = _target;
    }
}
