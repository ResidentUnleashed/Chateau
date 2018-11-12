using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour {

    [SerializeField]
    private AudioClip ocean;

    [SerializeField]
    private AudioClip portal;

    [SerializeField]
    private AudioClip waterfall;

    [SerializeField]
    private AudioClip volcano;

    [SerializeField]
    private AudioSource audioSource;



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ocean")
        {
            audioSource.clip = ocean;
            audioSource.Play();
        }
        else if (other.tag == "Portal")
        {
            audioSource.clip = portal;
            audioSource.Play();
        }
        else if (other.tag == "Waterfall")
        {
            audioSource.clip = waterfall;
            audioSource.Play();
        }
        else if (other.tag == "Volcano")
        {
            audioSource.clip = volcano;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ocean")
        {
            audioSource.Stop();
        }
        else if (other.tag == "Portal")
        {
            audioSource.Stop();
        }
        else if (other.tag == "Waterfall")
        {
            audioSource.Stop();
        }
        else if (other.tag == "Volcano")
        {
            audioSource.Stop();
        }
    }
}
