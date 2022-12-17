using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// all my player stuff. 
// basic funtionality


public class PlayerBehaviour : MonoBehaviour
{

    public static GameObject ThePlayer;
    public static Rigidbody rb;
    public static GameObject CurrentPlatform;

    public int leftmostLane = -1;
    public int rightmostLane = 1;

    private Lane lane;
    private int targetLane;
    private bool isCaught = false;

    void Awake()
    {
        ThePlayer = this.gameObject;
        rb = GetComponent<Rigidbody>();
        targetLane = 0;
    }

    private void Start()
    {
        CreatePlatforms.RunPhantom();
        isCaught = false;
    }

    void Update()
    {
        if (isCaught) return;
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (targetLane >= rightmostLane)
            {
                print("Cant Strafe Right");
            }
            else
            {
                targetLane++;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (targetLane <= leftmostLane)
            {
                print("Cant Strafe Left");
            }
            else
            {
                targetLane--;
            }

            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            print("Up Arrow Was Pressed");
            if (rb.position.y < 0.5) 
            {    
            rb.AddForce(transform.up * 6f, ForceMode.Impulse);
            }
        }
        if (rb.position.x != targetLane)
        {
            float newX = 0;
            if (rb.position.x < targetLane)
            {
                newX = rb.position.x + 4 * Time.deltaTime;
                if (newX > targetLane)
                {
                    newX = targetLane;
                }
            }
            else if (rb.position.x > targetLane)
            {
                newX = rb.position.x - 4 * Time.deltaTime;
                if (newX < targetLane)
                {
                    newX = targetLane;
                }
            }
            
            Vector3 newPosition = rb.position;
            newPosition.x = newX;
            rb.MovePosition(newPosition);
        }
        if (rb.position.z < 0)
        {
            float newZ = rb.position.z + 4 * Time.deltaTime;
            if (newZ > 0)
            {
                newZ = 0;
            }
            Vector3 newPosition = rb.position;
            newPosition.z = newZ;
            rb.MovePosition(newPosition);
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
            isCaught = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("Entered Trigger");
        // be executed when the player hits the box collider
        if (other is BoxCollider)
        {
           //print("create platform");
           CreatePlatforms.RunPhantom();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //print("Exitted Trigger");
    }

}
