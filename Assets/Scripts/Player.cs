using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    public enum CharacterType { SAMSPADE , NORACARTER, NULL};
    enum PulpPowerType { MALTESEFALCON, SATAN};
    [System.Serializable]
    public class InputSettings
    {
        //input variables
        public float delay = 0.3f;
        public float fwdInput, jumpInput, meleeInput, pulpInput;
        public string JUMP_AXIS;
        public string Horizontal_Axis;
        public string Melee_Axis;
        public string Pulp_Axis;
        public bool jump = false;
        public bool melee;
        public bool pulp;

        public void ConfigureInput(int playerNum)
        {
            switch (playerNum)
            {
                case 1:
                    JUMP_AXIS = "P1_Jump";
                    Horizontal_Axis = "P1_Horizontal";
                    Melee_Axis = "P1_Melee";
                    Pulp_Axis = "P1_Pulp";
                    break;

                case 2:
                    JUMP_AXIS = "P2_Jump";
                    Horizontal_Axis = "P2_Horizontal";
                    Melee_Axis = "P2_Melee";
                    Pulp_Axis = "P2_Pulp";
                    break;
            }
        }
    }
    //attributes 
    [SerializeField] private int health = 100;
    [SerializeField] private float attackPower = 20;
    [SerializeField] private float speed= 10;
    [SerializeField] private float jumpPower = 1000;
    [SerializeField] private CharacterType character;
    [SerializeField] private LayerMask ground;
    [SerializeField] private int playerNum;
    [SerializeField] private int score = 0;
    private bool FacingLeft;

    private Vector3 position = Vector3.zero;
    private Vector2 velocity = Vector2.zero;
    private bool grounded;
    private Transform groundCheck;
    private PulpPowerType pulpPower;
    private WorldController worldControl;

    private GameObject player;
    private InputSettings input = new InputSettings();
    private Rigidbody2D rBody;
    private bool meleeAttackDone = true;
    private bool airControl = true;

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

    public int PlayerNum
    {
        get { return playerNum; }
        set
        {
            if(value > 2 || value < 1)
            {
                Debug.LogError("Trying to set player num to a value that is not 1 or 2");
            }
            else
            {
                playerNum = value;
            }
        }
    }

    public CharacterType Character
    {
        get { return character; }
    }

    // Use this for initialization
    void Start() {

        input.ConfigureInput(playerNum); //configure the inputs for player 1 and 2
        worldControl = GameObject.Find("WorldController").GetComponent<WorldController>();

        //setup basic for physics of  character
        player = gameObject;
        input.fwdInput = 0;
        input.jumpInput = 0;
        input.pulpInput = 0;
        
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
        MeleeAttack();
        PulpPower();
        input.jump = false;
        input.melee = false;
        input.pulp = false;

        if(input.fwdInput ==0 && input.jumpInput == 0 && grounded) //if there is no input and the character is on the ground
        {
            rBody.velocity = Vector2.zero; // stops character 
        }

        if(grounded)
        {
            airControl = true;
        }
        //Debug.Log("rbody vel: " + rBody.velocity);

    }

    private void GetInput()
    {
        input.fwdInput = Input.GetAxis(input.Horizontal_Axis);
        input.jumpInput = Input.GetAxis(input.JUMP_AXIS);
        input.pulpInput = Input.GetAxis(input.Pulp_Axis);
        if (!input.jump)
        {
            input.jump = Input.GetButtonDown(input.JUMP_AXIS);
        }
        if (!input.melee)
        {
            input.melee = Input.GetButtonDown(input.Melee_Axis);
        }
        if (!input.pulp)
        {
            input.pulp = Input.GetButtonDown(input.Pulp_Axis);
        }
    }

    private void MeleeAttack()
    {
        if (input.melee)
        {
            int range = 2;
            Collider2D[] col = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x + range, transform.position.y+ 1));
            if (FacingLeft)//left
            {
                col = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x - range, transform.position.y + 1));
            }

            foreach (Collider2D thing in col)
            {
                if (thing.tag == "Enemy")
                {
                    Enemy enemy = thing.GetComponent<Enemy>();
                    enemy.Health -= (int)attackPower;
                    score += enemy.DamageScore;
                    if(enemy.Health <= 0)
                    {
                        score += enemy.DeathScore;
                    }
                    Debug.Log("Enemy: " + thing.name + " was hit for " + attackPower + " damage");
                }
            }
        }
    }

    private void PulpPower()
    {
        if (input.pulp)
        {
            GameObject b = new GameObject();
            switch (pulpPower)
            {
                case PulpPowerType.MALTESEFALCON:
                    b = GameObject.Instantiate(worldControl.GetComponent<WorldController>().malteseFalcon);
                    b.GetComponent<MalteseFalcon>().adjustVelocity(FacingLeft, gameObject);
                    break;

                case PulpPowerType.SATAN:
                    //shoot animation
                    b = GameObject.Instantiate(worldControl.GetComponent<WorldController>().fireBall);
                    b.GetComponent<FireBall>().adjustVelocity(FacingLeft);
                    break;
            }
            if (FacingLeft)//going left
            {
                b.transform.position = new Vector3(transform.position.x - .6f, transform.position.y + .2f);
            }
            else//going right
            {
                b.transform.position = new Vector3(transform.position.x + .6f, transform.position.y + .2f);
            }
        }
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
            if (airControl || grounded)
            {
                rBody.velocity = new Vector2(input.fwdInput * speed, rBody.velocity.y);
            }
            if(input.fwdInput < 0)//left
            {
                if(FacingLeft == false)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
                }
                FacingLeft = true;
            }
            else if(input.fwdInput > 0)//right
            {
                if (FacingLeft == true)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
                }
                FacingLeft = false;
            }
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, 0.3f);
    }

    private void Flip()
    {

    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
        print(gameObject.name + " has Died So Sad :(");
        Application.LoadLevel("Main Menu");
    }

    public void ModHealth(int mod) {
        health += mod;
    }

    public bool CheckIsAlive()
    {
        if(health <= 0) {
            Destroy(gameObject);
            switch (playerNum)
            {
                case 1:
                    worldControl.p1Active = false;
                    break;

                case 2:
                    worldControl.p2Active = false;
                    break;
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    public void addScore(int addition)
    {
        score += addition;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!grounded)
        {
            airControl = false;     
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        airControl = true;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log("Rbody: " + rBody.velocity);
    }
}
