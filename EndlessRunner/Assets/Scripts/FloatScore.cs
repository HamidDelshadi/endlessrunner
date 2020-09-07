using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatScore : MonoBehaviour
{
    /// <summary>
    /// manages the flying score on the screen (when the player picks a garbage)
    /// </summary>
    Text text;
    float alpha = 1;
    public float AlphaDifference = 0.05f;
    public int speed = 20;
    void Start()
    {
        text = this.GetComponent<Text>();
        text.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        //it gradually fades the text out
        this.transform.Translate(0, speed, 0);
        alpha -= AlphaDifference;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        if (alpha < 0)
            Destroy(this.gameObject);
    }
}
