using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HelpManager : MonoBehaviour
{
    /// <summary>
    /// it shows the how to play panels to the user
    /// </summary>
    public GameObject[] panels; //panels to be shown in order
    int ActivePanel = 0;// the current panel
    // Start is called before the first frame update
    void Start()
    {
        ShowPanel(0);
    }
    public void ShowPanel(int id)
    {
        if (id >= panels.Length) //if this is the last panel it moves to the main menu
            Context.StartMainMenu();
        else
        {
            //shows the new panel
            ActivePanel = id;
            foreach (var panel in panels)
                panel.SetActive(false);
            panels[id].SetActive(true);
        }
    }
    public void ShowNext()
    {
        //shows the next panel
        ShowPanel(ActivePanel + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
