using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
	
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //loads a unity scene 
    public void LoadScene(string sceneName){ //NOTE: scene must be in the build to be able to go to that scene
        Application.LoadLevel(sceneName);
    }

    public void StartGame()
    {
        LoadScene("Test Level");
    }
}
