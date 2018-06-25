﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    float speedDelta = 150;
    float maxVelocity;
    bool isGrounded = false;
    Rigidbody2D rb;
    GameObject player;

    Animator animator;

    enum command {
        walkRight,
        walkLeft,
        jump
    };

    // Use this for initialization
    void Start() {
        maxVelocity = speed * 2;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();

        animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() { 
        getMovementCommands();
        maxVelocityCheck();
    }

    //Limits a character's velocity
    void maxVelocityCheck() {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    //=========================================================================
    //Checks for left, right, and jump movement
    //=========================================================================

    void getMovementCommands() {
        if (Input.GetKey(KeyCode.D)) { 
            player.transform.position = (Vector2)player.transform.position + Vector2.right * (speed / speedDelta);
            setAnimatorBool(command.walkRight);
        }

        if (Input.GetKey(KeyCode.A)) {
            player.transform.position = (Vector2)player.transform.position + Vector2.left * (speed / speedDelta);
            setAnimatorBool(command.walkLeft);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded) {
            rb.AddForce(new Vector2(0, 100f) * ((25 * speed) / speedDelta));
            setAnimatorBool(command.jump);
        }
    }

    //Directly modifies animator parameters

    void setAnimatorBool(command command) {
        switch (command) {
            case command.walkRight:
                animator.SetBool("isWalking", true);
                animator.SetBool("walkingLeft", false);
                break;
            case command.walkLeft:
                animator.SetBool("isWalking", true);
                animator.SetBool("walkingLeft", true);
                break;
            case command.jump:
                animator.SetBool("isWalking", false);
                animator.SetBool("walkingLeft", false);
                break;
        }
    }

    //=========================================================================
    //Controls whether the player is grounded or not
    //=========================================================================

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }
}