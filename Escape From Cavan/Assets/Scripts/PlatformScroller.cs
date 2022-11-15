using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScroller : MonoBehaviour
{
    [SerializeField] float moveSpeed = -0.1f;
    // ratio of horizontal to vertical distance travelled
    // 5 in the horizontal, 2.3 in vertical
    [SerializeField] float verticalMoveSpeed = 0.046f;

    void FixedUpdate()
    {
        //this.transform.Translate(0f, 0f, moveSpeed);
        // move in the opposite direction to the player, so 
        // use the forward of the player to get this.
        this.transform.position +=
            PlayerBehaviour.ThePlayer.transform.forward * moveSpeed;
        // move stairs up and down
        if (PlayerBehaviour.CurrentPlatform == null) return;

        if (PlayerBehaviour.CurrentPlatform.tag == "Stairs Up")
            this.transform.Translate(0, (-1 * verticalMoveSpeed), 0);
        //this.transform.Translate(0, -2.3f, 0);

        if (PlayerBehaviour.CurrentPlatform.tag == "Stairs Down")
            this.transform.Translate(0, verticalMoveSpeed, 0);
        //this.transform.Translate(0, 2.3f, 0);



        // move the platform in the -z direction.
        //if (this.tag == "Stairs Up")
        //    this.transform.Translate(0f, 0f, -moveSpeed);
        //else
        //    this.transform.Translate(0f, 0f, moveSpeed);
    }
}
// change this to 
// this.transform.position += PlayerBehaviour.player.transform.forward * moveSpeed
