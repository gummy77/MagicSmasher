using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    private AudioSource audioSouce;

    [SerializeField] private int maxHealth;
    private ParticleSystem damageParticles;

    [SerializeField] private int health;

    [SerializeField] private GameObject deathParticles;

    [SerializeField] private AudioClip shieldBlock;

    void Start(){
        audioSouce = GetComponent<AudioSource>();
        damageParticles = GetComponent<ParticleSystem>();
        health = maxHealth;
    }

    public void TakeDamage(int damage){
        if(!GetComponent<PlayerAttackController>().Shielding){
            health -= damage;
            damageParticles.Emit(20);
        } else{
            audioSouce.PlayOneShot(shieldBlock, 1f);
        }
        if(health <= 0){
            GetComponent<PlayerMovementController>().enabled = false;
            GetComponent<PlayerAttackController>().enabled = false;
        }
    }

    public bool isDead(){
        return health <= 0;
    }

    public void revive(){
        GetComponent<PlayerMovementController>().enabled = true;
        GetComponent<PlayerAttackController>().enabled = true;
        health = maxHealth;
    }
}
