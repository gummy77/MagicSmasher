using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    /*
    Script for moving player based on keyboard controls, as well as other factors
    */

    private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeedModifier;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float jumpStrength;

    [Header("Rotation")]
    [SerializeField] private Transform head;
    [SerializeField] private float rotationSpeedModifier;
    [SerializeField] private float vertRotationSpeedModifier;
    [SerializeField] private Vector2 rotateLimits;
    private float currentXRotation;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
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

        //update position
        rb.Move(transform.position + (MoveVector * moveSpeedModifier * 0.1f), transform.rotation);

        if(Input.GetKeyDown("space")){
            rb.AddForce(transform.up * jumpStrength * 50);
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
}
