using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour {


    [Tooltip("The range needed to jump to the correct position.")]
    [SerializeField]
    private float range = 0.1f;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private Transform[] targets;


    private bool hasSwitchedPos = false;
    private Transform currTarget;

    // Update is called once per frame
    void Update()
    {
       //Move to new destination
       transform.position = Vector3.MoveTowards(transform.position, currTarget.position, speed);
       
       //In range
       if (Vector3.Distance(transform.position, currTarget.position) < range)
       {
           //Hit target and wait
           transform.position = currTarget.position;
            hasSwitchedPos = false;
       }

        if (!hasSwitchedPos)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (currTarget == targets[i])
                {
                    if (targets[i + 1] != null)
                    {
                        currTarget = targets[i + 1];
                        hasSwitchedPos = true;
                    }
                }
            }
        }
    }
}
