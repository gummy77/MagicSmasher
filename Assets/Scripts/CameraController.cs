using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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


    void LateUpdate()
    {
        trackPosition();
        trackRotation();
    }

    private void trackPosition() {
        if(positionTarget == null) return; //makes sure target exists
        transform.position = Vector3.Lerp(transform.position, positionTarget.position, Time.deltaTime * positionFollowSpeed); //translates camera position
    }
    private void trackRotation() {
        if(rotationTarget == null) return; //makes sure target exists
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget.rotation, Time.deltaTime * rotationFollowSpeed); //rotates camera
    }
}
