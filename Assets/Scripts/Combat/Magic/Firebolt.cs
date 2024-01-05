using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebolt : Projectile
{
    [SerializeField] private int damage;

    public override void OnTriggerEnter(Collider col) {
        switch(col.gameObject.layer) {
            case(6):
                col.gameObject.GetComponent<PlayerHealthController>().TakeDamage(damage);
                break;
            case(7):
                col.gameObject.GetComponent<EnemyController>().TakeDamage(damage, transform.position);
                break;
        }
        Destroy(gameObject);
    }
}
