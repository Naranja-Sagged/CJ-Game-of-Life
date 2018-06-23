using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Quit : MonoBehaviour {
    public void OnApplicationQuit(){
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        Console.WriteLine("Quit is working");
    }
}
