using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : MonoBehaviour
{
    [Tooltip("How fast the camera moves from one position to the next.")]
    [SerializeField]
    private float lerpSpeed = 1.0f;
    [SerializeField]
    private GameObject gameCamera;
    [Tooltip("The range needed to jump to the correct position.")]
    [SerializeField]
    private float range = 0.1f;
    [SerializeField]
    private float rotSpeed = 2.0f;
    [SerializeField]
    private float maxRotateTime = 1.0f;


    private Transform newTarget = null;
    private bool moveTowards = false;
    private float t;
    private Vector3 startPos;
    private bool rotate;
    private float rotateTimer;
    private Quaternion oldRot;
    private Quaternion targetRot;

    private void Update()
    {
        if(moveTowards)
        {
            //Clock
            t += Time.deltaTime * lerpSpeed;

            //Move to new destination
            gameCamera.transform.position = Vector3.MoveTowards(startPos, newTarget.position, t);

            //In range
            if(Vector3.Distance(gameCamera.transform.position, newTarget.position) < range)
            {
                //Hit target
                gameCamera.transform.position = newTarget.position;
                moveTowards = false;
            }
        }

        if (rotate)
        { 

            rotateTimer += Time.deltaTime;

            if (transform.rotation != targetRot) /* Does not equal our target rotation */
            {
                //Slerp towards rotation
                gameCamera.transform.rotation = Quaternion.RotateTowards(oldRot, targetRot, rotateTimer * rotSpeed);
            }
        }

        if(rotateTimer > maxRotateTime)
        {
            rotate = false;
            gameCamera.transform.rotation = targetRot;
            rotateTimer = 0.0f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "MoveCamera")
        {
            t = 0.0f;

            //Store our old rotation
            oldRot = transform.rotation;

            //Get our starting pos
            startPos = gameCamera.transform.position;

            //Get the child transform and we can switch target
            newTarget = other.transform.GetChild(0).gameObject.transform;

            //Get the child rotation and we can alter rotation
            targetRot = other.transform.GetChild(0).gameObject.transform.rotation;

            if (gameCamera.transform != newTarget)
            {
                moveTowards = true;
                rotate = true;
            }
        }
    }
}