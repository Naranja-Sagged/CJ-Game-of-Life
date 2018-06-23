using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script runs continuously while scene is active
public class GameMananger : MonoBehaviour {
    GameObject[] pauseObjects;
    GameObject[] movers;
    bool paused = false;


    // Use this for initialization
    void Start () {
        //Finds every object with the tag ShowOnPause
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        foreach (GameObject g in pauseObjects){
            g.SetActive(false);
        }
        //Finds every object with fake (GameObject) tag "Mover"
        movers = GameObject.FindGameObjectsWithTag("Mover");

    }

    // Update is called once per frame
    void Update () {
        //If the key pressed is the escape button
        if (Input.GetKeyDown(KeyCode.Escape)){
            //Pause the game
            if (!paused){
                paused = true;
                Time.timeScale = 0.0F;

                scriptController(false);

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
        scriptController(true);
    }
    //If true, enable scripts. False, disable scripts.
    public void scriptController(bool onOff) {
        //Iterates through each object with fake (GameObject) tag "Mover"
        foreach (GameObject gs in movers)
        {
            //Takes the parent of the empty GameObject tagged with "Mover", which is the real object
            GameObject realObject = gs.transform.parent.gameObject;

            MonoBehaviour[] scripts = realObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour m in scripts)
            {
                m.enabled = onOff;
            }
        }
    }
}
