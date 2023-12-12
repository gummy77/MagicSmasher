using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSouce;

    [SerializeField] LayerMask damageLayers;

    [Header("Melee Attacks")]
    [SerializeField] private float meleeAttackDistance;
    [SerializeField] private float meleeAttackRadius;
    [SerializeField] private AudioClip swingAudio;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSouce = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            anim.SetTrigger("Attack");
        }
    }

    public void MeleeAttackSound(){
        audioSouce.PlayOneShot(swingAudio, 0.5f);
    }

    public void MeleeAttackTrigger(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + (transform.forward * meleeAttackDistance) + new Vector3(0, 1, 0), meleeAttackRadius, damageLayers);
        foreach (var hitCollider in hitColliders)
        {
            EnemyHealthController enemyHealth = hitCollider.gameObject.GetComponent<EnemyHealthController>();
            if(enemyHealth){
                enemyHealth.TakeDamage(1);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (transform.forward * meleeAttackDistance) + new Vector3(0, 1, 0), meleeAttackRadius);
    }
}
