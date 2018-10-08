using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private Text text1;
    [SerializeField]
    private Text text2;
    [SerializeField]
    private Text text3;

    private bool gameIsPaused = false;
    private int currentSelected = 0;
    private bool hasScrolled = false;
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("Start") && !gameIsPaused)
        {
            gameIsPaused = true;
        }
        else if (Input.GetButtonDown("Start") && gameIsPaused)
        {
            gameIsPaused = false;
        }

        if (gameIsPaused)
        {
            Pause();
            
            if(!hasScrolled)
            {
                //Change the current selected based on input with a clamp
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

            if(Input.GetAxis("Vertical") == 0)
            {
                //Allow for non infinite scrolling
                hasScrolled = false;
            }
            
        }
        else
        {
            Resume();
        }

        //Change visuals of objects depending on if they are selected
        if (currentSelected == 0)
        {
            text3.color = Color.black;
            text2.color = Color.black;
            text1.color = Color.yellow;

            if (Input.GetButtonDown("Fire1"))
            {
                Resume();
            }
        }
        else if (currentSelected == 1)
        {
            text1.color = Color.black;
            text3.color = Color.black;
            text2.color = Color.yellow;

            if (Input.GetButtonDown("Fire1"))
            {
                LoadMenu();
            }
        }
        else if (currentSelected == 2)
        {
            text1.color = Color.black;
            text2.color = Color.black;
            text3.color = Color.yellow;

            if (Input.GetButtonDown("Fire1"))
            {
                Quit();
            }
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);

        //Set back to normal
        Time.timeScale = 1.0f;

        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);

        //Freeze the game
        Time.timeScale = 0.0f;

        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        //Change to main menu scene
    }

    public void Quit()
    {
        //Quit the game
        Application.Quit();
    }
}
