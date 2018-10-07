using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSwitch : MonoBehaviour {

    [SerializeField]
    GameObject[] objectList;



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            for (int i = 0; i < objectList.Length; i++)
            {
                objectList[i].SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            for (int i = 0; i < objectList.Length; i++)
            {
                objectList[i].SetActive(false);
            }
        }
    }
}
