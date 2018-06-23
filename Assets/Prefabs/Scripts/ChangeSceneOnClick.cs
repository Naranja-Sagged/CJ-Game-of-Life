using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneOnClick : MonoBehaviour {
    //String names defined in inspector
    public string sceneName;
    public string buttonName;

    void Start() {
        GameObject gaymeObject = GameObject.Find(buttonName);
        Button button = gaymeObject.GetComponent<Button>();
        //Makes button interactable
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick() { 
        SceneManager.LoadScene(sceneName);
    }
}
