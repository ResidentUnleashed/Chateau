using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFlame : MonoBehaviour {

    [SerializeField]
    GameObject flameIdle;
    [SerializeField]
    GameObject flameActive;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(flameIdle != null && flameActive != null)
            {
                flameIdle.SetActive(false);
                flameActive.SetActive(true);
            }
        }
    }
}
