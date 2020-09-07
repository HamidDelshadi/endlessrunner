using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    /// <summary>
    /// it is used for any object that the player can pick from the ground
    /// </summary>
    public Garbages GarbageType; //the type of garbage (paper,metal, plastic)
    public GameObject scorePrefab; //this prefab is used to show the acquired point on the screen
    GameObject canvas;
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Context.Data.AddScore(GarbageType);
            SoundManager.Instance.PlaySFX(SFX.pickup); //sound effect
            GameObject scoreText = Instantiate(scorePrefab); 
            scoreText.transform.SetParent(canvas.transform, false); //show the score on screen
            this.gameObject.SetActive(false); // hide the object
        }
    }

    void OnEnable()
    {
        this.gameObject.SetActive(true); //if we want to use the platform again, this is necessary
    }
}
