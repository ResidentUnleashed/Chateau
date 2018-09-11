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


    private Transform newTarget = null;
    private bool moveTowards = false;
    private float t;
    private Vector3 startPos;

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
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "MoveCamera")
        {
            t = 0.0f;

            //Get our starting pos
            startPos = gameCamera.transform.position;

            //Get the child transform and we can switch target
            newTarget = other.transform.GetChild(0).gameObject.transform;

            if(gameCamera.transform != newTarget)
            {
                moveTowards = true;
            }
        }
    }
}