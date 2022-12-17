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
        this.transform.Translate(0f, 0f, moveSpeed);
        // move in the opposite direction to the player, so 
        // use the forward of the player to get this.
        this.transform.position +=
            PlayerBehaviour.ThePlayer.transform.forward * moveSpeed;
        // move stairs up and down
        if (PlayerBehaviour.CurrentPlatform == null) return;
    }
}
// change this to 
// this.transform.position += PlayerBehaviour.player.transform.forward * moveSpeed
