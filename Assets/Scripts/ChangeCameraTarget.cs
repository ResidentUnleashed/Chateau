using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : MonoBehaviour {

    private Transform newTarget = null;
    private bool canSwitchTarget = false;

    public Transform NewTarget
    {
        get { return newTarget; }
    }

    public bool CanSwitchTarget
    {
        get { return canSwitchTarget; }
        set { canSwitchTarget = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "MoveCamera")
        {
            //Get the child transform and we can switch target
            newTarget = other.transform.GetChild(0).gameObject.transform;
            canSwitchTarget = true;
        }
    }
}
