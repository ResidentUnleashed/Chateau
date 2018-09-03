using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour {

    [Tooltip("How fast the camera moves from one position to the next.")]
    [SerializeField]
    private float lerpSpeed = 3.0f;


    private ChangeCameraTarget changeCameraTarget;
    


	// Use this for initialization
	void Awake ()
    {
        changeCameraTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeCameraTarget>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(changeCameraTarget != null)
        {
            if (changeCameraTarget.CanSwitchTarget)
            {
                StartCoroutine(Transition());
                changeCameraTarget.CanSwitchTarget = false;
            }
        }
    }


    private IEnumerator Transition()
    {
        //Create the float t (time)
        float t = 0.0f;

        //Get our starting pos
        Vector3 startPos = transform.position;

        while (t < 1.0f)
        {
            //Clock
            t += Time.deltaTime * (Time.timeScale / lerpSpeed);

            //Lerp to new destination
            transform.position = Vector3.Lerp(startPos, changeCameraTarget.NewTarget.position, t);

            //Return
            yield return 0;
        }
    }
}
