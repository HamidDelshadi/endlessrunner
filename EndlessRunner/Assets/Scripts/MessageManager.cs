using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    /// <summary>
    /// it shows messages to the user, the message is shown by a semitransparent banner
    /// </summary>
    // Start is called before the first frame update
    public Text MsgTxt;
    public GameObject MsgPanel;
    public float ShowTime = 0.3f; //the delay before clearing the message
    private float PassedTime = 0.0f; // the amount of time that the message has been shown so far
    void Start()
    {
        MsgTxt.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!string.IsNullOrEmpty(Context.Data.Message)) //if there is a new message
        {
            if (string.IsNullOrEmpty(MsgTxt.text))
                MsgTxt.text = Context.Data.Message; //show the message
            else
                MsgTxt.text +="\n"+ Context.Data.Message; //or add it to the previous message
            Context.Data.Message = "";
            MsgPanel.SetActive(true);
            PassedTime = 0;
        }
        else if (MsgPanel.activeSelf)
        {
            PassedTime += Time.deltaTime; //current show time
            if(PassedTime>=ShowTime)
            {
                MsgTxt.text = "";
                PassedTime = 0;
                MsgPanel.SetActive(false); // the message will be hidden after a certain amount of time
            }
        }


    }
}
