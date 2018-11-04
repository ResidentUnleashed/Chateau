using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{

    [SerializeField]
    private GameObject smallTextPip;

    [SerializeField]
    private GameObject smallTextPipling;

    [SerializeField]
    private GameObject bigTextPip;

    [SerializeField]
    private GameObject bigTextPipling;

    [SerializeField]
    private Transform[] transforms;

    [SerializeField]
    private float range;

    // Use this for initialization
    void Start()
    {
        bigTextPip.SetActive(false);
        bigTextPipling.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GetInactiveInRadius();
    }


    void GetInactiveInRadius()
    {
        foreach (Transform tr in transform)
        {
            float distanceSqr = (transform.position - tr.position).sqrMagnitude;

            if (distanceSqr < range)
            {
                if(bigTextPip != null)
                {
                    bigTextPip.gameObject.SetActive(true);
                }
                
                if(bigTextPipling != null)
                {
                    bigTextPipling.gameObject.SetActive(true);
                }
                
                if(smallTextPip != null)
                {
                    smallTextPip.gameObject.SetActive(false);
                }
                
                if(smallTextPipling != null)
                {
                    smallTextPipling.gameObject.SetActive(false);
                }
                
            }
            else
            {
                if (smallTextPip != null)
                {
                    smallTextPip.gameObject.SetActive(true);
                }

                if (smallTextPipling != null)
                {
                    smallTextPipling.gameObject.SetActive(true);
                }

                if (bigTextPip != null)
                {
                    bigTextPip.gameObject.SetActive(false);
                }

                if (bigTextPipling != null)
                {
                    bigTextPipling.gameObject.SetActive(false);
                }
            }

        }
    }
}

