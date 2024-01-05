using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private AudioSource audioSource;
    [SerializeField] private AudioClip parry;

    public virtual void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }

    public virtual void OnTriggerEnter(Collider col) {
        Destroy(gameObject);
    }

    public virtual void Parried(){
        audioSource.PlayOneShot(parry);
    }
}
