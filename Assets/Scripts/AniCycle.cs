using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniCycle : MonoBehaviour {

    [SerializeField]
    private float timeTaken;
    [SerializeField]
    private Material[] materials;


    private float timer;
    private bool switchImage;
    private Material currMaterial;

    private void Start()
    {
        currMaterial = materials[0];
    }

    // Update is called once per frame
    void Update ()
    {
        if(!switchImage)
        {
            timer += Time.deltaTime;
        }
        
        if(timer >= timeTaken)
        {
            switchImage = true;
        }

        //Cycle through the list and switch animation frames (based on materials)
        if(switchImage)
        {
            for(int i = 0; i < materials.Length; i++)
            {
                if(materials[i + 1] == null)
                {
                    currMaterial = materials[0];

                    gameObject.GetComponent<Projector>().material = currMaterial;
                    timer = 0.0f;
                    switchImage = false;
                }
                else if (currMaterial == materials[i])
                {
                    currMaterial = materials[i + 1];

                    gameObject.GetComponent<Projector>().material = currMaterial;
                    timer = 0.0f;
                    switchImage = false;
                }
            }
        }
	}
}
