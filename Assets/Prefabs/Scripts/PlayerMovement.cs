﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    float speedDelta = 150;
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.D)) {
            player.transform.position = (Vector2)player.transform.position + Vector2.right * (speed/speedDelta);
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.position = (Vector2)player.transform.position + Vector2.left * (speed/speedDelta);
        }
    }
}
