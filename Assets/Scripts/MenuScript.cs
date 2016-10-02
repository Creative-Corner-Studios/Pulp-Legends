using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    [SerializeField]
    private GameObject optionCanvas;
    [SerializeField]
    private GameObject creditsCanvas;

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

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SwitchCredits()
    {
        optionCanvas.SetActive(false);
        creditsCanvas.SetActive(true);    
    }

    public void SwitchOption()
    {
        optionCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

}
