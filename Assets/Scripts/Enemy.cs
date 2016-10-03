using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private int health;
    //private enum enemyType; //we should create a state machine so we can distinguish enemy types
    private float attackPower;
    private float speed;
    private GameObject enemy;
    private GameObject Bullet;
    private int detectRange;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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

    private void Sentry() // this method will have the enemy patrol for player
    {

    }

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
