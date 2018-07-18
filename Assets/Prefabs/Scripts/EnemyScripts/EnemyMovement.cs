using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    List<BoxCollider2D> enemyColliders = new List<BoxCollider2D>();

    // Use this for initialization
    void Start () {
        initializeEnemyColliders();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //=========================================================================
    //When enemy collides with another object, these functions are called
    //=========================================================================

    //When enemy collides
    void OnCollisionEnter2D(Collision2D col) {
        ignoreCollisionCheck(col);
    }

    //When enemy exits collision
    void OnCollisionExit2D(Collision2D col) {
    }

    //=========================================================================
    //Functions for ignoring collision between Enemy and other enemies
    //=========================================================================

    void initializeEnemyColliders() {
        foreach (Transform child in transform) {
            if (child.gameObject.GetComponent<BoxCollider2D>() != null) {
                enemyColliders.Add(child.gameObject.GetComponent<BoxCollider2D>());
            }
        }
    }

    //Ignores collision between given collider (Collision2D.collider) and all of player's colliders (including children objects) if Enemy
    void ignoreCollisionCheck(Collision2D col) {
        if (col.gameObject.tag == "Enemy") {

            //Because enemyColliders does not include the collider of the GameObject tagged "Enemy", I ignore collision of that here
            Physics2D.IgnoreCollision(col.collider, gameObject.GetComponent<BoxCollider2D>());

            foreach (Collider2D collider in enemyColliders) {
                Physics2D.IgnoreCollision(col.collider, collider);
            }
        }
    }
}
