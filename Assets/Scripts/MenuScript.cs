using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuScript : MonoBehaviour {

    [SerializeField]
    private GameObject optionGroup;
    [SerializeField]
    private GameObject creditsGroup;
    [SerializeField]
    private GameObject eventObject;

    private EventSystem eventSystem1;



    // Use this for initialization
    void Start () {
        eventSystem1 = eventObject.GetComponent<EventSystem>();
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
        LoadScene("Character Selection");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SwitchCredits()
    {
        optionGroup.GetComponent<CanvasGroup>().alpha = 0;
        optionGroup.GetComponent<CanvasGroup>().interactable = false;
        creditsGroup.GetComponent<CanvasGroup>().alpha = 1;
        creditsGroup.GetComponent<CanvasGroup>().interactable = true;
    }

    public void SwitchOption()
    {
        creditsGroup.GetComponent<CanvasGroup>().alpha = 0;
        creditsGroup.GetComponent<CanvasGroup>().interactable = false;
        optionGroup.GetComponent<CanvasGroup>().alpha = 1;
        optionGroup.GetComponent<CanvasGroup>().interactable = true;
    }

    public void ReturnToMenu()
    {
        LoadScene("Main Menu");
    }

}
