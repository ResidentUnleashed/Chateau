using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerEnter : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    private bool isFoward = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "MoveCamera")
        {
            if(isFoward)
            {
                animator.SetBool("Pos1", true);
                animator.SetBool("Pos2", false);
            }
            else
            {
                animator.SetBool("Pos1", false);
                animator.SetBool("Pos2", true);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "MoveCamera")
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
