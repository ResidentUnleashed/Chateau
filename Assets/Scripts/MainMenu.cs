using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuUI;
    [SerializeField]
    private GameObject creditsUI;
    [SerializeField]
    private GameObject levelSelectUI = null;
    [SerializeField]
    private string levelName;
    [SerializeField]
    private Text text1;
    [SerializeField]
    private Text text2;
    [SerializeField]
    private Text text3;

    //Play icon here

    private bool hasScrolled;
    private int currentSelected;

    private void Update()
    {
        //If not in the menu 
        if(mainMenuUI.activeSelf == false)
        {
            if(Input.GetButtonDown("Fire2"))
            {
                BackToMenu();
            }
        }
        else
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


            //Change visuals of objects depending on if they are selected
            if (currentSelected == 0)
            {
                text3.color = Color.black;
                text2.color = Color.black;
                text1.color = Color.yellow;


                if (Input.GetButtonDown("Fire1"))
                {
                    //Load first level
                    SceneManager.LoadScene(levelName);
                }
            }
            else if (currentSelected == 1)
            {
                text1.color = Color.black;
                text3.color = Color.black;
                text2.color = Color.yellow;

                if (Input.GetButtonDown("Fire1"))
                {
                    Credits();
                }
            }
            else if (currentSelected == 2)
            {
                text1.color = Color.black;
                text2.color = Color.black;
                text3.color = Color.yellow;

                if (Input.GetButtonDown("Fire1"))
                {
                    Application.Quit();
                }
            }
        }
    }




    public void BackToMenu()
    {
        //Turn on the main menu
        creditsUI.SetActive(false);
        //levelSelectUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void LevelSelect()
    {
        //Turn on level select
        creditsUI.SetActive(false);
        mainMenuUI.SetActive(false);
        levelSelectUI.SetActive(true);
    }

    public void Credits()
    {
        //Turn on credits
        mainMenuUI.SetActive(false);
        //levelSelectUI.SetActive(false);
        creditsUI.SetActive(true);
    }
}
