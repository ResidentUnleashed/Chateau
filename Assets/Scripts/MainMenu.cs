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




    public void BackToMenu()
    {
        creditsUI.SetActive(false);
        levelSelectUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void LevelSelect()
    {
        creditsUI.SetActive(false);
        mainMenuUI.SetActive(false);
        levelSelectUI.SetActive(true);
    }
}
