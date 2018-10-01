using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerEnter : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    private void Start()
    {
        animator.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "CamAni")
        {
            animator.enabled = true;
        }
    }
}
