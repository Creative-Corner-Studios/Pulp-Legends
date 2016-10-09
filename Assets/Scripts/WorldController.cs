using UnityEngine;
using UnityEditor;
using System.Collections;

public class WorldController : MonoBehaviour {
    public enum Screen { MAINMENU, OPTIONMENU, TESTLEVEL, LEVEL1, GAMEOVER};

    Camera GameCamera;
    public Screen currentScreen = Screen.MAINMENU;
    public bool runSetup = true;

    //Player/Character selection Data
    [SerializeField]
    private GameObject SamSpade;
    [SerializeField]
    private GameObject NoraCarter;
    public GameObject P1;
    public GameObject P2;
    private Player player1;
    private Player player2;
    private Player.CharacterType p1Char;
    private Player.CharacterType p2Char;
    public bool p1Active = true;
    public bool p2Active = true;

    //Test Level Data
    [SerializeField] private Vector3 p1TestPos;
    [SerializeField] private Vector3 p2TestPos;


    //Properties
    public Player.CharacterType P1Char
    {
        set { p1Char = value; }
    }

    public Player.CharacterType P2Char
    {
        set { p2Char = value; }
    }

    void Awake()//happens once, even if the script is not active
    {
        DontDestroyOnLoad(GameObject.Find("WorldController"));
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
                checkInOptionMenu();
                break;
            case "Test Level":
                if (runSetup)
                {
                    SetupCharacter();
                    SetupTestLevel();
                    runSetup = false;
                }
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
        GameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

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

    public void SetupCharacter()
    {
        if (p1Active)
        {
            switch (p1Char)
            {
                case Player.CharacterType.SAMSPADE:
                    P1 = Instantiate(SamSpade);
                    player1 = P1.GetComponent<Player>();
                    player1.PlayerNum = 1;
                    break;

                case Player.CharacterType.NORACARTER:
                    P1 = Instantiate(NoraCarter);
                    player1 = P1.GetComponent<Player>();
                    player1.PlayerNum = 1;
                    break;
            }
        }

        if (p2Active)
        {
            switch (p2Char)
            {
                case Player.CharacterType.SAMSPADE:
                    P2 = Instantiate(SamSpade);
                    player2 = P2.GetComponent<Player>();
                    player2.PlayerNum = 2;
                    if (p1Char == Player.CharacterType.SAMSPADE && !p1Active)
                    {
                        P2.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    break;

                case Player.CharacterType.NORACARTER:
                    P2 = Instantiate(NoraCarter);
                    player2 = P2.GetComponent<Player>();
                    player2.PlayerNum = 2;
                    if (p1Char == Player.CharacterType.NORACARTER && !p1Active)
                    {
                        P2.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    break;
            }
        }
    }

    void SetupTestLevel()
    {
        GameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (p1Active)
        {
            P1.transform.localPosition = p1TestPos;
            GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P1.transform;
        }
        if(p1Active && p2Active)
        {
            P1.transform.localPosition = p1TestPos;
            P2.transform.localPosition = p2TestPos;
            GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P1.transform;
        }
        if (p2Active)
        {
            P2.transform.localPosition = p2TestPos;
            GameCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = P2.transform;
        }
    }
}
