using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerEnter : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    private bool isFoward = true;
    private bool startTimer = false;
    private float timer;


    private void Update()
    {
        if(startTimer)
        {
            timer += Time.deltaTime;
        }

        if(timer >= 1.0f)
        {
            startTimer = false;
            animator.SetBool("ToIdle", true);
            timer = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "CamAni")
        {
            if(isFoward)
            {
                animator.SetBool("Pos1", true);
                animator.SetBool("Pos2", false);
                startTimer = true;
            }
            else
            {
                animator.SetBool("Pos1", false);
                animator.SetBool("Pos2", true);
                startTimer = true;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "CamAni")
        {
            if (isFoward)
            {
                isFoward = false;
            }
            else
            {
                isFoward = true;
            }
        }
    }
}
