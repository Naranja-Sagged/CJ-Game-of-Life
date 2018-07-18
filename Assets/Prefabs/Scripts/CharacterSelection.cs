using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour {

    private List<GameObject> characterList;
    //Default index
    private int index;
    public void Start() {
        index = PlayerPrefs.GetInt("CharacterSelected");
        characterList = new List<GameObject>();
        //Turns off the character unless it is chosen
        foreach (Transform t in transform) {
            characterList.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "CharacterSelection") {
            characterList[index].SetActive(true);
        }
    }
    public void Update() {

    }
    public void Select(int index){
        if (index < 0 || index >= characterList.Count) {
            return;
        }
        PlayerPrefs.SetInt("CharacterSelected", index);
        characterList[index].SetActive(true);

    }
    public void changeScene(int index) {
        SceneManager.LoadScene("TestLevel");
        PlayerPrefs.SetInt("CharacterSelected", index);
    }
}
