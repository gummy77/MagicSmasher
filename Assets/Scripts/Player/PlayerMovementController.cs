using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    /*
    Script for moving player based on keyboard controls, as well as other factors
    */

    private Rigidbody rb;
    private Animator anim;
    private AudioSource audioSouce;

    [Header("Movement")]
    [SerializeField] private float moveSpeedModifier;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float jumpStrength;
    private bool isGrounded;

    [Header("Rotation")]
    [SerializeField] private Transform head;
    [SerializeField] private float rotationSpeedModifier;
    [SerializeField] private float vertRotationSpeedModifier;
    [SerializeField] private Vector2 rotateLimits;
    private float currentXRotation;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] stepSounds;
    [SerializeField] private AudioClip jumpSound;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSouce = GetComponent<AudioSource>();
    }

    void Update() {
        updatePosition();
        updateRotation();
    }

    void updatePosition()
    {
        //read keyboard inputs
        float _inputX = Input.GetAxis("Horizontal");
        float _inputY = Input.GetAxis("Vertical");

        //turn into vector
        Vector3 MoveVector = (transform.forward * _inputY) + (transform.right * _inputX);
        MoveVector = Vector3.Normalize(MoveVector) * MoveVector.magnitude;

        anim.SetFloat("Speed", MoveVector.magnitude);

        //update position
        rb.Move(transform.position + (MoveVector * moveSpeedModifier * 0.1f), transform.rotation);

        RaycastHit hit;
        if (Physics.Raycast(transform.position + (Vector3.up*0.1f), -Vector3.up, out hit, 0.2f, groundLayerMask)){
            isGrounded = true;
        }else {
            isGrounded = false;
        }
        anim.SetBool("AirBorne", !isGrounded);

        if(Input.GetKeyDown("space") && isGrounded){
            rb.AddForce(transform.up * jumpStrength * 50);
            audioSouce.PlayOneShot(jumpSound, 0.5f);
        }
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
        head.localEulerAngles = new Vector3(currentXRotation, 0, 0);
        transform.Rotate(transform.rotation.x, _rotationY, transform.rotation.z);
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
