using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// all my player stuff. 
// basic funtionality


public class PlayerBehaviour : MonoBehaviour
{

    public static GameObject ThePlayer;
    public static GameObject CurrentPlatform;

    private Lane lane;
    private bool isDead = false;

    void Awake()
    {
        ThePlayer = this.gameObject;
    }

    private void Start()
    {
        //CreatePlatforms.RunPhantom();
        lane = Lane.Middle;
        isDead = false;
    }

    void Update()
    {
        if (isDead) return;
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        // figure which key was hit.
        // space to jump, use input keys (left and right arrow)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            // check if "canTurn" is true or false.
            if (lane == Lane.Right)
            {
                print("Cant Strafe Right");
            }
            else
            {
                if (lane == Lane.Middle)
                {
                    lane = Lane.Right;
                }
                else
                {
                    lane = Lane.Middle;
                }
                this.transform.position = this.transform.right * (int)lane;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
            // check if "canTurn" is true or false.
            if (lane == Lane.Left)
            {
                print("Cant Strafe left");
            }
            else
            {
                
                if (lane == Lane.Middle)
                {
                    lane = Lane.Left;
                }
                else
                {
                    lane = Lane.Middle;
                }
                this.transform.position = this.transform.right * (int)lane;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // the capsule hits the mesh collider of the platform
        CurrentPlatform = other.gameObject;
        //print("Current is: " + CurrentPlatform.tag);
        if (other.gameObject.tag == "Death Ground")
        {
            // set boolean for player dead.
            isDead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Entered Trigger");
        //// be executed when the player hits the box collider
        //if (other is SphereCollider)
        //{
        //    canTurn = true;    // turn 90 degrees
        //    print("Can turn is true");
        //}
        //else if (other is BoxCollider)
        //{
        //    //print("create platform");
        //    CreatePlatforms.RunPhantom();
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        print("Exitted Trigger");
    }

}
