using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMeleeAttackController : MonoBehaviour
{
    private GoblinMovementController movement;
    private Animator anim;
    private AudioSource audioSouce;

    private Transform target;

    [SerializeField] private AudioClip swingAudio;

    [SerializeField] private Vector2 AttackSpeed;
    [SerializeField] LayerMask damageLayers;
    [SerializeField] private float meleeAttackDistance;
    [SerializeField] private float meleeAttackRadius;

    private float attackTimer = 0;

    void Start()
    {
        movement = GetComponent<GoblinMovementController>();
        anim = GetComponent<Animator>();
        audioSouce = GetComponent<AudioSource>();
        target = movement.target;
    }

    void Update() {
        if(movement.nav.remainingDistance <= movement.stoppingdistance){
            Vector3 targetDirection = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * 10f, 0.0f));
            
            if(attackTimer >= Random.Range(AttackSpeed.x, AttackSpeed.y)){
                Attack();
                attackTimer = 0;
            }
            attackTimer += Time.deltaTime;
        }
    }

    private void Attack(){
        anim.SetTrigger("Attack");
    }

    public void MeleeAttackSound(){
        audioSouce.PlayOneShot(swingAudio, 0.5f);
    }

    public void DamagePoint(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + (transform.forward * meleeAttackDistance) + new Vector3(0, 1, 0), meleeAttackRadius, damageLayers);
        foreach (var hitCollider in hitColliders)
        {
            PlayerHealthController playerHealth = hitCollider.gameObject.GetComponent<PlayerHealthController>();
            if(playerHealth){
                playerHealth.TakeDamage(1);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (transform.forward * meleeAttackDistance) + new Vector3(0, 1, 0), meleeAttackRadius);
    }
}
