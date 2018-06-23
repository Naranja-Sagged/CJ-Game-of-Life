using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    float speedDelta = 150;
    GameObject player;
    CapsuleCollider2D playerCollider;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<CapsuleCollider2D>();

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.D)) {
            player.transform.position = (Vector2)player.transform.position + Vector2.right * (speed / speedDelta);
        }
        if (Input.GetKey(KeyCode.A)) { 
            player.transform.position = (Vector2)player.transform.position + Vector2.left * (speed / speedDelta);
        }
        if (Input.GetKey(KeyCode.Space)) {
            float distFromGround = playerCollider.bounds.extents.y;
            bool isGrounded = Physics.Raycast(transform.position, Vector3.down, distFromGround + 1.0f);
            if (!isGrounded) {
                player.transform.position = (Vector2)player.transform.position + Vector2.up * ((speed + 1) / speedDelta);
            }
        }
    }
}
