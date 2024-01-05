using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private PlayerAttackController pAttack;

    void Start(){
        anim = GetComponent<Animator>();
    }

    public void Trigger(string triggerName){
        anim.SetTrigger(triggerName);
    }

    public void MeleeAttackTrigger() {
        pAttack.MeleeAttackTrigger();
    }
}
