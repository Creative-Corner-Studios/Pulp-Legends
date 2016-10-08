﻿using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {
    private class InputSettings
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
    private GameObject P1_Controller, P2_Controller;

    //controller positions
    [SerializeField]
    private float xl, xm, xr;

    private InputSettings input;

    [SerializeField]
    private bool p1Active = true;

    [SerializeField]
    private bool p2Active = true;

    private bool p1Confirm = false;
    private bool p2Confirm = false;

    private Player.CharacterType p1Character;
    private Player.CharacterType p2Character;

    private WorldController worldControl;

    // Use this for initialization
    void Start () {
        input = new InputSettings();
        worldControl = GameObject.Find("WorldController").GetComponent<WorldController>();
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();

        StartLevel();

        if (p1Confirm != true)
        {
            P1Select();
            P1Confirm();
        }
        input.P1Submit = false;
        if (p2Confirm != true)
        {
            P2Select();
            P2Confirm();
        }
        input.P2Submit = false;

        CancelConfirmation();

	}

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

    void P1Select()
    {
        if(input.P1Hinput < 0)
        {
            if(P1_Controller.transform.localPosition.x == xr)
            {
                P1_Controller.transform.localPosition = new Vector3(xm, P1_Controller.transform.localPosition.y, P1_Controller.transform.localPosition.z);
            }
            else if(P1_Controller.transform.localPosition.x == xm)
            {
                P1_Controller.transform.localPosition = new Vector3(xl, P1_Controller.transform.localPosition.y, P1_Controller.transform.localPosition.z);
            }
        }
        else if (input.P1Hinput > 0)
        {
            if (P1_Controller.transform.localPosition.x == xl)
            {
                P1_Controller.transform.localPosition = new Vector3(xm, P1_Controller.transform.localPosition.y, P1_Controller.transform.localPosition.z);
            }
            else if (P1_Controller.transform.localPosition.x == xm)
            {
                P1_Controller.transform.localPosition = new Vector3(xr, P1_Controller.transform.localPosition.y, P1_Controller.transform.localPosition.z);
            }
        }
    }

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

    void P1Confirm()
    {
        if(input.P1Submit == true)
        {
            if(P1_Controller.transform.localPosition.x == xl)
            {
                p1Character = Player.CharacterType.SAMSPADE;
                p1Confirm = true;
            }
            else if(P1_Controller.transform.localPosition.x == xr)
            {
                p1Character = Player.CharacterType.NORACARTER;
                p1Confirm = true;
            }

            Debug.Log("P1: " + p1Character);
        }
    }

    void P2Confirm()
    {
        if (input.P2Submit == true)
        {
            if (P2_Controller.transform.localPosition.x == xl)
            {
                p2Character = Player.CharacterType.SAMSPADE;
                p2Confirm = true;
            }
            else if (P2_Controller.transform.localPosition.x == xr)
            {
                p2Character = Player.CharacterType.NORACARTER;
                p2Confirm = true;
            }

            Debug.Log("P2: " + p2Character);
        }
    }

    void CancelConfirmation()
    {
        if(input.P1Cancel == true)
        {
            if (p1Confirm)
            {
                p1Character = Player.CharacterType.NULL;
                p1Confirm = false;
            }
        }
        if(input.P2Cancel == true)
        {
            if (p2Confirm)
            {
                p2Confirm = false;
                p2Character = Player.CharacterType.NULL;
            }
        }
    }

    void StartLevel()
    {
        if (input.P1Submit || input.P2Submit)
        {
            if (p1Confirm && p2Confirm)
            {
                //transfer data to world controller
                worldControl.P1Char = p1Character;
                worldControl.P2Char = p2Character;
                worldControl.currentScreen = WorldController.Screen.TESTLEVEL;
                worldControl.runSetup = true;
                Application.LoadLevel("Test Level");
            }
        }
    }
}