using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    /// <summary>
    /// shows the ramining lives on the screen
    /// </summary>
    // Start is called before the first frame update
    public GameObject[] Hearts; // the gameobjects indacting the lives
    void Start() 
    {
        lives = (int)Context.Data.Lives;
        UpdateLives(lives); //initializing the lives on the screen
    }
    private int lives = 0;
    // Update is called once per frame
    void Update()
    {
        if (lives != (int)Context.Data.Lives)
        {
            lives = (int)Context.Data.Lives;
            UpdateLives(lives); // if the number of lives has changed it updates it
        }
    }
    public void UpdateLives(int count)
    {
        // updates the hearts on the screen
        foreach(var heart in Hearts)
        {
            heart.SetActive(count>0);
            count--;
        }
    }
}
