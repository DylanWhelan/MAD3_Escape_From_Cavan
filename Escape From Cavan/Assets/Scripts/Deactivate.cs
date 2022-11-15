using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    bool deactivateScheduled = false;

    // triggered by the box collider
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (deactivateScheduled == false)
            {
                // 1.5 second delay needs to be tested
                Invoke("DeactivateObject", 1.5f);
                deactivateScheduled = true;
            }
        }
    }

    private void DeactivateObject()
    {
        this.gameObject.SetActive(false);
        deactivateScheduled = false;
    }
}
