using UnityEngine;
//using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using System;

public class WorldController : MonoBehaviour {
    public enum Screen { MAINMENU, OPTIONMENU, TUTORIALLEVEL, TESTLEVEL, LEVEL1, GAMEOVER}; // a public finite state for all screens in game.

    Camera GameCamera;// holds the main game camera
    public Screen currentScreen = Screen.MAINMENU; //finite state for which screen this is
    public bool runTutorialSetup = true;//bool to see if setup for Tutorial has been run
    public bool runTestSetup = true; //boolean to see if setup for the scene has been run
    public bool runLevel1Setup = true; //bool to see if setup for level 1 has been run
    public bool GamePaused = false;
    //Player/Character selection Data
    [SerializeField] private GameObject SamSpade; //holds the sam spade character prefab
    [SerializeField] private GameObject NoraCarter;// holds the nora carter character prefab
    public GameObject malteseFalcon; //pulp power for sam spade
    public GameObject fireBall; //pulp power for lucy faire
    public GameObject P1; //holds player 1 gameobject
    public GameObject P2; //holds player 2 game object

    //holds player script for player 1 and player 2
    private Player player1;
    private Player player2;

    //Finite states for player1 and player 2 character type
    private Player.CharacterType p1Char;
    private Player.CharacterType p2Char;

    //booleans to see if which players are in game
    public bool p1Active = true;
    public bool p2Active = true;
    public bool has2Players;

    //Game UI Data
    public GameUI gameUI;
    //public Slider p1HealthBar;
    //public Slider p2HealthBar;
    //public Slider p1PulpPowerBar;
    //public Slider p2PulpPowerBar;

    //Test Level Data
    [SerializeField] private Vector3 p1TestPos;// player 1 start position
    [SerializeField] private Vector3 p2TestPos;//player 2 start position

    //Level 1 Data
    [SerializeField] private Vector3 p1Level1Pos;// player 1 start position
    [SerializeField] private Vector3 p2Level1Pos;//player 2 start position

    //Tutorial Data
    [SerializeField] private Vector3 p1TutorialPos;// player 1 start position
    [SerializeField] private Vector3 p2TutorialPos;//player 2 start position
    //Properties
    public Player.CharacterType P1Char
    {
        set { p1Char = value; }
    }

    public Player.CharacterType P2Char
    {
        set { p2Char = value; }
    }

    public Player Player1
    {
        get { return player1; }
    }

    public Player Player2
    {
        get { return player2; }
    }

    void Awake()//happens once, even if the script is not active
    {
        DontDestroyOnLoad(GameObject.Find("WorldController")); //keeps game object from being destroyed between scenes
    }

	// Use this for initialization
	void Start () {
        GameCamera = Camera.current;
	}
	

	// Update is called once per frame
	void Update () {
        switch (Application.loadedLevelName) //tracks stuff throught game
        {
            case "Main Menu":
                checkInMainMenu();
                break;
            case "Option Menu":
                CheckInOptionMenu();
                break;
            case "Tutorial":
                if (runTutorialSetup) //checks to see if inital setup was run
                {
                    SetupCharacter();
                    SetupTutorialLevel();
                    runTutorialSetup = false;
                }
                if (!GamePaused)
                {
                    CheckInTutorialLevel();
                }
                break;
            case "Test Level":
                if (runTestSetup) //checks to see if inital setup was run
                {
                    SetupCharacter();
                    SetupTestLevel();
                    runTestSetup = false;
                }
                if (!GamePaused)
                {
                    CheckInTestLevel();
                }
                break;
            case "Level 1":
                if (runLevel1Setup)
                {
                    SetupCharacter();
                    SetupLevel1();
                    runLevel1Setup = false;
                }
                if (GamePaused)
                {
                    CheckInLevel1();
                }
                break;
            default: //returns to main menu incase of error
                //Application.LoadLevel("Main Menu");
                break;
        }
    }

    public void GameOver() // this will end the game
    {
        Application.LoadLevel("Main Menu");
    }

    //public void PauseGame() // pause game and opens pause menu
    //{

    //}

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

