﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    enum CharacterType { SAMSPADE , NORACARTER};
    enum PulpPowerType { MALTESEFALCON, SATAN}
    [System.Serializable]
    public class InputSettings
    {
        //input variables
        public float delay = 0.3f;
        public float fwdInput, jumpInput;
        public string JUMP_AXIS;
        public string Horizontal_Axis;
        public bool jump = false;

        public void ConfigureInput(int playerNum)
        {
            switch (playerNum)
            {
                case 1:
                    JUMP_AXIS = "P1_Jump";
                    Horizontal_Axis = "P1_Horizontal";
                    break;

                case 2:
                    JUMP_AXIS = "P2_Jump";
                    Horizontal_Axis = "P2_Horizontal";
                    break;
            }
        }
    }
    //attributes 
    [SerializeField] private int health = 100;
    [SerializeField] private float attackPower = 20;
    [SerializeField] private float speed= 10;
    [SerializeField] private float jumpPower = 800;
    [SerializeField] private GameObject bullet;
    [SerializeField] private CharacterType character;
    [SerializeField] private LayerMask ground;
    [SerializeField] private int playerNum;

    private Vector3 position = Vector3.zero;
    private Vector2 velocity = Vector2.zero;
    private bool grounded;
    private Transform groundCheck;
    private PulpPowerType pulpPower;
    
    private GameObject player;
    private InputSettings input = new InputSettings();
    private Rigidbody2D rBody;

    
    //Properties
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
        }
    }

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    // Use this for initialization
    void Start() {

        input.ConfigureInput(playerNum); //configure the inputs for player 1 and 2

        //setup basic for physics of  character
        player = gameObject;
        input.fwdInput = 0;
        input.jumpInput = 0;
        groundCheck = transform.Find("GroundCheck");
        position = transform.position;
        rBody = GetComponent<Rigidbody2D>();
        rBody.mass = 1.0f;

        //Setup Stats for character based on character
        switch (character)
        {
            case CharacterType.SAMSPADE:
                health = 120;
                attackPower = 35;
                pulpPower = PulpPowerType.MALTESEFALCON;
                break;

            case CharacterType.NORACARTER:
                health = 200;
                attackPower = 50;
                pulpPower = PulpPowerType.SATAN;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        GetInput(); //gets input from players each frame
	}

    void FixedUpdate()
    {
        IsGrounded(); // checks if player is on the ground

        Move(); // moves the player based on input
        Jump(); //makes the player jump if there is input for jump
        input.jump = false;

        if(input.fwdInput ==0 && input.jumpInput == 0 && grounded) //if there is no input and the character is on the ground
        {
            rBody.velocity = Vector2.zero; // stops character 
        }

    }

    private void GetInput()
    {
        input.fwdInput = Input.GetAxis(input.Horizontal_Axis);
        input.jumpInput = Input.GetAxis(input.JUMP_AXIS);
        if (!input.jump)
        {
            input.jump = Input.GetButtonDown(input.JUMP_AXIS);
        }
    }

    private void Attack()
    {

    }

    private void PulpPower()
    {

    }

    private void Jump()
    {
        if(input.jump && grounded == true)
        {
            grounded = false;
            rBody.AddForce(new Vector2(0f, jumpPower));
        }
    }

    private void Move()
    {
        if(Mathf.Abs(input.fwdInput) > input.delay)
        {
            rBody.velocity = new Vector2(input.fwdInput * speed, rBody.velocity.y);

            //velocity = new Vector2( input.fwdInput * speed, rBody.velocity.y);
        }
        else
        {
            velocity = Vector3.zero;
        }
    }

    private void IsGrounded()
    {
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.3f, ground);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
        print(gameObject.name + " has Died So Sad :(");
        Application.LoadLevel("Main Menu");
    }

    public void ModHealth(int mod) {

    }
}