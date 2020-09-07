using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    /// <summary>
    /// It keeps the deathzone always in a certain distance from the current platform
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Context.Data.Player.currentPlatform)
        {
            //finds a position with a certain distance from the current platform
            Vector3 vec = new Vector3(this.transform.position.x, Context.Data.Player.currentPlatform.transform.position.y - 11, this.transform.position.z);
            //sets the position of the death zone
            this.transform.position = vec;
        }
    }
}
