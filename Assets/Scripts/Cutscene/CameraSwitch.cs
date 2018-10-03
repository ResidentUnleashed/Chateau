using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

    [SerializeField]
    private GameObject[] cameraList;
    [SerializeField]
    private TowardsTarget[] scriptList;
    [SerializeField]
    private Animator greenPipAni;



    private float timer;
    private int currentCamera = 0;
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if(timer > 5.0f && currentCamera == 0)
        {
            cameraList[0].SetActive(false);
            cameraList[1].SetActive(true);

            currentCamera = 1;
        }
        else if (timer > 10.0f && currentCamera == 1)
        {
            cameraList[1].SetActive(false);
            cameraList[2].SetActive(true);

            currentCamera = 2;
        }
        else if (timer > 15.0f && currentCamera == 2)
        {
            cameraList[2].SetActive(false);
            cameraList[3].SetActive(true);

            currentCamera = 3;
        }
        else if (timer > 20.0f && currentCamera == 3)
        {
            cameraList[3].SetActive(false);
            cameraList[4].SetActive(true);

            currentCamera = 4;
        }
        else if (timer > 25.0f && currentCamera == 4)
        {
            cameraList[4].SetActive(false);
            cameraList[5].SetActive(true);

            currentCamera = 5;
        }
    }
}
