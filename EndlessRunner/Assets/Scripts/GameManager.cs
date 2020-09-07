using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CharacterItem
{
    /// <summary>
    /// stores a character item with the name and the 3d object.
    /// </summary>
    public string Name;
    public GameObject Character;
}

public class GameManager: MonoBehaviour
{
    /// <summary>
    /// is responsible for the logic of the game. it is like a controller for the current game.
    /// </summary>

    public void Awake()
    {
        Context.Manager = this; //sets the related field in the context
        if (Context.Data.Lives==null) // if this is a new game then start with maximum lives
            Context.Data.Lives = MaxLives;
        if (!PlayerPrefs.HasKey("Character")) // use the default character if none is chosen.
            PlayerPrefs.SetString("Character", Characters[0].Name);
        string selected_char = PlayerPrefs.GetString("Character"); //get the name of the selected character
        foreach(var item in Characters) //for each character that we have
        {
            if (item.Name.CompareTo(selected_char)==0) //set the selected character active and set proper fields
            {
                item.Character.SetActive(true);
                Context.Data.Player = item.Character.GetComponent<PlayerController>(); //introduce the selected character as the player
                MainCamera.target = Context.Data.Player.LookingPoint.GetComponent<Transform>(); //make the camera follow the selected character
                Context.Data.Player.transform.position = StartPosition.transform.position; //put in the starting position
            }
            else
                item.Character.SetActive(false); // deactivate other characters
        }

    }
    void Update()
    {
        var player = Context.Data.Player;
        if (player.IsDead) return; // if player is dead don't do anything
        if (Input.GetKeyDown(KeyCode.Space) && !player.IsMagic && !player.IsJumping)
        {
            player.StartJumping(); //jumping
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            Fire(Garbages.Metal); //cast spell made of metal
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Fire(Garbages.Paper); //cast spell made of paper
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            Fire(Garbages.Plastic); // cast spell made of plastic
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.LeftArrow))) && player.CanTurn)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                player.Turn("right"); //choose the right road
            else
                player.Turn("left"); // choose the left road
            //tell the world generator which way the character is facing
            GenerateWorld.dummyTraveller.transform.forward = -player.transform.forward;
            //build the next platform
            GenerateWorld.RunDummy();
            //if we are in a T shape platform, don't make the next platform and wait for the player to decide
            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            //move player slightly to the left
            player.transform.Translate(-this.MovementAmount, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // move player slightly to the right
            player.transform.Translate(this.MovementAmount, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            //pause the game
            PauseGame();
        }
    }
    public void FixedUpdate()
    {
        Context.Data.Speed += Context.Data.Acceleration; //speeding up
        var animSpeed = Context.Data.Player.GetComponent<Animator>().speed; 
        animSpeed += (animSpeed * Context.Data.Acceleration)%1;
        Context.Data.Player.GetComponent<Animator>().speed = animSpeed; //make the animation faster
    }
    
    public CharacterItem[] Characters; // the available characters
    public string MainScene = "MainScene"; // the name of the main scene
    public GameObject StartPosition; // starting position for the player
    public float MovementAmount = 0.5f; // the amount that the player goes left or right each time
    public int ResetTimeout = 2; //the time to revive after death
    public int MaxLives = 3; // the maximum number of lives
    public GameObject GameOverPanel; //the GameOver panel to be shown
    public Text FinalScore; // the final score
    public Text FinalMsg; // the message in the game over panel
    public GameObject[] OtherPanels; //a list of panels in the canvas
    public GameObject PausePanel; // this panel is being shown whenever player pause the game
    public SmoothFollow MainCamera;
    public void Run() //it starts generating platforms
    {
        GenerateWorld.RunDummy();
    }

    internal void LostOneLife()//shows a message after each death
    {
        Context.Data.Message = "Be Carefull, You Lost One Life!";
    }

    public void BackToMainMenu() //loads the mainmanu
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    public void Fire(Garbages garbageType) // fires a spell from a certain type
    {
        if (Context.Data.GetScore(garbageType) >= Context.Data.MagicCost) // if we have enough resources
        {
            Context.Data.RemoveGarbage(garbageType, Context.Data.MagicCost);
            Context.Data.Player.StartMagic(garbageType);
        }
        else
            NotEnoughGarbage(garbageType); // if the resources are not enough shows a message
    }

    public void GameOver() // in case of gameover
    {
        Context.Data.Player.Die();  // play death animation
        GameOverPanel.SetActive(true); // show the gameover panel and the score
        FinalScore.text = Context.Data.TotalScore.ToString();
        if (PlayerPrefs.GetInt("HighestScore") == Context.Data.TotalScore)
            FinalMsg.text = "NEW HIGHSCORE!";
        foreach (var panel in OtherPanels)
            panel.SetActive(false);
    }

    public void StartGameScene() // loads the game scene
    {
        SceneManager.LoadScene(MainScene, LoadSceneMode.Single);
    }

    public void Die() // after each death
    {
        Context.Data.Lives -= 1; //remove one life
        if (Context.Data.Lives > 0) //if lives left
        {
            Context.Data.Player.Die(); //show dead animation
            Context.Manager.LostOneLife(); //show message
            Invoke("StartGameScene", ResetTimeout); // start the game again after a certain time
        }
        else
            Context.Manager.GameOver(); // if no life is left, then game is over
    }

    internal void WrongSpell() // message for wrong type of spell
    {
        Context.Data.Message = "Wrong type of garbage!";
    }
    public void NotEnoughGarbage(Garbages garbageType) // message for not having enough resources
    {
        Context.Data.Message = "You don't have enough "+garbageType;
    }
    public void PauseGame()
    {
        Time.timeScale = 0; //pauses the game
        PausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1; // resume the game after pause 
        PausePanel.SetActive(false);
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
}
