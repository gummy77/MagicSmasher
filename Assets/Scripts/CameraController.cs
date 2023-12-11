using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*
        Simple Camera Follower.
    */

    [Header("Editor Controls")]
    [SerializeField]
    private float positionFollowSpeed;
    [SerializeField]
    private float rotationFollowSpeed;

    [Header("Target to Follow")]
    [SerializeField]
    private Transform positionTarget;
    [SerializeField]
    private Transform rotationTarget;
    [SerializeField]
    private Transform followTarget;


    private Vector3 velocity = Vector3.zero;
    private Vector3 tVelocity = Vector3.zero;

    void Update() {
        setPosition();
        setRotation();
    }

    void setPosition() {
        if(positionTarget == null) return; //makes sure target exists
        followTarget.position = positionTarget.position;
    }
    void setRotation() {
        if(rotationTarget == null) return; //makes sure target exists
        followTarget.rotation = rotationTarget.rotation;
    }

    void FixedUpdate()
    {
        trackPosition();
        trackRotation();
    }

    private void trackPosition() {
        transform.position = Vector3.SmoothDamp(transform.position, followTarget.position, ref velocity, positionFollowSpeed);
    }
    private void trackRotation() {
        transform.rotation = Quaternion.Slerp(transform.rotation, followTarget.rotation, Time.fixedDeltaTime * rotationFollowSpeed); //rotates camera
    }
}
