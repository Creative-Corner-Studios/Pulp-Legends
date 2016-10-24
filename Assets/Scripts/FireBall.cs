using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

    private Rigidbody2D rBody;
    private Collider2D cBody;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;
    private WorldController worldControl;
    internal GameObject home;

    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        rBody.mass = 0.01f;
        rBody.gravityScale = 0f;
        cBody = GetComponent<Collider2D>();
        worldControl = GameObject.Find("WorldController").GetComponent<WorldController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!worldControl.GamePaused)
        {
            //detects collisions
        }
    }

    void OnTriggerEnter2D(Collider2D thing)
    {
        if (thing.tag == "Enemy")
        {
            thing.GetComponent<Enemy>().Health-=damage;
            home.GetComponent<Player>().addScore(thing.GetComponent<Enemy>().DamageScore);
            if (thing.GetComponent<Enemy>().Health <= 0)
            {
                home.GetComponent<Player>().addScore(thing.GetComponent<Enemy>().DeathScore);
            }
        }
        if (thing.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    public void adjustVelocity(bool FacingLeft)//adjusts the velocity of the bullet depending on turned left or right
    {
        if (rBody == null)
        {
            rBody = GetComponent<Rigidbody2D>();
        }
        if (FacingLeft)
        {
            rBody.velocity = new Vector2(-1 * speed, 0);//sets the velocity
        }
        else
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
            rBody.velocity = new Vector2(1 * speed, 0);//sets the velocity
        }
        rBody.velocity *= speed;//multiples by speed of bullet
    }
}

