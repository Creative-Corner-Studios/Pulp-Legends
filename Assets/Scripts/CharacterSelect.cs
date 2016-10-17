using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {
    private class InputSettings //Hold all of the important variables for getting input from players
    {
        public string P1_HAXIS = "P1_Horizontal";
        public string P2_HAXIS = "P2_Horizontal";
        public string P1_SUBMIT = "P1_Submit";
        public string P2_SUBMIT = "P2_Submit";
        public string P1_CANCEL = "P1_Cancel";
        public string P2_CANCEL = "P2_Cancel";

        public float P1Hinput, P2Hinput, P1SubInput, P2SubInput, P1CancelInput, P2CancelInput;

        public bool P1Submit = false;
        public bool P2Submit = false;
        public bool P1Cancel = false;
        public bool P2Cancel = false;
    }

    //attributes
    [SerializeField]
    private GameObject P1_Controller, P2_Controller; // the players controller game assets

    [SerializeField]
    private GameObject P1_Check, P2_Check, Instruct0, Instruct1, Instruct2, P1_Inactive, P2_Inactive; // Instructional assets

    //controller positions
    [SerializeField]
    private float xl, xm, xr; //holds the left, middle , and right position for player's game controller asset

    private InputSettings input;

    //character selection's booleans to tell whether the player is joining game
    [SerializeField]
    private bool p1Active = true;

    [SerializeField]
    private bool p2Active = true;

    //Booleans to tell whether player has chosen a character yet
    [SerializeField] private bool p1Confirm = false;
    [SerializeField] private bool p2Confirm = false;

    //Player states to hold what character the player chose
    private Player.CharacterType p1Character;
    private Player.CharacterType p2Character;

    private WorldController worldControl; //variable for accessing the world controller

    // Use this for initialization
    void Start () {
        input = new InputSettings(); // intizales input class
        worldControl = GameObject.Find("WorldController").GetComponent<WorldController>(); // links the world controller script to the world controll attribut to give access to world controller data
	}
	
	// Update is called once per frame
	void Update () {
        GetInput(); //gets all input from players

        CheckActive(); // checks and updates which players are active in game

        UpdateInstructions(); // updates the UI Instructional images accordingly.

       

        StartLevel();// sends data to world controller and start game

        //player 1 slection and confirmation section
        if (p1Active) // if player 1 has join the game
        {
            if (p1Confirm != true) // and has not selected a character
            {
                P1Select(); 
                P1Confirm();
            }
           
        }

        //player 2 slection and confirmation section
        if (p2Active) // if player 2 has joined the game
        {
            if (p2Confirm != true) // and has not selected a character
            {
                P2Select();
                P2Confirm();
            }
        }

        CancelConfirmation(); // this method will cancel a player character selection or will drop player from game
        
	}

    //this method gets all input required for character selection
    void GetInput()
    {
        input.P1Hinput = Input.GetAxis(input.P1_HAXIS);
        //Debug.Log(input.P1Hinput);
        input.P1SubInput = Input.GetAxis(input.P1_SUBMIT);
        input.P1CancelInput = Input.GetAxis(input.P1_CANCEL);

        //P2 Inputs
        input.P2Hinput = Input.GetAxis(input.P2_HAXIS);
        input.P2SubInput = Input.GetAxis(input.P2_SUBMIT);
        input.P2CancelInput = Input.GetAxis(input.P2_CANCEL);

        if (!input.P1Submit)
        {
            input.P1Submit = Input.GetButtonDown(input.P1_SUBMIT);
            if (input.P1Submit == true)
            {
                Debug.Log(input.P1Submit);
            }
        }
        if (!input.P2Submit)
        {
            input.P2Submit = Input.GetButtonDown(input.P2_SUBMIT);
        }
        if (!input.P1Cancel)
        {
            input.P1Cancel = Input.GetButtonDown(input.P1_CANCEL);
        }
        if (!input.P2Cancel)
        {
            input.P2Cancel = Input.GetButtonDown(input.P2_CANCEL);
        }
    }

    //this method will move player 1 controller asset based on input.
    void P1Select()
    {
        if(input.P1Hinput < 0) // if the input is left
        {
            if(P1_Controller.transform.localPosition.x == xr) // and the controller is at the far right
            {
                P1_Controller.transform.localPosition = new Vector3(xm, P1_Controller.transform.localPosition.y, P1_Controller.transform.localPosition.z); // move controller to middle
            }
            else if(P1_Controller.transform.localPosition.x == xm) // or if controller is at middle
            {
                P1_Controller.transform.localPosition = new Vector3(xl, P1_Controller.transform.localPosition.y, P1_Controller.transform.localPosition.z); //move controller to left
            }
        }
        else if (input.P1Hinput > 0) // if the input is left
        {
            if (P1_Controller.transform.localPosition.x == xl) // and the controller is at the far right
            {
                P1_Controller.transform.localPosition = new Vector3(xm, P1_Controller.transform.localPosition.y, P1_Controller.transform.localPosition.z); // move controller to middle
            }
            else if (P1_Controller.transform.localPosition.x == xm) // or if controller is at middle
            {
                P1_Controller.transform.localPosition = new Vector3(xr, P1_Controller.transform.localPosition.y, P1_Controller.transform.localPosition.z); //move controller to left
            }
        }
    }

    //this method will move player 2 controller asset based on input.
    // this method is simillar to p1Select() see comments there for understanding it
    void P2Select()
    {
        if (input.P2Hinput < 0) 
        {
            if (P2_Controller.transform.localPosition.x == xr)
            {
                P2_Controller.transform.localPosition = new Vector3(xm, P2_Controller.transform.localPosition.y, P2_Controller.transform.localPosition.z);
            }
            else if (P2_Controller.transform.localPosition.x == xm)
            {
                P2_Controller.transform.localPosition = new Vector3(xl, P2_Controller.transform.localPosition.y, P2_Controller.transform.localPosition.z);
            }
        }
        else if (input.P2Hinput > 0)
        {
            if (P2_Controller.transform.localPosition.x == xl)
            {
                P2_Controller.transform.localPosition = new Vector3(xm, P2_Controller.transform.localPosition.y, P2_Controller.transform.localPosition.z);
            }
            else if (P2_Controller.transform.localPosition.x == xm)
            {
                P2_Controller.transform.localPosition = new Vector3(xr, P2_Controller.transform.localPosition.y, P2_Controller.transform.localPosition.z);
            }
        }
    }

    //this method will confirm player 1 character selection based on where player 1 controller asset is.
    void P1Confirm()
    {
        if(input.P1Submit == true) // checks to see if player 1 pressed submit button was pressed
        {
            if(P1_Controller.transform.localPosition.x == xl) // if controller asset was at left position; player 1 chose Sam Spade
            {
                p1Character = Player.CharacterType.SAMSPADE; // assigns player 1 character to sam spade
                p1Confirm = true; // tells script player 1 has chosen a character
                P1_Check.SetActive(true); //displays checkmark for visual confirmation
            }
            else if(P1_Controller.transform.localPosition.x == xr) //if controller asset was at right position; player 1 chose Nora Carter
            {
                p1Character = Player.CharacterType.NORACARTER; //assigns player 1 character to nora carter
                p1Confirm = true; // tells script player 1 has chosen a character
                P1_Check.SetActive(true);//displays checkmark for visual confirmation
            }

            Debug.Log("P1: " + p1Character); //prints player 1 character to console for debug purposes.
            input.P1Submit = false; // reset p1 submit boolean to false
        }
    }

    //this method will confirm player 2 character selection based on where player 2 controller asset is.
    //this method is simillar to P1Confirm() look at p1Confirm comments to understand this method.
    void P2Confirm()
    {
        if (input.P2Submit == true)
        {
            if (P2_Controller.transform.localPosition.x == xl)
            {
                p2Character = Player.CharacterType.SAMSPADE;
                p2Confirm = true;
                P2_Check.SetActive(true);
            }
            else if (P2_Controller.transform.localPosition.x == xr)
            {
                p2Character = Player.CharacterType.NORACARTER;
                p2Confirm = true;
                P2_Check.SetActive(true);
            }

            Debug.Log("P2: " + p2Character);
            input.P2Submit = false; // reset player 2 submit input to false
        }
    }

    // this method will cancel a player character selection or will drop player from game
    void CancelConfirmation()
    {
        if(input.P1Cancel == true) //checks if player 1 pressed cancel button
        {
            if (p1Confirm) //checks if player 1 has chosen a character
            {
                //cancels selection of character 
                p1Character = Player.CharacterType.NULL; // sets player 1 character chose to the null state
                p1Confirm = false; // tells script that player 1 has not chosen a character
                P1_Check.SetActive(false); // removes checkmark for player 1 selection
            }
            else if (p1Confirm == false) //checks to see if player wants to drop out of game
            {
                p1Active = false; //sets player 1 to no longer being in the game
                P1_Controller.transform.localPosition = new Vector3(xm, P1_Controller.transform.localPosition.y, P1_Controller.transform.localPosition.z); // moves p1 controller asset to middle
            }

            input.P1Cancel = false;
        }

        //Player 2 cancel code
        //See comments above for understanding this section of code
        if(input.P2Cancel == true)
        {
            if (p2Confirm)
            {
                p2Confirm = false;
                p2Character = Player.CharacterType.NULL;
                P2_Check.SetActive(false);
            }
            else if(p2Confirm == false)
            {
                p2Active = false;
                P2_Controller.transform.localPosition = new Vector3(xm, P2_Controller.transform.localPosition.y, P2_Controller.transform.localPosition.z);
            }
            input.P2Cancel = false;
        }
    }

    void CheckActive() //checks whether to join player into game
    {
        if(input.P1Submit && p1Active == false) //if player 1 pressed the submit button and is not in game
        {
            p1Active = true; //player 1 joins the game
        }

        if(input.P2Submit && p2Active == false) //if player 2 pressed the submit button and is not in game
        {
            p2Active = true; //player 2 joins the game
        }
    }

    //this method will check wether to start game and transfer neccessary data to world controller
    void StartLevel()
    {
        //code for if both players are in game
        if (p1Active && p2Active) 
        {
            if (input.P1Submit || input.P2Submit) //checks if submit button was pressed
            {
                if (p1Confirm && p2Confirm) //makes sure players have chosen characters
                {
                    //transfer data to world controller
                    worldControl.P1Char = p1Character;
                    worldControl.P2Char = p2Character;
                    worldControl.p1Active = p1Active;
                    worldControl.p2Active = p2Active;
                    worldControl.has2Players = true;
                    worldControl.currentScreen = WorldController.Screen.TESTLEVEL;
                    worldControl.runTestSetup = true;
                    Application.LoadLevel("Test Level");
                }
            }
        }
        else if (p1Active) //code for if only player 1 is in game
        {
            if (input.P1Submit)
            {
                if (p1Confirm)
                {
                    //transfer data to world controller
                    worldControl.P1Char = p1Character;
                    worldControl.p1Active = p1Active;
                    worldControl.p2Active = p2Active;
                    worldControl.has2Players = false;
                    worldControl.currentScreen = WorldController.Screen.TESTLEVEL;
                    worldControl.runTestSetup = true;
                    Application.LoadLevel("Test Level");
                }
            }
        }
        else if (p2Active) //code for if only player 2 is in game
        {
            if (input.P2Submit)
            {
                if (p2Confirm)
                {
                    //transfer data to world controller
                    worldControl.P2Char = p2Character;
                    worldControl.p1Active = p1Active;
                    worldControl.p2Active = p2Active;
                    worldControl.has2Players = false;
                    worldControl.currentScreen = WorldController.Screen.TESTLEVEL;
                    worldControl.runTestSetup = true;
                    Application.LoadLevel("Test Level");
                }
            }
        }
    }

    //this method updates instructional UI components based on current state of character selection
    void UpdateInstructions()
    {
        if (p1Active && p2Active) //checks if both players are in game
        {
            //turns off instruction for how to join game and set both inactive markers to off
            Instruct0.SetActive(false);
            P1_Inactive.SetActive(false);
            P2_Inactive.SetActive(false);

            if (p1Confirm && p2Confirm) //if both players have chosen characters
            {
                //turn off how to select character and turn on how to start game instruction
                Instruct1.SetActive(false);
                Instruct2.SetActive(true);
            }
            else 
            {
                //turn on how to select character and turn off how to star game instruction
                Instruct1.SetActive(true);
                Instruct2.SetActive(false);
            }
        }
        else if (p1Active)
        {
            P1_Inactive.SetActive(false);
            if (p1Confirm)
            {
                //turn off how to select character and turn on how to start game instruction
                Instruct1.SetActive(false);
                Instruct2.SetActive(true);
            }
            else
            {
                //turn on how to select character and turn off how to star game instruction
                Instruct1.SetActive(true);
                Instruct2.SetActive(false);
            }
        }
        else if (p2Active)
        {
            P2_Inactive.SetActive(false);
            if (p2Confirm)
            {
                //turn off how to select character and turn on how to start game instruction
                Instruct1.SetActive(false);
                Instruct2.SetActive(true);
            }
            else
            {
                //turn on how to select character and turn off how to star game instruction
                Instruct1.SetActive(true);
                Instruct2.SetActive(false);
            }
        }

        if (!p1Active || !p2Active) //if player 1 or player to has not joined game
        {
            //displays how to join game instruction
            Instruct0.SetActive(true);
        }

        //turns on inactive X if player is not in game
        if(p1Active == false)
        {
            P1_Inactive.SetActive(true);
        }
        if(p2Active == false)
        {
            P2_Inactive.SetActive(true);
        }
    }
}
