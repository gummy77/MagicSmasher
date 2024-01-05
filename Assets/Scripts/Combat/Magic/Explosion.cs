using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionPushForce;
    [SerializeField] private int explosionDamage;
    [SerializeField] private float explosionRadius;

    public void Start() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); 

        foreach (Collider col in colliders) {
            PlayerHealthController pHealth = col.gameObject.GetComponent<PlayerHealthController>();
            EnemyController eHealth = col.gameObject.GetComponent<EnemyController>();
            Rigidbody eRB = col.gameObject.GetComponent<Rigidbody>();
            //CharacterController pCC = col.gameObject.GetComponent<CharacterController>();

            if(pHealth) pHealth.TakeDamage(explosionDamage);
            if(eHealth) eHealth.TakeDamage(explosionDamage, transform.position);
            //if(pCC) pCC. Fix somehow? maybe players dont bget pushed around?
            if(eRB) eRB.AddExplosionForce(explosionPushForce, transform.position, explosionRadius);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
