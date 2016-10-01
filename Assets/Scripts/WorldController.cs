using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

    public GameObject P1;
    public GameObject P2;
    Camera GameCamera;
    MenuScript Menues;

    void Awake()//happens once, even if the script is not active
    {
        DontDestroyOnLoad(GameObject.Find("WorldController"));
    }

	// Use this for initialization
	void Start () {
        P1 = null;
        P2 = null;
        GameCamera = Camera.current;
        Menues = new MenuScript();
	}
	
	// Update is called once per frame
	void Update () {
        switch (Application.loadedLevelName)
        {
            case "Main Menu":
                break;
            case "Option Menu":
                break;
            case "Test Level":
                break;
            case "Level 1":
                break;
            default:
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

}
