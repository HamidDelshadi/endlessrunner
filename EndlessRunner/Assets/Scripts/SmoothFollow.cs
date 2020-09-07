using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    /// <summary>
    /// It keeps the camera in a certain distance from the player
    /// </summary>
    public Transform target;
    float distance = 5.5f; //the distance between camera and the target
    float height = 1.7f; // the height of the camera
    float heightDamping = 5.0f; // controls the rate of change in the height to make the transition smooth
    float rotationDamping = 3.0f; // controls the rate of change in the rotation to make the transition smooth

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        if (!Context.Data.Player.IsDead) // if the player is alive
        {
            //the current position and rotation of the target
            float wantedRotationAngle = target.eulerAngles.y;
            float wantedHeight = target.position.y + height;

            //the current position and rotation of the camera
            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;

            //the smooth interpolation
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            //put the camera in the position of the target
            transform.position = target.position;
            //put a distance between them in the right angle
            transform.position -= currentRotation * Vector3.forward * distance;

            //levitates the camera
            transform.position = new Vector3(transform.position.x,
                                    currentHeight,
                                    transform.position.z);
        }
        //face the camera toward the target
        transform.LookAt(target);
    }
}
