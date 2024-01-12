using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseController : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSouce;

    [SerializeField] LayerMask damageLayers;

    [Header("Melee Attacks")]
    [SerializeField] private float meleeAttackDistance;
    [SerializeField] private float meleeAttackRadius;
    [SerializeField] private AudioClip swingAudio;

    [HideInInspector] public bool Shielding;

    [Header("magic hehe")]
    [SerializeField] private GameObject fireball;

    [Header("UI")]
    [SerializeField] private UIController uiController;


    

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSouce = GetComponent<AudioSource>();
    }

    void Update()
    {
        RaycastHit hit;
        bool rayHit = Physics.Raycast(transform.position, transform.forward, out hit, 5f);

        uiController.setPickup(false);

        switch (rayHit ? hit.collider.gameObject.tag : null) {
            case("quest"):
                uiController.setPickup(true);
            break;

            default:
                if(Input.GetMouseButtonDown(0)){
                    MeleeAttackSound();
                    uiController.Trigger("punch");
                }
                if(Input.GetMouseButtonDown(1)){
                    Instantiate(fireball, transform.position + (transform.forward * 2.0f), transform.rotation);
                }
            break;
        }
    }

    public void MeleeAttackSound(){
        audioSouce.PlayOneShot(swingAudio, 0.5f);
    }

    public void MeleeAttackTrigger(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + (transform.forward * meleeAttackDistance), meleeAttackRadius, damageLayers, QueryTriggerInteraction.Collide);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.layer == 8) {
                hitCollider.transform.rotation = transform.rotation;
                hitCollider.gameObject.GetComponent<Projectile>().Parried();
                return;
            }
            EnemyController enemyHealth = hitCollider.gameObject.GetComponent<EnemyController>();
            if(enemyHealth){
                enemyHealth.TakeDamage(1, transform.position);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (transform.forward * meleeAttackDistance), meleeAttackRadius);
    }
}
