using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

    public GameObject P1;
    public GameObject P2;
    Camera GameCamera;
    //MenuScript Menues;

    void Awake()//happens once, even if the script is not active
    {
        DontDestroyOnLoad(GameObject.Find("WorldControllerPrefab"));
    }

	// Use this for initialization
	void Start () {
        P1 = null;
        P2 = null;
        GameCamera = Camera.current;
        //Menues = new MenuScript();
	}
	

	// Update is called once per frame
	void Update () {
        switch (Application.loadedLevelName) //tracks stuff throught game
        {
            case "Main Menu":
                checkInMainMenu();
                break;
            case "Option Menu":
                checkInOptionMenu();
                break;
            case "Test Level": //will take out later
                checkInTestLevel();
                break;
            case "Level 1":
                checkInLevel1();
                break;
            default: //returns to main menu incase of error
                //Application.LoadLevel("Main Menu");
                break;
        }
    }

    public void GameOver() // this will end the game
    {
        
    }

    public void PauseGame() // pause game and opens pause menu
    {

    }

    public void WinGame() // done when you win the game
    {
        
    }


    private void checkInMainMenu() //if the scene is the main menu, do this for update
    {
        //resetting
        if(P1 != null)
        {
            P1 = null;
        }
        if (P2 != null)
        {
            P2 = null;
        }
    }

    private void checkInOptionMenu()//if the scene is the option menu, do this for update
    {

    }

    private void checkInTestLevel()//if the scene is the Test level, do this for update, will take out later
    {
        if (P1 == null)
        {
            P1 = GameObject.Find("Sam Spade");
        }
        if (P2 == null)
        {
            P2 = GameObject.Find("Nora Carter");
        }

        if(P1 == null && P2 == null)//both characters are gone. Game Over
        {
            GameOver();
        }
    }

    private void checkInLevel1()//if the scene is level 1, do this for update
    {

    }

}
