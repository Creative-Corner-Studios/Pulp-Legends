using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System;

public class MenuScript : MonoBehaviour {

    [SerializeField]
    private GameObject optionGroup;
    [SerializeField]
    private GameObject creditsGroup;

    [SerializeField]
    private CanvasGroup ControlsGroup;
    [SerializeField]
    private Dropdown controlSelector;
    [SerializeField]
    private Image controlImage;
    [SerializeField]
    private Sprite keyboardControlSprite;
    [SerializeField]
    private Sprite p1ControlSprite;
    [SerializeField]
    private Sprite p2ControlSprite;
    [SerializeField]
    private Text p1score;
    [SerializeField]
    private Text p2score;
    private WorldController worldControl;
    // Use this for initialization
    void Start () {
        worldControl = GameObject.Find("WorldController").GetComponent<WorldController>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (worldControl.currentScreen)
        {
            case WorldController.Screen.GAMEOVER:
                GameOverUpdate();
                break;

            case WorldController.Screen.WINGAME:
                WinUpdate();
                break;
        }
	}

    private void WinUpdate()
    {
        UpdateScore(worldControl.P1Score, worldControl.P2Score);
    }

    private void GameOverUpdate()
    {
        UpdateScore(worldControl.P1Score, worldControl.P2Score);
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
        //make sure other canvas groups are turned off
        optionGroup.GetComponent<CanvasGroup>().alpha = 0;
        optionGroup.GetComponent<CanvasGroup>().interactable = false;
        ControlsGroup.alpha = 0;
        ControlsGroup.interactable = false;

        //turn on credits canvase group
        creditsGroup.GetComponent<CanvasGroup>().alpha = 1;
        creditsGroup.GetComponent<CanvasGroup>().interactable = true;

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Credits Back Btn"));
    }

    public void SwitchOption()
    {
        //make sure other canvas groups are turned off
        creditsGroup.GetComponent<CanvasGroup>().alpha = 0;
        creditsGroup.GetComponent<CanvasGroup>().interactable = false;
        ControlsGroup.alpha = 0;
        ControlsGroup.interactable = false;

        //turn on option group canvas
        optionGroup.GetComponent<CanvasGroup>().alpha = 1;
        optionGroup.GetComponent<CanvasGroup>().interactable = true;

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Controls Btn")); //sets view control button as the current selected gameobject
    }

    public void SwitchControls()
    {
        //make sure other canvas groups are turned off
        creditsGroup.GetComponent<CanvasGroup>().alpha = 0;
        creditsGroup.GetComponent<CanvasGroup>().interactable = false;
        optionGroup.GetComponent<CanvasGroup>().alpha = 0;
        optionGroup.GetComponent<CanvasGroup>().interactable = false;

        //turn on control canvas group
        ControlsGroup.alpha = 1;
        ControlsGroup.interactable = true;

        //reset control selector
        controlSelector.value = 0;
        EventSystem.current.SetSelectedGameObject( controlSelector.gameObject); //sets control selector as the current selected gameobject
    }

    public void ReturnToMenu()
    {
        Destroy(GameObject.Find("WorldController"));
        LoadScene("Main Menu");
    }

    public void UpdateControlDisplay()
    {
        switch (controlSelector.value)
        {
            case 0:
                controlImage.sprite = keyboardControlSprite;
                break;

            case 1:
                controlImage.sprite = p1ControlSprite;
                break;

            case 2:
                controlImage.sprite = p2ControlSprite;
                break;
        }
    }

    private void UpdateScore(int score1, int score2)
    {
        if (worldControl.has2Players)
        {
            p1score.gameObject.SetActive(true);
            p2score.gameObject.SetActive(true);
            p1score.text = "Player 1 Score: " + score1;
            p2score.text = "Player 2 Score: " + score2;
        }else if (worldControl.hasP1)
        {
            p1score.gameObject.SetActive(true);
            p1score.text = "Player Score: " + score1;
            p2score.gameObject.SetActive(false);
        }else if (worldControl.hasP2)
        {
            p1score.gameObject.SetActive(false);
            p2score.gameObject.SetActive(true);
            p2score.text = "Player Score: " + score2;
        }
    }
}
