using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// create a pool of objects at the start of the game
// more efficient that constant instantiation and deletion
// specify platforms and numbers to be used using a array of 
// prefabs set in the inspector.
[System.Serializable]   // allows this to be seen in the inspector
public class PoolItem
{
    public GameObject prefab;
    public int amount;
    public bool isExpandable;
}

public class PrefabPool : MonoBehaviour
{
    [SerializeField] GameObject theParent;
    public static PrefabPool singleton;
    // pool is a list of objects
    public List<GameObject> poolItemsToUse;
    // list of all prefabs to instantiate
    public List<PoolItem> prefabItems;

    void Awake()
    {
        singleton = this;

        poolItemsToUse = new List<GameObject>();
        foreach (PoolItem item in prefabItems)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject GO = Instantiate(item.prefab);
                GO.transform.parent = theParent.transform;
                GO.SetActive(false);
                poolItemsToUse.Add(GO);
            }
        }
    }

    // method to return another platform.
    // called from CreatePlatforms class
    public GameObject GetPlatform()
    {
        // shuffle the list for randomisation
        Utils.Shuffle(poolItemsToUse);
        for (int i = 0; i < poolItemsToUse.Count; i++)
        {
            if (!poolItemsToUse[i].activeInHierarchy)
                return poolItemsToUse[i];
        }
        // if code gets here, then check for expandable items.
        foreach (PoolItem item in prefabItems)
        {
            if (item.isExpandable)
            {
                GameObject GO = Instantiate(item.prefab);
                GO.transform.parent = theParent.transform;
                GO.SetActive(false);
                poolItemsToUse.Add(GO);
                return (GO);
            }
        }

        return null;
    }
}

public static class Utils
{
    // fisher yeats shuffle
    public static System.Random r = new System.Random();
    public static void Shuffle<T>(this IList<T> theList)
    {
        int n = theList.Count;
        while (n > 1)
        {
            n--;
            int k = r.Next(n + 1);
            T value = theList[k];
            theList[k] = theList[n];
            theList[n] = value;
        }
    }
}
