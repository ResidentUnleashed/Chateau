using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerEnter : MonoBehaviour {

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animation ani;

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
            ani.Stop("Cam1 - E2S");
            ani.Stop("Cam1 - S2E");
            timer = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "CamAni")
        {
            if(isFoward)
            {
                ani.Play("Cam1 - S2E");
                startTimer = true;
            }
            else
            {
                ani.Play("Cam1 - E2S");
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
