using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    [SerializeField]
    private float maxTimeBetweenScenes = 1.2f;
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private Animator playerAni = null;
    [SerializeField]
    bool suck2 = false;



    private bool hasHit = false;
    private float timer;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(hasHit)
        {
            playerMovement.enabled = false;

            if(!suck2)
            {
                playerAni.Play("Suck");
            }
            else
            {
                playerAni.Play("Suck2");
            }
            

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