    private void CheckInOptionMenu()//if the scene is the option menu, do this for update
    {
        GameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    private void CheckInTutorialLevel()
    {
        bool p1Alive = true;
        bool p2Alive = true;

        if (p1Active && p2Active)
        {
            p1Alive = player1.CheckIsAlive();
            p2Alive = player2.CheckIsAlive();
            if (p1Alive == false)
            {
                GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P2.transform;
            }
            if (p1Alive == false && p2Alive == false)//both characters are gone. Game Over
            {
                GameOver();
            }
        }
        else if (p1Active)
        {
            if (player1.CheckIsAlive() == false)
            {
                GameOver();
            }
        }
        else if (p2Active)
        {
            if (player2.CheckIsAlive() == false)
            {
                GameOver();
            }
        }
    }

    private void CheckInTestLevel()//if the scene is the Test level, do this for update, will take out later
    {
        bool p1Alive = true;
        bool p2Alive = true;

        if (p1Active && p2Active)
        {
            p1Alive = player1.CheckIsAlive();
            p2Alive = player2.CheckIsAlive();
            if(p1Alive == false)
            {
                GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P2.transform;
            }
            if (p1Alive == false && p2Alive == false)//both characters are gone. Game Over
            {
                GameOver();
            }
        }
        else if(p1Active)
        {
            if(player1.CheckIsAlive() == false)
            {
                GameOver();
            }
        }
        else if (p2Active)
        {
            if(player2.CheckIsAlive() == false)
            {
                GameOver();
            }
        }
    }

    private void CheckInLevel1()//if the scene is level 1, do this for update
    {
        bool p1Alive = true;
        bool p2Alive = true;

        if (p1Active && p2Active)
        {
            p1Alive = player1.CheckIsAlive();
            p2Alive = player2.CheckIsAlive();
            if (p1Alive == false)
            {
                GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P2.transform;
            }
            if (p1Alive == false && p2Alive == false)//both characters are gone. Game Over
            {
                GameOver();
            }
        }
        else if (p1Active)
        {
            if (player1.CheckIsAlive() == false)
            {
                GameOver();
            }
        }
        else if (p2Active)
        {
            if (player2.CheckIsAlive() == false)
            {
                GameOver();
            }
        }
    }

    //this method sets up characters and instantiate them into current level
    public void SetupCharacter()
    {
        if (has2Players)
        {
            p1Active = true;
            p2Active = true;
        }

        if (p1Active) //if player 1 is active
        {
            switch (p1Char) // check which character player 1 is
            {
                case Player.CharacterType.SAMSPADE: 
                    P1 = Instantiate(SamSpade); //instantiate a sam spade character and save it into p1 gameobject
                    player1 = P1.GetComponent<Player>(); //get the player script from p1 object for player 1
                    player1.PlayerNum = 1; // set this as player 1
                    break;

                case Player.CharacterType.NORACARTER:
                    P1 = Instantiate(NoraCarter); //instantiate a sam spade character and save it into p1 gameobject
                    player1 = P1.GetComponent<Player>(); //get the player script from p1 object for player 1
                    player1.PlayerNum = 1; // set this as player 1
                    break;
            }
        }

        if (p2Active) //if player 2 is active
        {
            switch (p2Char) //determine which character player 2 chose
            {
                case Player.CharacterType.SAMSPADE:
                    P2 = Instantiate(SamSpade); //instantiate a sam spade character and save it into p2 gameobject
                    player2 = P2.GetComponent<Player>();//gets the player script from p2 object for player 2
                    player2.PlayerNum = 2; //sets this as player 2
                    if (p1Char == Player.CharacterType.SAMSPADE && p1Active) //if player 2 is same character as player 1 and player one is in game
                    {
                        P2.GetComponent<SpriteRenderer>().color = Color.green; //render this sprite as green
                    }
                    break;

                case Player.CharacterType.NORACARTER: //look at above comments to understand what is going on
                    P2 = Instantiate(NoraCarter);
                    player2 = P2.GetComponent<Player>();
                    player2.PlayerNum = 2;
                    if (p1Char == Player.CharacterType.NORACARTER && p1Active)
                    {
                        P2.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    break;
            }
        }
    }

    void SetupTutorialLevel()
    {
        GameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameUI = GameObject.Find("Game UI Canvas").GetComponent<GameUI>();

        gameUI.SetupUI();
        if (p1Active && p2Active)
        {
            P1.transform.localPosition = p1TutorialPos;
            P2.transform.localPosition = p2TutorialPos;
            //GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P1.transform;
        }
        else if (p1Active)
        {
            P1.transform.localPosition = p1TutorialPos;
            //GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P1.transform;
        }
        else if (p2Active)
        {
            P2.transform.localPosition = p2TutorialPos;
            //GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P2.transform;
        }
    }
    //this method setup the camera and character position in testLevel
    void SetupTestLevel()
    {
       GameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameUI = GameObject.Find("Game UI Canvas").GetComponent<GameUI>();

        gameUI.SetupUI();
        if (p1Active && p2Active)
        {
            P1.transform.localPosition = p1TestPos;
            P2.transform.localPosition = p2TestPos;
            //GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P1.transform;
        }
        else if (p1Active)
        {
            P1.transform.localPosition = p1TestPos;
            //GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P1.transform;
        }
        else if (p2Active)
        {
            P2.transform.localPosition = p2TestPos;
            //GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P2.transform;
        }
    }

    void SetupLevel1()
    {
        GameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameUI = GameObject.Find("Game UI Canvas").GetComponent<GameUI>();

        gameUI.SetupUI();
        if (p1Active && p2Active)
        {
            P1.transform.localPosition = p1Level1Pos;
            P2.transform.localPosition = p2Level1Pos;
           // GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P1.transform;
        }
        else if (p1Active)
        {
            P1.transform.localPosition = p1Level1Pos;
            //GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P1.transform;
        }
        else if (p2Active)
        {
            P2.transform.localPosition = p2Level1Pos;
            //GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P2.transform;
        }
    }
}
