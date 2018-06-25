using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour {

    private List<GameObject> characterList;
    //Default index
    private int selectionIndex = 0;
    public void Start() {
        characterList = new List<GameObject>();
        //Turns off the character unless it is chosen
        foreach (Transform t in transform) {
            characterList.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
    }
    public void Update() {

    }
    public void Select(int index){
        if (index < 0 || index >= characterList.Count) {
            return;
        }
        PlayerPrefs.SetInt("CharacterSelected", index);
        characterList[selectionIndex].SetActive(false);
        selectionIndex = index;
        characterList[selectionIndex].SetActive(true);

    }
 }
