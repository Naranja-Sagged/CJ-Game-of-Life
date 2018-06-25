using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    Transform playerTransform;
    Vector3 initCameraPos;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        initCameraPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = playerTransform.position + initCameraPos;
	}
}
