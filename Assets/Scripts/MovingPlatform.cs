using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [Tooltip("The range needed to jump to the correct position.")]
    [SerializeField]
    private float range = 0.1f;

    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform target2;

    [SerializeField]
    private Transform target3;

    [SerializeField]
    private float maxWait = 1.0f;

    [SerializeField]
    private Transform startPos;

    [SerializeField]
    private int numberOfTargets;


    private float waitTimer;
    private bool wait = false;
    private bool hasSwitchedPos = false;
    private Transform start;
    private Transform middle;
    private Transform middle2;
    private Transform end;
    private int currentTarget = 1;
    private bool startTimer = false;
    private bool firstTime = true;


    private void Awake()
    {
        start = startPos;
        middle = target;
        middle2 = target2;
        end = target3;
    }

    // Update is called once per frame
    void Update ()
    {
        if(startTimer)
        {
            waitTimer += Time.deltaTime;
        }

        if (waitTimer > maxWait)
        {
            //Waited long enough
            startTimer = false;
            wait = false;
        }


        if (wait)
        {
            startTimer = true;

            if(!hasSwitchedPos && numberOfTargets == 1)
            {
                //Switch the start pos and the end pos
                Transform tempTrans = target;

                target = startPos;
                startPos = tempTrans;

                hasSwitchedPos = true;
            }
            else if (!hasSwitchedPos && numberOfTargets == 2)
            {
                //Check if we need to start again
                if (target == start)
                {
                    currentTarget = 0;
                }


                if (currentTarget == 0)
                {
                    target = middle;

                    currentTarget++;
                    hasSwitchedPos = true;
                }
                else if (currentTarget == 1)
                {
                    target = middle2;

                    currentTarget++;
                    hasSwitchedPos = true;
                }
                else if (currentTarget == 2)
                {
                    target = start;

                    hasSwitchedPos = true;
                }
            }
            else if (!hasSwitchedPos && numberOfTargets == 3)
            {
                //Check if we need to start again
                if (target == start)
                {
                    currentTarget = 0;
                }


                if (currentTarget == 0)
                {
                    target = middle;

                    currentTarget++;
                    hasSwitchedPos = true;
                }
                else if (currentTarget == 1)
                {
                    target = middle2;

                    currentTarget++;
                    hasSwitchedPos = true;
                }
                else if (currentTarget == 2)
                {
                    target = end;

                    currentTarget++;
                    hasSwitchedPos = true;
                }
                else if (currentTarget == 3)
                {
                    target = start;

                    hasSwitchedPos = true;
                }
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
