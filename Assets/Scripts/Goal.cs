using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Goal : MonoBehaviour {
    //attributes
    WorldController worldControl;
    [SerializeField] private WorldController.Screen nextLevel;
    private string level;

	// Use this for initialization
	void Start () {
        worldControl = GameObject.Find("WorldController").GetComponent<WorldController>();

        switch (nextLevel)
        {
            case WorldController.Screen.MAINMENU:
                level = "Main Menu";
                break;

            case WorldController.Screen.TESTLEVEL:
                level = "Test Level";
                break;

            case WorldController.Screen.LEVEL1:
                level = "Level 1";
                break;

            case WorldController.Screen.OPTIONMENU:
                level = "Option Menu";
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D thing)
    {
        Debug.Log("Goal triggered");
        if (thing.tag == "Player")
        {
            NextLevel();
        }
    }

    void NextLevel()
    {
        switch (nextLevel)
        {
            case WorldController.Screen.TESTLEVEL:
                worldControl.runTestSetup = true;
                break;

            case WorldController.Screen.LEVEL1:
                worldControl.runLevel1Setup = true;
                break;
        }
        worldControl.currentScreen = nextLevel;
        Application.LoadLevel(level);
    }
}
