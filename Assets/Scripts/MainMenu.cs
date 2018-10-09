using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuUI;
    [SerializeField]
    private GameObject creditsUI;
    [SerializeField]
    private GameObject levelSelectUI;



    private void Update()
    {
        
    }




    public void BackToMenu()
    {
        //Turn on the main menu
        creditsUI.SetActive(false);
        levelSelectUI.SetActive(false);
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
        levelSelectUI.SetActive(false);
        creditsUI.SetActive(true);
    }
}
