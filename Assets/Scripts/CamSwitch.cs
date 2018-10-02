using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour {

    public GameObject vCamMain;
    public GameObject[] vCamOther;

    private bool atStart = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player") && atStart)
        {
            vCamMain.SetActive(false);
            atStart = false;
            Debug.Log("YES");

            for(int i = 0; i < vCamOther.Length; i++)
            {
                vCamOther[i].SetActive(false);
            }
        }
    }
}
