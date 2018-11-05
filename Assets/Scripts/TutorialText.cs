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
    private Transform transformPip;

    [SerializeField]
    private Transform transformPipling;

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
        TextPip();
        TextPipling();
    }


    private void TextPip()
    {
        //Distance between two objects
        Vector3 vecBetween = transform.position - transformPip.position;

        float distance = vecBetween.magnitude;

        if (distance < range)
        {
            if (smallTextPip != null)
            {
                smallTextPip.gameObject.SetActive(false);
            }

            if (bigTextPip != null)
            {
                bigTextPip.gameObject.SetActive(true);
            }

        }
        else
        {
            if (smallTextPip != null)
            {
                smallTextPip.gameObject.SetActive(true);
            }

            if (bigTextPip != null)
            {
                bigTextPip.gameObject.SetActive(false);
            }
        }
    }

    private void TextPipling()
    {
        //Distance between two objects
        Vector3 vecBetween = transform.position - transformPipling.position;

        float distance = vecBetween.magnitude;

        if (distance < range)
        {
            if (smallTextPipling != null)
            {
                smallTextPipling.gameObject.SetActive(false);
            }

            if (bigTextPipling != null)
            {
                bigTextPipling.gameObject.SetActive(true);
            }

        }
        else
        {
            if (smallTextPipling != null)
            {
                smallTextPipling.gameObject.SetActive(true);
            }

            if (bigTextPipling != null)
            {
                bigTextPipling.gameObject.SetActive(false);
            }
        }
    }
}

