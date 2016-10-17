using UnityEngine;
using System.Collections;

public class MalteseFalcon : MonoBehaviour {

    private Rigidbody2D rBody;
    private Collider2D cBody;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;
    private bool returning; //the time when the falcon is returning to the player
    [SerializeField] int returningMaxTime;//amount of time before returning to player
    [SerializeField] int existenceMaxTime;//amount of time before falcon is deleted
    private int returnTimer = 0;
    private GameObject home; //the player that threw the falcon
    private WorldController worldControl;

    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        rBody.mass = 0.01f;
        rBody.gravityScale = 0f;
        cBody = GetComponent<Collider2D>();
        returning = false;
        worldControl = GameObject.Find("WorldController").GetComponent<WorldController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!worldControl.GamePaused)
        {
            //detects collisions
            if (returnTimer >= returningMaxTime && !returning)
            {
                print("falcon now returning to " + home);
                returning = true;
            }
            else if (returnTimer >= existenceMaxTime)
            {
                print("falcon has returned to " + home);
                Destroy(gameObject);
            }
            else { returnTimer++; }

            if (returning)//falcon is returning to player
            {
                rBody.velocity = new Vector2((home.transform.position.x - transform.position.x), (home.transform.position.y - transform.position.y));
                rBody.velocity.Normalize();
                rBody.velocity *= 2 * speed;
            }

            if (rBody.velocity.x > 0) { transform.Rotate(new Vector3(0, 0, -5)); }
            else { transform.Rotate(new Vector3(0, 0, 5)); }
        }
    }

    void OnTriggerEnter2D(Collider2D thing)
    {
        if (thing.tag == "Enemy")
        {
            thing.GetComponent<Enemy>().Health -= damage;
        }
        if (returning)
        {
            if (thing.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
    }

    public void adjustVelocity(bool direction, GameObject returnPlayer)//adjusts the velocity of the bullet depending on turned left or right
    {
        if (rBody == null)
        {
            rBody = GetComponent<Rigidbody2D>();
        }
        if (direction)
        {
            rBody.velocity = new Vector2(-1 * speed, 0);//sets the velocity
        }
        else
        {
            rBody.velocity = new Vector2(1 * speed, 0);//sets the velocity
        }
        rBody.velocity *= speed;//multiples by speed of bullet
        home = returnPlayer;
    }
}
