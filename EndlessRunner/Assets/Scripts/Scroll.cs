using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    /// <summary>
    /// It moves the object in the opposite direction of the player in every update
    /// </summary>

    void FixedUpdate()
    {
        if (Context.Data.Player.IsDead) return;
        //moves the object in the opposite direction of the player to create the illusion of running.
        this.transform.position += Context.Data.Player.transform.forward * -Context.Data.Speed;
        var currentPlatform = Context.Data.Player.currentPlatform;
        if (currentPlatform == null) return;
        if (currentPlatform.tag == "stairsUp")
            this.transform.Translate(0, -0.06f, 0);
        if (currentPlatform.tag == "stairsDown")
            this.transform.Translate(0, 0.06f, 0);
    }
}
