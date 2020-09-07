using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Context : MonoBehaviour
{
    /// <summary>
    /// It works as a the heart of the game by storing the data and knowing the necessary objects
    /// </summary>
    // Start is called before the first frame update


    public static GameData Data = new GameData(); //the data related to the current game
    public static void Reset() { Data = new GameData(); } // resests the game data
    public static GameManager Manager; // the manager class related to the current game
    // the name of the scenes, used for the navigation
    public static string MainScene = "MainScene"; 
    public static string CharacterSelectionScene = "SelectCharacter";
    public static string MainMenu = "Menu";
    public static string HelpScene = "Help";

    //these functions work as the single point of navigation in order to achieve encapsulation.
    public static void StartGameScene() //goes to the game scene
    {
        SceneManager.LoadScene(MainScene, LoadSceneMode.Single);
    }
    public static void StartCharacterSelectionScene() //goes to the character selection scene
    {
        SceneManager.LoadScene(CharacterSelectionScene, LoadSceneMode.Single);
    }
    public static void StartMainMenu() // goes to the main menu scene 
    {
        SceneManager.LoadScene(MainMenu, LoadSceneMode.Single);
    }
    public static void StartHelpScene() //goes to the how to play scene.
    {
        SceneManager.LoadScene(HelpScene, LoadSceneMode.Single);
    }
}
