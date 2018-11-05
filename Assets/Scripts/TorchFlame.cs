using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFlame : MonoBehaviour {

    [SerializeField]
    GameObject flameIdle;
    [SerializeField]
    GameObject flameActive;
    [SerializeField]
    Transform checkpointTransform;
    [SerializeField]
    Checkpoint checkpoint;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(playerMovement.RespawnPos != (new Vector3(checkpointTransform.position.x, (checkpointTransform.position.y + checkpoint.YOffset), checkpointTransform.position.z)))
        {
            flameIdle.SetActive(true);
            flameActive.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(flameIdle != null && flameActive != null)
            {
                flameIdle.SetActive(false);
                flameActive.SetActive(true);
            }
        }
    }
}
