using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// shows the scores on the screen
    /// </summary>
    /// 
    //UI responsible for showing the score for each type of garbage
    public Text MetalCount; 
    public Text PaperCount; 
    public Text PlasticCount; 
    public Text TotalScore; // the total score
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //shows the updated score on the screen.
        MetalCount.text =  Context.Data.GetScore(Garbages.Metal).ToString();
        PaperCount.text = Context.Data.GetScore(Garbages.Paper).ToString();
        PlasticCount.text = Context.Data.GetScore(Garbages.Plastic).ToString();
        TotalScore.text = Context.Data.TotalScore.ToString();
    }
    public void Fire(string garbageType)
    {
        //the UI responsible for showing the score, is also a button to fire a spell
        // this function passes the fire event to the GameManager
        Context.Manager.Fire((Garbages)Enum.Parse(typeof(Garbages), garbageType));
    }
}
