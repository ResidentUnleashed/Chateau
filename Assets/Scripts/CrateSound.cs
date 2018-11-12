using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSound : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Rigidbody rb;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
           audioSource.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            audioSource.Stop();
        }
    }
}
