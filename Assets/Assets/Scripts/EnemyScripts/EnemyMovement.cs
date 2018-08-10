using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour {

    List<BoxCollider2D> enemyColliders = new List<BoxCollider2D>();

    // Use this for initialization
    void Start () {
        initializeEnemyColliders();
        IgnoreEnemyCollision();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //=========================================================================
    // When enemy collides with another object, these functions are called
    //=========================================================================

    // When enemy collides
    void OnCollisionEnter2D(Collision2D col) {
    }

    // When enemy exits collision
    void OnCollisionExit2D(Collision2D col) {
    }

    //=========================================================================
    // Functions for ignoring collision between Enemy and other enemies
    //=========================================================================

    void initializeEnemyColliders() {
        foreach (Transform child in transform) {
            if (child.gameObject.GetComponent<BoxCollider2D>() != null) {
                enemyColliders.Add(child.gameObject.GetComponent<BoxCollider2D>());
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

            foreach (BoxCollider2D collider in enemyColliders) {
                Physics2D.IgnoreCollision(boxCol, collider);
            }
        }
    }
}
