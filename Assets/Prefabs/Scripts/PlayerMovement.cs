using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    float speedDelta = 150;

    bool isGrounded = false;
    Rigidbody2D rb;
    GameObject player;
    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        movementCheck();
    }
    //Checks for left, right, and jump movement
    void movementCheck() {
        if (Input.GetKey(KeyCode.D)) { 
            player.transform.position = (Vector2)player.transform.position + Vector2.right * (speed / speedDelta);
        }

        if (Input.GetKey(KeyCode.A)) {
            player.transform.position = (Vector2)player.transform.position + Vector2.left * (speed / speedDelta);
        }

        if (Input.GetKey(KeyCode.Space)) { 
            if (isGrounded) {
                rb.AddForce(new Vector2(0, 100f) * ((25 * speed) / speedDelta));
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            Debug.Log("enter");
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            Debug.Log("exit");
            isGrounded = false;
        }
    }
}