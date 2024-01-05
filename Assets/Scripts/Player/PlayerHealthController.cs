using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    private AudioSource audioSouce;

    [SerializeField] private int maxHealth;
    [SerializeField] private int health;

    [Header("UI Elements")]
    [SerializeField] private RectTransform healthBar;
    [SerializeField] private RectTransform healthDamageBar;
    [SerializeField] private Image damageIndicator;

    [Header("Audio")]
    [SerializeField] private AudioClip[] damageSounds;

    void Start(){
        audioSouce = GetComponent<AudioSource>();
        health = maxHealth;
        updateUI();
    }

    void Update() {
        updateUI();
    }

    public void TakeDamage(int damage){
        health -= damage;
        audioSouce.PlayOneShot(damageSounds[Random.Range(0, damageSounds.Length-1)], Random.Range(0.2f, 0.5f));
        if(health <= 0){
            GetComponent<PlayerMovementController>().enabled = false;
        }
        health = Mathf.Clamp(health, 0, maxHealth * 2);

        Color myCol = damageIndicator.color;
        myCol.a = damageIndicator.color.a + ((float)damage/2.0f);
        damageIndicator.color = myCol;
    }

    public bool isDead(){
        return health <= 0;
    }

    public void revive(){
        GetComponent<PlayerMovementController>().enabled = true;
        GetComponent<PlayerAttackController>().enabled = true;
        health = maxHealth;
    }

    private void updateUI(){
        float healthPosition = Mathf.Lerp(healthBar.localScale.x, ((float)health / (float)maxHealth), Time.deltaTime * 4.0f);
        healthBar.localScale = new Vector3(healthPosition, 1, 1);

        float healthDamagePosition = Mathf.Lerp(healthDamageBar.localScale.x, ((float)health / (float)maxHealth), Time.deltaTime/ (health <= 0 ? 0.25f : 1.5f) );
        healthDamageBar.localScale = new Vector3(healthDamagePosition, 1, 1);

        Color myCol = damageIndicator.color;
        myCol.a = Mathf.Lerp(damageIndicator.color.a, 0, Time.deltaTime);
        damageIndicator.color = myCol;
    }
}
