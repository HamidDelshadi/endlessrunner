using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    /// <summary>
    /// stores a prefab to be instantiated as a platform
    /// </summary>
    public GameObject prefab;
    public int amount; // the number of gameobject allowed to be made
    public bool expandable; //is it allowed to make more than amount
}

public class Pool : MonoBehaviour
{
    /// <summary>
    /// a pool of platforms to draw from
    /// </summary>
    public static Pool singleton;
    private int ItemIndex = 0; //the index in InitItems list
    public List<PoolItem> items; // list of items to be instantiated
    public List<GameObject> InitItems; // list of items to be instantiated in the begining
    private List<GameObject> pooledItems; // list of Instanstiated Items

    void Awake()
    {
        singleton = this;
        Utils.Shuffle(InitItems); //shuffles items to make the world random
        pooledItems = new List<GameObject>();
        foreach (PoolItem item in items) // Instantiate and saves platforms
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
            }
        }
    }

    public GameObject GetRandom()
    {
        //returns a random platform

        if (ItemIndex < InitItems.Count) // if we are at the begining of the game it returns specified items in the InitItems
        {
            GameObject obj = Instantiate(InitItems[ItemIndex]);
            obj.SetActive(false);
            ItemIndex++;
            return obj;
        }
            
        Utils.Shuffle(pooledItems); //shuffles the items to get a random world

        for (int i = 0; i < pooledItems.Count; i++)
        {
            if (!pooledItems[i].activeInHierarchy)
            {
                return pooledItems[i]; //picks the first one that is not in the hierarchy
            }
        }

        foreach (PoolItem item in items) 
        {
            if (item.expandable) // if it ran out of items, use the first expandable one.
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }

        return null;
    }
}

public static class Utils 
{
    /// <summary>
    /// some utility functions necessary in the game
    /// </summary>
    public static System.Random r = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        //it shuffles a list and returns it.
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = r.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}


