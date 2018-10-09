using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour {

    [SerializeField]
    private LevelStore levelStore;

    private bool hasScrolled;
    private int currentSelected = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!hasScrolled)
        {
            //Change the current selected based on input with a clamp0
            if (Input.GetAxis("Vertical") == -1)
            {
                currentSelected++;

                if (currentSelected >= 2)
                {
                    currentSelected = 2;
                }

                hasScrolled = true;
            }
            else if (Input.GetAxis("Vertical") == 1)
            {
                currentSelected--;

                if (currentSelected <= 0)
                {
                    currentSelected = 0;
                }

                hasScrolled = true;
            }
        }

        if (Input.GetAxis("Vertical") == 0)
        {
            //Allow for non infinite scrolling
            hasScrolled = false;
        }

    }
}
