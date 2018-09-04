using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    private GameObject pauseMenuUI;

    private bool gameIsPaused = false;
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("Start") && !gameIsPaused)
        {
            gameIsPaused = true;
        }

        if(gameIsPaused)
        {
            Pause();
        }
        else
        {
            Resume();
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
        //Add code here
    }

    public void Quit()
    {
        //Add code here
    }
}
