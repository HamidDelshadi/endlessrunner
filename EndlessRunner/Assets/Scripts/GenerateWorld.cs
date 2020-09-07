using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateWorld : MonoBehaviour
{
    //this class is responsible for making new platforms
    static public GameObject dummyTraveller;// this empty game object is used for finding the position for each new platform
    static public GameObject lastPlatform;
    static public int PlatformSize = 20; //the length of each platform
    static public int TPlatformSize = 10; // the length of the T platform (platform with 2ways is shorter)

    void Awake()
    {
        dummyTraveller = new GameObject("dummy");
    }

    public static void RunDummy()
    {
        GameObject p = Pool.singleton.GetRandom(); // it gets a random platform from the pool
        if (p == null) return;

        if (lastPlatform != null)
        {
            if(lastPlatform.tag == "platformTSection")
                //moving the dummy forward
                dummyTraveller.transform.position = lastPlatform.transform.position +
                    Context.Data.Player.transform.forward * PlatformSize;
            else
                dummyTraveller.transform.position = lastPlatform.transform.position +
                    Context.Data.Player.transform.forward * TPlatformSize;
            //moves the dummy up in case of stairs
            if (lastPlatform.tag == "stairsUp")
                dummyTraveller.transform.Translate(0, 5, 0);
        }
        // puts the new platform in place
        lastPlatform = p;
        p.SetActive(true);
        p.transform.position = dummyTraveller.transform.position;
        p.transform.rotation = dummyTraveller.transform.rotation;

        // moves the dummy down if case of downside stairs
        if (p.tag == "stairsDown")
        {
            dummyTraveller.transform.Translate(0, -5, 0);
            p.transform.Rotate(0, 180, 0);
            p.transform.position = dummyTraveller.transform.position;
        }

    }

}
