using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private ParticleSystem damageParticles;

    [SerializeField] private int health;



    void Start(){
        damageParticles = GetComponent<ParticleSystem>();
        health = maxHealth;
    }

    public void TakeDamage(int damage){
        health -= damage;
        damageParticles.Emit(20);

        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
