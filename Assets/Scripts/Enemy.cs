using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour {

    [System.Serializable] enum enemyType { MOVING, SHOOTING }; //we should create a state machine so we can distinguish enemy types

    //attributes
    public int health;
    [SerializeField] private int attackPower;
    [SerializeField] private float speed;
    [SerializeField] private enemyType type; //the type of enemy  
    [SerializeField] private GameObject Bullet;
    public GameObject endPointLeft; //left end point to turn around for moving enemies
    public GameObject endPointRight; //right end point to turn around for moving enemies
    private Vector3 ePLeftStart;//the starting position of the left end point
    private Vector3 ePRightStart;//the starting position of the right end point
    public bool direction; //left = true, right = false
    private int timer; //current amount has passed
    [SerializeField] private int timeToAttack; //amount of frames have passed for enemy to attack
    [SerializeField] private float detectRange; //how close a player can be so the enemy will notice them

    [SerializeField] private int damageScore; //the score the player gets when they hit the enemy
    [SerializeField] private int deathScore; //the score the player gets when the enemy dies

    private Rigidbody2D rBody;

    // Use this for initialization
    void Start () {
        timer = 100;
        rBody = GetComponent<Rigidbody2D>();
        rBody.mass = 1.0f;
        if (endPointLeft != null && endPointRight != null)
        {
            ePLeftStart = endPointLeft.transform.position;
            ePRightStart = endPointRight.transform.position;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(health <= 0)//enemy is out of health, so die
        {
            this.DestroyEnemy();
        }
        switch(type)
        {
            case enemyType.MOVING:
                checkToMove();
                Move();
                checkToMelee();
                break;
            case enemyType.SHOOTING:
                checkToShoot();
                break;
            default:
                break;
        }
	}

    //prooperites
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public void modHealth(int mod)  // add/subtract from health
    {
        health += mod;
    }

    public int DamageScore
    {
        get { return damageScore; }
    }

    public int DeathScore
    {
        get { return deathScore; }
    }

    private void checkToMove()
    {
        if (direction && Mathf.Abs(endPointLeft.transform.position.x - transform.position.x) <= speed)//walking left and close to end point
        {
            direction = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
        if (!direction && Mathf.Abs(endPointRight.transform.position.x - transform.position.x) <= speed)//walking left and close to end point
        {
            direction = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void Move()
    {
        if (direction)
        {
            rBody.velocity = new Vector2(-1 * speed, rBody.velocity.y);
        }
        else
        {
            rBody.velocity = new Vector2(speed, rBody.velocity.y);
        }
        if (endPointLeft != null && endPointRight != null)
        {
            endPointLeft.transform.position = ePLeftStart;
            endPointRight.transform.position = ePRightStart;
        }
    }

    private void checkToMelee()//check to see if time to melee attack
    {
        if (timer % timeToAttack == 0)//time to shoot
        {
            int range = 2;
            Collider2D[] col = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x + range, transform.position.y + 1));
            if (direction)//left
            {
                col = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x - range, transform.position.y + 1));
            }

            foreach (Collider2D thing in col)
            {
                if (thing.tag == "Player")
                {
                    Player player = thing.GetComponent<Player>();
                    player.ModHealth(-attackPower);
                    Debug.Log("Player: " + thing.name + " was hit for " + attackPower + " damage");
                }
            }
        }
        timer++;
        timer %= timeToAttack;
    }

    private void checkToShoot()//check to see if time to shoot a bullet
    {
        //if(DetectPlayer())
        if(timer % timeToAttack == 0)//time to shoot
        {
            //shoot animation
            GameObject b = GameObject.Instantiate(Bullet);
            
            if (direction)//going left
            {
                //b.transform.localScale = new Vector3(b.transform.localScale.x, b.transform.localScale.y);
                b.transform.position = new Vector3(transform.position.x - .6f, transform.position.y+.2f);
            }
            else//going right
            {
                //b.transform.localScale = new Vector3(-b.transform.localScale.x, b.transform.localScale.y);
                b.transform.position = new Vector3(transform.position.x + .6f, transform.position.y+.2f);
            }
            b.GetComponent<Bullet>().adjustVelocity(direction);
        }
        timer++;
        timer %= timeToAttack;
    }

    //private void Sentry() // this method will have the enemy patrol for player
    //{

    //}

    private bool DetectPlayer(Player p)  // this method checks if player is within enemy’s detection range.
    {
        float xDis = Mathf.Abs(p.gameObject.transform.position.x - gameObject.transform.position.x);
        float yDis = Mathf.Abs(p.gameObject.transform.position.y - gameObject.transform.position.y);
        float distance = Mathf.Sqrt((xDis * xDis) + (yDis * yDis));
        if(distance <= detectRange)//close enough to be detected
        {
            return true;
        }
        return false;
    }

    private void DestroyEnemy() // destroys the gameobject
    {
        Destroy(gameObject);
    }
}
