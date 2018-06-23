using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour {
    //Gets button name from inspector's text
    public string buttonName;
    void Start()
    {
        GameObject gaymeObject = GameObject.Find(buttonName);
        Button button = gaymeObject.GetComponent<Button>();

        button.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick() //Quits game
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
