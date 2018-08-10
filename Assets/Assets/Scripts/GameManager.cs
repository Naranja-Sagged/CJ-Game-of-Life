using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script runs continuously while scene is active
public class GameManager : MonoBehaviour {
    public static List<GameObject> allObjects = new List<GameObject>();

    GameObject[] pauseObjects;
    GameObject[] movers;

    bool paused = false;

    // Called before start. Used to initialize values that other scripts need immediately.
    void Awake() {
        InitializeAllGameObjects();
    }

    // Use this for initialization
    void Start () {
        InitializePauseObjects();
    }

    // Update is called once per frame
    void Update () {
        PauseCheck();
	}

    void InitializeAllGameObjects() {
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(allObjects);
    }

    //=========================================================================
    // These functions are for the pause function in game
    //=========================================================================

    void InitializePauseObjects() {
        //Finds every object with the tag ShowOnPause
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
        //Finds every object with fake (GameObject) tag "Mover"
        movers = GameObject.FindGameObjectsWithTag("Mover");
        //Ensures game doesn't start paused
        Resume();
    }

    void PauseCheck() {
        //If the key pressed is the escape button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Pause the game
            if (!paused)
            {
                paused = true;
                Time.timeScale = 0.0F;

                ScriptController(false);

                foreach (GameObject g in pauseObjects)
                {
                    g.SetActive(true);
                }
            }
            //It's already paused so go back to game
            else
            {
                Resume();
            }
        }
    }

    //Pressed the continue button so resume game
    void Resume() {
        paused = false;
        Time.timeScale = 1.0F;
        foreach (GameObject g in pauseObjects) {
            g.SetActive(false);
        }
        ScriptController(true);
    }

    //If true, enable scripts. False, disable scripts.
    void ScriptController(bool onOff) {
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
