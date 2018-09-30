using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsTarget : MonoBehaviour {

    [Tooltip("The range needed to jump to the correct position.")]
    [SerializeField]
    private float range = 0.1f;

    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float maxWait = 1.0f;

    [SerializeField]
    private Transform startPos;


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
                Transform tempTrans = target;

                target = startPos;
                startPos = tempTrans;

                hasSwitchedPos = true;
            }

        }
        else
        {
            waitTimer = 0.0f;
            hasSwitchedPos = false;


            //Move to new destination
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);

            //In range
            if (Vector3.Distance(transform.position, target.position) < range)
            {
                //Hit target and wait
                transform.position = target.position;
                wait = true;
            }
        }
    }
}
