using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSwitch : MonoBehaviour {

    [SerializeField]
    GameObject[] onList;

    [SerializeField]
    GameObject[] offList;



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "ParticleDisplay")
        {
            for (int i = 0; i < onList.Length; i++)
            {
                onList[i].SetActive(true);
            }

            for (int c = 0; c < offList.Length; c++)
            {
                offList[c].SetActive(false);
            }
        }

        if(other.transform.tag == "MoveCamera")
        {
            for (int i = 0; i < onList.Length; i++)
            {
                onList[i].SetActive(false);
            }

            for (int c = 0; c < offList.Length; c++)
            {
                offList[c].SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "ParticleDisplay")
        {
            for (int i = 0; i < onList.Length; i++)
            {
                onList[i].SetActive(false);
            }

            for (int c = 0; c < offList.Length; c++)
            {
                offList[c].SetActive(false);
            }
        }
    }
}
