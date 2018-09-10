using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour {

    [SerializeField]
    private Button[] buttonList;

    private int buttonIndex = 0;
	
	void Update ()
    {
        //Go through and select the current button
        if (Input.GetAxis("Horizontal") == 1)
        {
            buttonIndex++;

            //Cap button index
            if(buttonIndex > buttonList.Length)
            {
                buttonIndex = buttonList.Length;
            }
        }

        //Go through and select the current button
        if (Input.GetAxis("Horizontal") == -1)
        {
            buttonIndex--;

            //Cap button index
            if (buttonIndex < 0)

            {
                buttonIndex = 0;
            }
        }


        //Iterate through and highlight current button
        for(int i = 0; i < buttonList.Length; i++)
        {
            if(buttonList[i] == buttonList[buttonIndex])
            {
                //Highlight
            }
            else
            {
               //Unhighlight
            }
        }
    }
}
