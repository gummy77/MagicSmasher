using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    /*
    Script for moving player based on keyboard controls, as well as other factors
    */

    private CharacterController cc;
    private Animator anim;
    private AudioSource audioSouce;

    [Header("Movement")]
    [SerializeField] private float moveSpeedModifier;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float gravity = 20.0f;
    private bool isSprinting;

    [Header("Rotation")]
    [SerializeField] private Transform head;
    [SerializeField] private float rotationSpeedModifier;
    [SerializeField] private float vertRotationSpeedModifier;
    [SerializeField] private Vector2 rotateLimits;
    private float currentXRotation;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] stepSounds;
    [SerializeField] private float stepSpeed;
    private float stepTimer;
    [SerializeField] private AudioClip jumpSound;

    private Vector3 moveDirection;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audioSouce = GetComponent<AudioSource>();
    }

    void Update() {
        if(Cursor.lockState == CursorLockMode.Locked){
            if (Input.GetKey(KeyCode.Escape)) {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            updatePosition();
            updateRotation();
        } else {
            if (Input.GetMouseButton(0)){
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void updatePosition()
    {
        //read keyboard inputs
        float _inputX = Input.GetAxis("Horizontal");
        float _inputY = Input.GetAxis("Vertical");

        //turn into vector
        Vector3 MoveVector = (transform.forward * _inputY) + (transform.right * _inputX);
        //MoveVector = Vector3.Normalize(MoveVector) * MoveVector.magnitude;

        //if sprinting add speed
        if (Input.GetKey("left shift")) {
            isSprinting = true;
            MoveVector *= 2;
        } else {
            isSprinting = false;
        }

        //update position
        float movementDirectionY = moveDirection.y;
        moveDirection += (MoveVector * moveSpeedModifier * 0.1f);

        //Clamp Speed and add drag
        moveDirection *= 0.9f;
        moveDirection = Vector3.ClampMagnitude(moveDirection, (moveSpeedModifier * (isSprinting ? 2f : 4f)));

        if(Input.GetButton("Jump") && cc.isGrounded){
            moveDirection.y = jumpStrength;
            audioSouce.PlayOneShot(jumpSound, 0.5f);
        } else {
            moveDirection.y = movementDirectionY;
        }

        if (!cc.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        } else {
            
            if(stepTimer >= stepSpeed && Mathf.Abs(moveDirection.x + moveDirection.z) / 2 >= 1.5f) {
                PlayStepSound();
                stepTimer = Random.Range(0.0f, stepSpeed/6);
            }
        }
        stepTimer += Time.deltaTime * (isSprinting ? 1.75f : 1);

        cc.Move(moveDirection * Time.deltaTime);
    }

    void updateRotation()
    {
        //read mouse inputs
        float _rotationX = -Input.GetAxis("Mouse Y") * rotationSpeedModifier;
        float _rotationY = Input.GetAxis("Mouse X") * vertRotationSpeedModifier;

        //clamp the vertical axis
        currentXRotation = Mathf.Clamp(currentXRotation, -rotateLimits.x, rotateLimits.y);

        //apply rotation
        currentXRotation += _rotationX;
        head.localRotation = Quaternion.Euler(currentXRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, _rotationY, 0);
    }

    public void PlayStepSound(){
        audioSouce.PlayOneShot(stepSounds[(int)(Random.Range(0, stepSounds.Length))], 0.5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + (Vector3.up*0.1f), transform.position - Vector3.up * 0.2f);
    }
}
