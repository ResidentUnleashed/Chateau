using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : MonoBehaviour
{
    [Tooltip("How fast the camera moves from one position to the next.")]
    [SerializeField]
    private float lerpSpeed = 1.0f;
    [SerializeField]
    private GameObject gameCamera;


    private Transform newTarget = null;
    private bool canSwitchTarget = false;
    private GameObject[] targetList;
    private bool hasMoved = false;
    private bool first = true;

    public Transform NewTarget
    {
        get { return newTarget; }
    }

    public bool CanSwitchTarget
    {
        get { return canSwitchTarget; }
        set { canSwitchTarget = value; }
    }

    public bool HasMoved
    {
        get { return hasMoved; }
        set { hasMoved = value; }
    }

    private void Awake()
    {
        targetList = GameObject.FindGameObjectsWithTag("MoveCamera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "MoveCamera")
        {
            if (!hasMoved && !first)
            {
                //Set all to active
                for (int i = 0; i < targetList.Length; i++)
                {
                    targetList[i].SetActive(true);
                }
            }

            //Get the child transform and we can switch target
            newTarget = other.transform.GetChild(0).gameObject.transform;
            hasMoved = true;

            //Deactivate current
            for (int i = 0; i < targetList.Length; i++)
            {
                if (other.transform == targetList[i].transform)
                {
                    first = false;
                    StartCoroutine(Transition());
                    targetList[i].SetActive(false);
                }
            }
        }
    }

    private IEnumerator Transition()
    {
        //Create the float t (time)
        float t = 0.0f;

        //Get our starting pos
        Vector3 startPos = gameCamera.transform.position;

        while (t < 1.0f)
        {
            //Clock
            t += Time.deltaTime * (Time.timeScale / lerpSpeed);

            //Lerp to new destination
            gameCamera.transform.position = Vector3.Lerp(startPos, newTarget.position, t);

            //Return
            yield return 0;
        }
    }
}