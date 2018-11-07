using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {

    public GameObject blocks;

    public bool on;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (on)
            {
                blocks.SetActive(true);
            }
            else
            {
                blocks.SetActive(false);
            }
        }
    }
}
