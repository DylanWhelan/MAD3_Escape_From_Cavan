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
    [SerializeField] int NumberOfPlatforms = 10;
    [SerializeField] GameObject[] platforms;
    public static GameObject LastPlatform; // determine position of next platform
    public static GameObject phantom;
    public static bool passedTurn = false;

    private void Awake()
    {
        phantom = new GameObject("phantom");    // at 0,0,0
    }

    //void Start()
    //{
    //    //CreateBasicSetOfPlatforms();
    //    //CreatePlatformsUsingPhantom();
    //}


    public static void RunPhantom()
    {
        GameObject p = PrefabPool.singleton.GetPlatform();
        if (p == null) return;
        //print("Getting platform: " + p.tag);

        // get the first platform, we already have a static platform on the screen.
        if (LastPlatform != null)
        {
            // stop generating the platform in mid air.
            // need a boolean so this only runs once.
            if ((LastPlatform.tag).Contains("Turn") && passedTurn == false)
            {
                //print("Don't generate");
                passedTurn = true;  // skip this next time around.
                return;
            }
            print("last platform is: " + LastPlatform.tag);

            // need to adjust the position of the phantom.
            phantom.transform.position = LastPlatform.transform.position +
                            PlayerBehaviour.ThePlayer.transform.forward * 5f;
            // will need to adjust for the stairs, either up or down as well.
            if (LastPlatform.tag == "Stairs Up")
            {
                //phantom.transform.Translate(new Vector3(0, 2.2f, 0));
                phantom.transform.Translate(new Vector3(0, 1.3f, 0));
            }
            else if (LastPlatform.tag == "Stairs Down")
            {
                //phantom.transform.Translate(new Vector3(0, -2.2f, 0));
                phantom.transform.Translate(new Vector3(0, -1.1f, 0));
            }
            else if (LastPlatform.tag == "Right Turn")
            {
                // repostition the phantom, add the new platform in front of the right turn
                phantom.transform.Translate(new Vector3(0, 0, -1.4f));
            }
            else if (LastPlatform.tag == "Left Turn")
            {
                // repostition the phantom, add the new platform in front of the right turn
                phantom.transform.Translate(new Vector3(0, 0, -1.4f));
            }
        }
        LastPlatform = p;
        p.transform.position = phantom.transform.position;
        p.transform.rotation = phantom.transform.rotation;
        // now adjust the platform only as before
        if (p.tag == "Stairs Down")
        {
            // move the phantom in the y direction
            //phantom.transform.Translate(new Vector3(0, -2.2f, 0));
            p.transform.position += new Vector3(0f, -1.3f, 0f);
        }
        // if stairs up - then offset plus rotate
        else if (p.tag == "Stairs Up")
        {
            //phantom.transform.Translate(new Vector3(0, 2.2f, 0));
            p.transform.position += new Vector3(0f, 0.93f, 0f);
            p.transform.Rotate(0, 180, 0);
        }
        else if (p.tag == "Right Turn")
        {
            p.transform.Translate(new Vector3(0, 0, -1.5f));
            passedTurn = false; // reset for the next time.
            //phantom.transform.Translate(new Vector3(-1.5f, 0, -1.5f));
            //phantom.transform.Rotate(0, 90, 0); // turns right
        }
        else if (p.tag == "Left Turn")
        {
            p.transform.Translate(new Vector3(0, 0, -1.5f));
            p.transform.Rotate(0, 90, 0);
            passedTurn = false; // reset for the next time.
            //phantom.transform.Translate(new Vector3(1.5f, 0, -1.5f));
            //phantom.transform.Rotate(0, -90, 0); // turns right
        }
        p.SetActive(true);


    }




    public static void UsePhantom()
    {
        GameObject p = PrefabPool.singleton.GetPlatform();    // return a platform from the pool.
        if (p == null) return; // fail safe

        if (LastPlatform != null)
        {
            // position the phantom one position in front
            phantom.transform.position = LastPlatform.transform.position +
                                        PlayerBehaviour.ThePlayer.transform.forward * 5;
            if (LastPlatform.tag == "Stairs Up") // reposition the phantom for placing the platform
            {
                //phantom.transform.Translate(new Vector3(0, 2.2f, 0));
                phantom.transform.Translate(new Vector3(0, 1.1f, 0));
            }
            else if (LastPlatform.tag == "Stairs Down")
            {
                //phantom.transform.Translate(new Vector3(0, -2.2f, 0));
                phantom.transform.Translate(new Vector3(0, -1.1f, 0));
            }
        }

        LastPlatform = p;
        p.transform.position = phantom.transform.position;
        p.transform.rotation = phantom.transform.rotation;

        if (p.tag == "Stairs Down")
        {
            // move the phantom in the y direction
            //phantom.transform.Translate(new Vector3(0, -2.2f, 0));
            p.transform.position += new Vector3(0f, -1.3f, 0f);
            // change the y value for pos
            //pos.y += -2.2f;
        }
        // if stairs up - then offset plus rotate
        else if (p.tag == "Stairs Up")
        {
            //phantom.transform.Translate(new Vector3(0, 2.2f, 0));
            p.transform.position += new Vector3(0f, 0.93f, 0f);
            p.transform.Rotate(0, 180, 0);
            //pos.y += 2.2f;
        }
        else if (p.tag == "Right Turn")
        {
            p.transform.Translate(new Vector3(0, 0, -1.5f));
            phantom.transform.Translate(new Vector3(-1.5f, 0, -1.5f));
            phantom.transform.Rotate(0, 90, 0); // turns right
        }
        else if (p.tag == "Left Turn")
        {
            p.transform.Translate(new Vector3(0, 0, -1.5f));
            p.transform.Rotate(0, 90, 0);
            phantom.transform.Translate(new Vector3(1.5f, 0, -1.5f));
            phantom.transform.Rotate(0, -90, 0); // turns right
        }
        p.SetActive(true);
    }

    /// <summary>
    /// use the phantom to position the platforms.
    /// manage the rotation as well - transform
    /// </summary>
    private void CreatePlatformsUsingPhantom()
    {
        int platformNumber;
        GameObject p;

        // fix first position of phantom
        //phantom.transform.position =
          //  PlayerBehaviour.ThePlayer.transform.forward * 5;

        for (int i = 0; i < NumberOfPlatforms; i++)
        {
            platformNumber =
                UnityEngine.Random.Range(0, platforms.Length);
            // set to the correct position in the front
            //p = Instantiate(platforms[platformNumber], 
            //                phantom.transform.position, 
            //                phantom.transform.rotation);
            //pos.z += 5;
            // get platform from the pool
            p = PrefabPool.singleton.GetPlatform();
            //if (p == null) return; // keep for later
            p.transform.position = phantom.transform.position;
            p.transform.rotation = phantom.transform.rotation;


            // stairs down - then offset
            if (p.tag == "Stairs Down")
            {
                // move the phantom in the y direction
                phantom.transform.Translate(new Vector3(0, -2.2f, 0));
                p.transform.position += new Vector3(0f, -1.3f, 0f);
                // change the y value for pos
                //pos.y += -2.2f;
            }
            // if stairs up - then offset plus rotate
            else if (p.tag == "Stairs Up")
            {
                phantom.transform.Translate(new Vector3(0, 2.2f, 0));
                p.transform.position += new Vector3(0f, 0.93f, 0f);
                p.transform.Rotate(0, 180, 0);
                //pos.y += 2.2f;
            }
            else if (p.tag == "Right Turn")
            {
                p.transform.Translate(new Vector3(0, 0, -1.5f));
                phantom.transform.Translate(new Vector3(-1.5f, 0, -1.5f));
                phantom.transform.Rotate(0, 90, 0); // turns right
            }
            else if (p.tag == "Left Turn")
            {
                p.transform.Translate(new Vector3(0, 0, -1.5f));
                p.transform.Rotate(0, 90, 0);
                phantom.transform.Translate(new Vector3(1.5f, 0, -1.5f));
                phantom.transform.Rotate(0, -90, 0); // turns right
            }
            p.SetActive(true);
            // move the phantom forward for now
            phantom.transform.Translate(Vector3.forward * 5f);
        }   // end of for

    }






    private void CreateBasicSetOfPlatforms()
    {
        Vector3 pos = new Vector3(0, 0, 5);
        int platformNumber;
        GameObject p;

        for (int i = 0; i < NumberOfPlatforms; i++)
        {
            platformNumber = UnityEngine.
Random.Range(0, platforms.Length);
            // create a new platform using the prefab
            // set to the correct position in the front
            p = Instantiate(platforms[platformNumber], pos, Quaternion.identity);
            pos.z += 5;
            // stairs down - then offset
            if (p.tag == "Stairs Down")
            {
                p.transform.position += new Vector3(0f, -1.3f, 0f);
                // change the y value for pos
                pos.y += -2.2f;
            }
            // if stairs up - then offset plus rotate
            else if (p.tag == "Stairs Up")
            {
                p.transform.position += new Vector3(0f, 0.93f, 0f);
                p.transform.Rotate(0, 180, 0);
                pos.y += 2.2f;
            }
        }
    }
}
