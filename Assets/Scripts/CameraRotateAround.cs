using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAround : MonoBehaviour {

    [SerializeField]
    private GameObject target = null;
    [SerializeField]
    private float angle = 15.0f;

	void Awake()
	{
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(target.transform.position, Vector3.down, Time.deltaTime * angle);
	}
}
