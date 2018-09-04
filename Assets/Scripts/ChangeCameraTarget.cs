using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : MonoBehaviour
{
    [Tooltip("How fast the camera moves from one position to the next.")]
    [SerializeField]
    private float lerpSpeed = 1.0f;
    [SerializeField]
    private float maxTime;
    [SerializeField]
    private GameObject gameCamera;


    private Transform newTarget = null;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "MoveCamera")
        {
            //Get the child transform and we can switch target
            newTarget = other.transform.GetChild(0).gameObject.transform;

            if(gameCamera.transform != newTarget)
            {
                StartCoroutine(Transition());
            }
        }
    }

    private IEnumerator Transition()
    {
        //Create the float t (time)
        float t = 0.0f;

        //Get our starting pos
        Vector3 startPos = gameCamera.transform.position;

        while (t < maxTime)
        {
            //Clock
            t += Time.deltaTime * (Time.timeScale / lerpSpeed);

            //Lerp to new destination
            gameCamera.transform.position = Vector3.Lerp(startPos, newTarget.position, t);

            //Return
            yield return 0;
        }
    }
}