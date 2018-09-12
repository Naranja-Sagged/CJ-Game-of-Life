using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    bool isWalking = false; // Not actual animator parameter

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
       
        InitializePlayerColliders();

        IgnoreEnemyCollision();
    }

    // Update is called once per frame
    void Update() { 
        GetMovementCommands();
        MaxVelocityCheck();
    }

    //=========================================================================
    // Functions used for player movement
    //=========================================================================

    // Checks for left, right, and jump movement
    void GetMovementCommands() {
        if (Input.GetKey(KeyCode.D)) { 
            player.transform.position = (Vector2)player.transform.position + Vector2.right * (speed / speedDelta);
            SetAnimatorBool(command.walkRight);
        }

        else if (Input.GetKey(KeyCode.A)) {
            player.transform.position = (Vector2)player.transform.position + Vector2.left * (speed / speedDelta);
            SetAnimatorBool(command.walkLeft);
        }

        else {
            SetAnimatorBool(command.none);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, speed / 1.25f);
            SetAnimatorBool(command.jump);
        }
    }

    // Directly modifies animator parameters
    void SetAnimatorBool(command command) {
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

    // Limits a character's velocity
    void MaxVelocityCheck() {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    //=========================================================================
    // When player collides with another object, these functions are called
    //=========================================================================

    // When player collides
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    // When player exits collision
    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }

    //=========================================================================
    // Functions for ignoring collision between Player and Enemies
    //=========================================================================

    void InitializePlayerColliders() {
        foreach (Transform child in transform) { // Goes through every child object of enemy
            if (child.gameObject.GetComponent<BoxCollider2D>() != null && !child.gameObject.GetComponent<BoxCollider2D>().isTrigger) { // Checks if object is a non-isTrigger collider
                AddPlayerCollidersToList(child);
            }
        }
    }

    // Recursively checks if the child objects are colliders, and if so, add them to enemyColliders
    void AddPlayerCollidersToList(Transform child) {
        playerColliders.Add(child.gameObject.GetComponent<BoxCollider2D>()); // add 2 list :3

        foreach (Transform grandChild in child) { // go deeper . . .
            if (grandChild.gameObject.GetComponent<BoxCollider2D>() != null && !child.gameObject.GetComponent<BoxCollider2D>().isTrigger) { // Currently, grandchild (and beyond) colliders of enemy will always have non-isTrigger parents. Change if this fact changes.
                AddPlayerCollidersToList(grandChild);
            }
            else {
                continue;
            }
        }
    }

    // Checks all game objects in the scene for enemy and ignores collision with player
    void IgnoreEnemyCollision() {
        foreach (GameObject obj in GameManager.allObjects) {
            if (obj.gameObject.tag == "Enemy") {
                IgnoreCollision(obj);
            }
        }
    }

    // Ignore all of a given object's colliders whole list playerColliders
    void IgnoreCollision(GameObject obj) {
        foreach (BoxCollider2D boxCol in obj.GetComponentsInChildren<BoxCollider2D>()) {
            // Because playerColliders does not include the collider of the GameObject tagged "Player", I ignore collision of that here
            Physics2D.IgnoreCollision(boxCol, gameObject.GetComponent<BoxCollider2D>());

            foreach (BoxCollider2D collider in playerColliders) {
                Physics2D.IgnoreCollision(boxCol, collider);
            }
        }
    }
}