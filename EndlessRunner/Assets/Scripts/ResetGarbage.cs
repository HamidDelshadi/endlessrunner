using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGarbage : MonoBehaviour
{
    /// <summary>
    /// it resets a garbage platforme to their original position if ther are being used again.
    /// </summary>
    List<GameObject> garbages; //list of garbages on the platform

    public void GetChildObject(Transform parent, string _tag, List<GameObject> objs)
    {
        // find all the children of an object with a certain tag and adds them to objs list
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                objs.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag,objs);
            }
        }
    }

    private void Awake()
    {
        garbages = new List<GameObject>();
        GetChildObject(transform, "garbage", garbages); //find all the garbages on the platform
    }
    private void OnEnable()
    {
        //it makes the platform ready to be used again.
        foreach (GameObject g in garbages)
            g.SetActive(true); //activates all garbages
    }
}


