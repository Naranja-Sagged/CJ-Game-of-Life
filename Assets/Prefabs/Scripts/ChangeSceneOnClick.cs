using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneOnClick : MonoBehaviour {
    public string sceneName = "CharacterSelection";
    void Start() {
        GameObject gaymeObject = GameObject.Find("Play");
        Button button = gaymeObject.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
