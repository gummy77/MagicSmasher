using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private PlayerMouseController pMouse;

    private bool pickup;

    [Header("UI Elements")]
    [SerializeField] private GameObject pickupImage;

    void Start(){
        anim = GetComponent<Animator>();
    }

    void Update(){
        updateUIElements();
    }

    private void updateUIElements(){
        pickupImage.SetActive(pickup);
    }

    // -- Public Methods --

    public void Trigger(string triggerName){
        anim.SetTrigger(triggerName);
    }

    public void MeleeAttackTrigger() {
        pMouse.MeleeAttackTrigger();
    }

    public void setPickup(bool _pickup){
        pickup = _pickup;
    }
}
