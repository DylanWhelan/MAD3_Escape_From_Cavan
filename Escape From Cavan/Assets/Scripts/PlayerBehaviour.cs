using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// all my player stuff. 
// basic funtionality


public class PlayerBehaviour : MonoBehaviour
{

    public static GameObject ThePlayer;
    public static GameObject CurrentPlatform;

    private bool canTurn;
    private bool isDead = false;

    void Awake()
    {
        ThePlayer = this.gameObject;
    }

    private void Start()
    {
        CreatePlatforms.RunPhantom();
        canTurn = false;
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
            print("Turning Right");
            // check if "canTurn" is true or false.
            if (!canTurn)
            {
                // move right, but what about turning
                // for now, lateral movement
                this.transform.Translate(0.3f, 0f, 0f);
            }
            else
            {
                // turn right, reposition the phantom and add a new platform
                this.transform.Rotate(Vector3.up * 90);
                CreatePlatforms.phantom.transform.forward = this.transform.forward;
                // try translating the phantom to the front of the right turn.
                // current platform is right turn.
                CreatePlatforms.phantom.transform.position = CurrentPlatform.transform.position +
                    CreatePlatforms.phantom.transform.forward * 3.5f;
                CreatePlatforms.RunPhantom();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // move left, but what about turning
            // for now, lateral movement
            // check if "canTurn" is true or false.
            print("Turning Left");
            if (!canTurn)
            {
                // move right, but what about turning
                // for now, lateral movement
                this.transform.Translate(-0.3f, 0f, 0f);
            }
            else
            {
                // turn right, reposition the phantom and add a new platform
                this.transform.Rotate(Vector3.up * -90);
                CreatePlatforms.phantom.transform.forward = this.transform.forward;
                // try translating the phantom to the front of the right turn.
                // current platform is right turn.
                CreatePlatforms.phantom.transform.position = CurrentPlatform.transform.position +
                    CreatePlatforms.phantom.transform.forward * 3.5f;
                CreatePlatforms.RunPhantom();
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
        // be executed when the player hits the box collider
        if (other is SphereCollider)
        {
            canTurn = true;    // turn 90 degrees
            print("Can turn is true");
        }
        else if (other is BoxCollider)
        {
            //print("create platform");
            CreatePlatforms.RunPhantom();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is SphereCollider) canTurn = false;
        print("Can turn is false");
    }

}
