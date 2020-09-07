using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public enum Garbages
{
    /// <summary>
    /// the main types of garbage used in the game.
    /// </summary>
    Plastic,
    Metal,
    Paper,
}

public class GameData
{
    /// <summary>
    /// stores the data related to the current game
    /// it must be cleared before restarting the game.
    /// </summary>
    public string Message = ""; // this message is shown on the screen automatically.
    public int BaseScore = 10; // the score for picking up garbage
    public int MagicCost = 30; // the cost of casting spell, it will be reduced from the amount of picked up garbage.
    public float Speed = 0.075f; // the speed at begining of the game
    public float Acceleration = 0.000005f; //the acceleration through the time.
    // the number of picked up garbage
    private Dictionary<Garbages, int> scores = new Dictionary<Garbages, int>();
    public PlayerController Player;// a reference to the current player object
    private int? lives = null;
    public int? Lives // the number of remaining lives
    {
        set
        {
            lives = value;           
        }
        get { return lives; }
    }
    // any object with the following tags kills the player
    public List<string> DeadlyTags = new List<string>() { "Fire", "Wall", "DeathZone" };


    public int TotalScore = 0; // the total score of the player in the current game.


    public Dictionary<Garbages, int> Scores // the number of picked up garbage stored by type
    {
        set
        {
            scores = value;
        }
        get
        {
            return scores;
        }
    }

    internal void RemoveGarbage(Garbages garbageType, int cost)
    {
        //this is used for removing garbage to make a spell
        if (Scores.ContainsKey(garbageType))
             Scores[garbageType]-= cost;
    }

    //if the game is over (lives==0) then this will be true
    public bool IsDead { get { if (Lives == 0) return true; return false; } }

    //adds score for picking up garbage
    public void AddScore(Garbages garbageType)
    {
        TotalScore += BaseScore;
        if (!Scores.ContainsKey(garbageType))
            Scores.Add(garbageType, BaseScore);
        else
            Scores[garbageType] += BaseScore;
        PlayerPrefs.SetInt("LastScore", TotalScore);
        if (PlayerPrefs.HasKey("HighestScore"))
        {
            if (PlayerPrefs.GetInt("HighestScore") < TotalScore)
                PlayerPrefs.SetInt("HighestScore", TotalScore);
        }
        else
            PlayerPrefs.SetInt("HighestScore", TotalScore);
    }

    // returns the number of picked up garbage of a certain type.
    public int GetScore(Garbages garbageType)
    {
        if (!Scores.ContainsKey(garbageType))
            return 0;
        else
            return Scores[garbageType];
    }
}