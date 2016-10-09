using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    private Rigidbody2D rBody;
    private Collider2D cBody;
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    // Use this for initialization
    void Start() {
        rBody = GetComponent<Rigidbody2D>();
        rBody.mass = 0.000001f;
        rBody.gravityScale = 1f;
        cBody = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update() {//detects collisions

        if (GameObject.Find("WorldController") != null)//makes sure the world controller exists
        {
            WorldController wc = GameObject.Find("WorldController").GetComponent<WorldController>();
            if (wc.P1 != null)//makes sure there is a player 1
            {
                if (cBody.IsTouching(wc.P1.GetComponent<Collider2D>()))
                {
                    print("touched the butt");
                    wc.P1.GetComponent<Player>().ModHealth(-damage);
                    Destroy(gameObject);
                }
            }
            if (wc.P2 != null)//makes sure there is a player 1
            {
                if (cBody.IsTouching(wc.P2.GetComponent<Collider2D>()))
                {
                    print("touched the butt 2");
                    wc.P2.GetComponent<Player>().ModHealth(-damage);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void adjustVelocity(bool direction)//adjusts the velocity of the bullet depending on turned left or right
    {
        if (rBody == null)
        {
            rBody = GetComponent<Rigidbody2D>();
        }
        if (direction)
        {
            rBody.velocity = new Vector2(-1 * speed,0);//sets the velocity
        }
        else
        {
            rBody.velocity = new Vector2(1 * speed, 0);//sets the velocity
        }
        rBody.velocity *= speed;//multiples by speed of bullet
    }
}
