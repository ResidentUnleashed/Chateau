using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFlame : MonoBehaviour {

    [SerializeField]
    Animator flameAni;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            flameAni.Play("Torch");
        }
    }
}
