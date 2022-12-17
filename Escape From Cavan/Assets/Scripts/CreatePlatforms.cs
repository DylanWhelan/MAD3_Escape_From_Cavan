using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// generate a platform and put it next to the current one
// straight line - later add the stairs, corners, bridges, 
// narrow beams, coins, walls, powerups
// a lot of those are added as prefabs on platforms. 

// add arrayof platforms, and build a better track.

public class CreatePlatforms : MonoBehaviour
{
    public static GameObject LastPlatform; // determine position of next platform
    public static GameObject phantom;

    private void Awake()
    {
        phantom = new GameObject("phantom");    // at 0,0,0
    }


    public static void RunPhantom()
    {
        GameObject p = PrefabPool.singleton.GetPlatform();
        if (p == null) return;

        if (LastPlatform != null)
        {
            
            phantom.transform.position = LastPlatform.transform.position +
                            PlayerBehaviour.ThePlayer.transform.forward * 15f;
        }
        LastPlatform = p;
        p.transform.position = phantom.transform.position;
        p.transform.rotation = phantom.transform.rotation;
        // now adjust the platform only as before
        // if stairs up - then offset plus rotate
        p.SetActive(true);
    }
}
