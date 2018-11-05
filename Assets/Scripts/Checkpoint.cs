using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float yOffset = 5.0f;

    private GameObject player;
    private bool updateCheckpoint = false;
    private PlayerMovement playerMovement;

    public float YOffset
    {
        get { return yOffset; }
    }

    // Use this for initialization
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(updateCheckpoint)
        {
            playerMovement.RespawnPos = new Vector3(transform.position.x, (transform.position.y + yOffset), transform.position.z);
            updateCheckpoint = false;

            //Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            updateCheckpoint = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            updateCheckpoint = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            updateCheckpoint = false;
        }
    }
}
