using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeForm : MonoBehaviour {

    public GameObject bigMode;
    public GameObject littleMode;

    private RaycastDetection rayDet;
    private bool isLittle = false;
    private PlayerMovement playerMovement;
    private bool underObject = false;

    public bool IsLittle
    {
        get { return isLittle; }
    }

    // Use this for initialization
    void Start ()
    {
        rayDet = GetComponent<RaycastDetection>();
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (rayDet.InShadow == true)
        {
            if(Input.GetButtonDown("Fire1") && !isLittle && !playerMovement.OnWall)
            {
                bigMode.SetActive(false);
                littleMode.SetActive(true);
                isLittle = true;
            }
            else if (Input.GetButtonDown("Fire1") && isLittle && !playerMovement.OnWall && !underObject)
            {
                bigMode.SetActive(true);
                littleMode.SetActive(false);
                isLittle = false;
            }
            
        }
        else
        {
            bigMode.SetActive(true);
            littleMode.SetActive(false);
            isLittle = false;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Under")
        {
            underObject = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Under")
        {
            underObject = false;
        }
    }
}
