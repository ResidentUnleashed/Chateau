using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float maxWait = 1.0f;

    private Transform startPos;
    private float timer;
    private float waitTimer;
    private bool moveTowardsTarget = true;
    private bool wait = false;

	// Use this for initialization
	void Start ()
    {
        startPos = transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(moveTowardsTarget && !wait)
        {
            waitTimer = 0.0f;
            timer += Time.deltaTime * speed;

            //Move to new destination
            transform.position = Vector3.MoveTowards(startPos.position, target.position, timer);

            //In range
            if (Vector3.Distance(transform.position, target.position) < 1.0f)
            {
                //Hit target and wait
                transform.position = target.position;
                wait = true;
                moveTowardsTarget = false;
            }
        }
        else if (!moveTowardsTarget && !wait)
        {
            waitTimer = 0.0f;
            timer += Time.deltaTime * speed;

            //Move to new destination
            transform.position = Vector3.MoveTowards(target.position, startPos.position, timer);

            //In range
            if (Vector3.Distance(transform.position, startPos.position) < 1.0f)
            {
                //Hit target and wait
                transform.position = startPos.position;
                wait = true;
                moveTowardsTarget = true;
            }
        }
        
        if(wait)
        {
            timer = 0.0f;
            waitTimer += Time.deltaTime;

            if(waitTimer > maxWait)
            {
                //Waited long enough
                wait = false;
            }
        }

    }
}
