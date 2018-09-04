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

            //Lerp to new destination
            gameCamera.transform.position = Vector3.MoveTowards(startPos, newTarget.position, t);

            if(Vector3.Distance(gameCamera.transform.position, newTarget.position) < 0.1f)
            {
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