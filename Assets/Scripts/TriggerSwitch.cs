using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : MonoBehaviour {

    [SerializeField]
    private GameObject[] targetList;

    // Use this for initialization
    void Awake ()
    {
        targetList = GameObject.FindGameObjectsWithTag("MoveCamera");
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            for(int i = 0; i < targetList.Length; i++)
            {
                targetList[i].SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
