using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayerManager : MonoBehaviour
{
    /// <summary>
    /// Manages the character selecting scene
    /// </summary>
    // Start is called before the first frame update
    public CharacterItem[] Characters; //all the character that are available
    public Text CharacterName; //the name of the selected character
    public void Awake()
    {
        if (!PlayerPrefs.HasKey("Character")) //if this is the first time, use the default character
            PlayerPrefs.SetString("Character", "Johnny");
        string selected_char = PlayerPrefs.GetString("Character"); // read the selected character from playerPrefs
        ActiveSelected(selected_char);
   }

    public void ActiveSelected(string name)
    {
        //it selects and shows a character
        CharacterName.text = name;
        PlayerPrefs.SetString("Character", name);
        foreach (var item in Characters)
        {
            if (item.Name.CompareTo(name) == 0)
                item.Character.SetActive(true);
            else
                item.Character.SetActive(false);
        }
    }
    public void Play()
    {
        //starts the game
        Context.StartGameScene();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
