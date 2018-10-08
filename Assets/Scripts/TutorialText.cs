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
                bigTextPip.gameObject.SetActive(true);
                bigTextPipling.gameObject.SetActive(true);
                smallTextPip.gameObject.SetActive(false);
                smallTextPipling.gameObject.SetActive(false);
            }
            else
            {
                smallTextPip.gameObject.SetActive(true);
                smallTextPipling.gameObject.SetActive(true);
                bigTextPip.SetActive(false);
                bigTextPipling.SetActive(false);
            }

        }
    }
}

