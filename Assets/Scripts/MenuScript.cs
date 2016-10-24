using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

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
}
