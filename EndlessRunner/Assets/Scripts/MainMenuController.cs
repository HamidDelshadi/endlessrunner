using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    GameObject[] panels; //list of panels inside the canvas
    GameObject[] mmButtons; //list of buttons responsible for opening a panel
    public Text HighScore; //shows the highscore
    public Text LastScore; //shows the last score

    void Start()
    {
        panels = GameObject.FindGameObjectsWithTag("subpanel");
        mmButtons = GameObject.FindGameObjectsWithTag("mmbutton");

        foreach (GameObject p in panels) //hides all the panels at first
            p.SetActive(false);

        if (PlayerPrefs.HasKey("HighestScore")) //shows the highscore
            HighScore.text = "Highest Score: " + PlayerPrefs.GetInt("HighestScore").ToString(); ;
        if (PlayerPrefs.HasKey("LastScore")) //shows the last score
            LastScore.text = "Last Score: " + PlayerPrefs.GetInt("LastScore").ToString();
    }

    public void ClosePanel(Button button)
    {
        //for closing a panel
        button.gameObject.transform.parent.gameObject.SetActive(false);
        foreach (GameObject b in mmButtons)
            b.SetActive(true);
    }

    public void OpenPanel(Button button)
    {
        //for opening a panel
        button.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        foreach (GameObject b in mmButtons) //closing other panels
            if (b != button.gameObject)
                b.SetActive(false);
    }

    public void LoadGameScene()
    {
        Context.Reset();
        Context.StartCharacterSelectionScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Help()
    {
        Context.StartHelpScene();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            QuitGame();
        }
    }
}
