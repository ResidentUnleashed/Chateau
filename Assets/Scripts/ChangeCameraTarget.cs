using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : MonoBehaviour {

    private Transform newTarget = null;
    private bool canSwitchTarget = false;
    private List<GameObject> targetList;

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
        if (other.transform.tag == "MoveCamera")
        {
            //Set all to active
            for (int i = 0; i < targetList.Count; i++)
            {
                targetList[i].SetActive(true);
            }

            //Get the child transform and we can switch target
            newTarget = other.transform.GetChild(0).gameObject.transform;

            //Deactivate current
            for (int i = 0; i < targetList.Count; i++)
            {
                if(other.transform == targetList[i].transform)
                {
                    canSwitchTarget = true;
                    targetList[i].SetActive(false);
                }
            }
        }
    }
}
