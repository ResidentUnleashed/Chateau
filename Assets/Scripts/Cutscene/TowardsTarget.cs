using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsTarget : MonoBehaviour {

    [Tooltip("The range needed to jump to the correct position.")]
    [SerializeField]
    private float range = 0.1f;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private Transform endTarget;

    [SerializeField]
    private float maxWait = 1.0f;

    [SerializeField]
    private float rotateSpeed = 2.0f;

    [SerializeField]
    private Transform startTarget;


    private float waitTimer;
    private bool wait = false;
    private bool hasSwitchedPos = false;

    // Update is called once per frame
    void Update()
    {

        if (waitTimer > maxWait)
        {
            //Waited long enough
            wait = false;
        }


        if (wait)
        {
            waitTimer += Time.deltaTime;

            if (!hasSwitchedPos)
            {
                //Switch the start pos and the end pos
                Transform tempTrans = endTarget;

                endTarget = startTarget;
                startTarget = tempTrans;

                hasSwitchedPos = true;
            }

        }
        else
        {
            waitTimer = 0.0f;
            hasSwitchedPos = false;


            //Move to new destination
            transform.position = Vector3.MoveTowards(transform.position, endTarget.position, speed);

            //In range
            if (Vector3.Distance(transform.position, endTarget.position) < range)
            {
                //Hit target and wait
                transform.position = endTarget.position;
                wait = true;
            }
        }


        //Look at the correct direction
        Vector3 relativePos = endTarget.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed);
    }
}
