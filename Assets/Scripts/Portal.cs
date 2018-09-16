using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    [SerializeField]
    private float maxTimeBetweenScenes = 2.0f;
    [SerializeField]
    private string sceneName;


    private bool hasHit = false;
    private float timer;

    private void Update()
    {
        if(hasHit)
        {
            timer += Time.deltaTime;

            if(timer > maxTimeBetweenScenes)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            hasHit = true;
        }
    }
}
