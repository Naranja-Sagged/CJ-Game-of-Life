using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script runs continuously while scene is active
public class GameMananger : MonoBehaviour {

    GameObject[] pauseObjects;
    bool paused = false;


    // Use this for initialization
    void Start () {
        //Finds every object with th tag ShowOnPause
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        foreach (GameObject g in pauseObjects){
            g.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update () {
        //If the key pressed is the escape button
        if (Input.GetKeyDown(KeyCode.Escape)){
            //Pause the game
            if (!paused){
                paused = true;
                Time.timeScale = 0.0F;
                foreach (GameObject g in pauseObjects){
                    g.SetActive(true);
                }
            }
            //It's already paused so go back to game
            else {
                resume();
            }
        }
	}
    //Pressed the continue button so resume game
    public void resume() {
        paused = false;
        Time.timeScale = 1.0F;
        foreach (GameObject g in pauseObjects){
            g.SetActive(false);
        }
    }
}
