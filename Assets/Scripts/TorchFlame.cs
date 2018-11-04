using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFlame : MonoBehaviour {

    [SerializeField]
    ParticleSystem flame;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            flame.Play();
        }
    }
}
