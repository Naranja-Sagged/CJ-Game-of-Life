using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    float speedDelta = 150;
    float maxVelocity;
    bool isGrounded = false;
    Rigidbody2D rb;
    GameObject player;

    List<BoxCollider2D> playerColliders = new List<BoxCollider2D>();

    SpriteRenderer spriteRenderer;
    Animator animator;
    bool isWalking = false; //Not actual animator parameter

    enum command {
        none,
        walkRight,
        walkLeft,
        jump
    };

    // Use this for initialization
    void Start() {
        maxVelocity = speed * 2;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();

        spriteRenderer = player.GetComponent<SpriteRenderer>();

        animator = player.GetComponent<Animator>();
        animator.SetFloat("Speed", (speed / speedDelta) * 25);

        initializePlayerColliders();
    }

    // Update is called once per frame
    void Update() { 
        getMovementCommands();
        maxVelocityCheck();
    }

    //=========================================================================
    //Functions used for player movement
    //=========================================================================

    //Checks for left, right, and jump movement
    void getMovementCommands() {
        if (Input.GetKey(KeyCode.D)) { 
            player.transform.position = (Vector2)player.transform.position + Vector2.right * (speed / speedDelta);
            setAnimatorBool(command.walkRight);
        }

        else if (Input.GetKey(KeyCode.A)) {
            player.transform.position = (Vector2)player.transform.position + Vector2.left * (speed / speedDelta);
            setAnimatorBool(command.walkLeft);
        }

        else {
            setAnimatorBool(command.none);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, speed / 1.25f);
            setAnimatorBool(command.jump);
        }
    }

    //Directly modifies animator parameters
    void setAnimatorBool(command command) {
        switch (command) {
            case command.none:
                isWalking = false;
                animator.SetBool("isWalking", false);
                break;
            case command.walkRight:
                animator.SetBool("isWalking", true);
                isWalking = true;
                spriteRenderer.flipX = false;
                break;
            case command.walkLeft:
                animator.SetBool("isWalking", true);
                isWalking = true;
                spriteRenderer.flipX = true;
                break;
            case command.jump:
                if (isWalking) {
                    animator.SetBool("isWalking", true);
                }
                else {
                    animator.SetBool("isWalking", false);
                }
                spriteRenderer.flipX = false;
                break;
        }
    }

    //Limits a character's velocity
    void maxVelocityCheck() {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    //=========================================================================
    //When player collides with another object, these functions are called
    //=========================================================================

    //When player collides
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            isGrounded = true;
        }

        ignoreCollisionCheck(col);
    }

    //When player exits collision
    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }

    //=========================================================================
    //Functions for ignoring collision between Player and Enemies
    //=========================================================================

    void initializePlayerColliders() {
        foreach (Transform child in transform) {
            if (child.gameObject.GetComponent<BoxCollider2D>() != null) {
                playerColliders.Add(child.gameObject.GetComponent<BoxCollider2D>());
            }
        }
    }

    //Ignores collision between given collider (Collision2D.collider) and all of player's colliders (including children objects) if Enemy
    void ignoreCollisionCheck(Collision2D col) {
        if (col.gameObject.tag == "Enemy") {

            //Because playerColliders does not include the collider of the GameObject tagged "Player", I ignore collision of that here
            Physics2D.IgnoreCollision(col.collider, gameObject.GetComponent<BoxCollider2D>());

            foreach (Collider2D collider in playerColliders) {
                Physics2D.IgnoreCollision(col.collider, collider);
            }
        }
    }
}