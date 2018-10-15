using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeForm : MonoBehaviour {

    [SerializeField]
    private float meldTimerCap = 1.0f;
    [SerializeField]
    private float unmeldTimerCap = 1.0f;
    [SerializeField]
    private GameObject bigMode;
    [SerializeField]
    private GameObject littleMode;

    private RaycastDetection rayDet;
    private bool isLittle = false;
    private PlayerMovement playerMovement;
    private float meldTimer = 0.0f;
    private float unmeldTimer = 0.0f;
    private bool firstTime = true;
    private bool hasJustUnmelded = false;


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
                firstTime = false;
                hasJustUnmelded = false;
                unmeldTimer = 0.0f;
                playerMovement.IsUnmelded = false;
                playerMovement.IsMelding = true;
            }
            else if (Input.GetButtonDown("Fire1") && isLittle && !playerMovement.OnWall)
            {
                meldTimer = 0.0f;
                playerMovement.IsMelding = false;

                if(!hasJustUnmelded)
                {
                    playerMovement.IsUnmelded = true;
                }
                
                bigMode.SetActive(true);
                littleMode.SetActive(false);
                isLittle = false;
            }
            
        }
        else
        {
            if(!firstTime)
            {
                meldTimer = 0.0f;
                playerMovement.IsMelding = false;
                
                if(!hasJustUnmelded)
                {
                    playerMovement.IsUnmelded = true;
                }

                bigMode.SetActive(true);
                littleMode.SetActive(false);
                isLittle = false;
            }
            
        }

        //Timers
        if(playerMovement.IsMelding)
        {
            meldTimer += Time.deltaTime;
        }
        else if(playerMovement.IsUnmelded)
        {
            unmeldTimer += Time.deltaTime;
        }

        //Little
        if (meldTimer > meldTimerCap)
        {
            bigMode.SetActive(false);
            littleMode.SetActive(true);
            isLittle = true;
        }

        //Big
        if(unmeldTimer > unmeldTimerCap)
        {
            hasJustUnmelded = true;
            playerMovement.IsUnmelded = false;
        }
    }
}
