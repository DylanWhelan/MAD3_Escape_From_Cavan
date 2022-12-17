using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static GameObject playerCamera;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = player.transform.position.x;
        transform.position = newPosition;
    }
}
