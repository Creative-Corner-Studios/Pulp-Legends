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
        rBody.gravityScale = .01f;
        cBody = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update() {//detects collisions

    }

    void OnTriggerEnter2D(Collider2D thing)
    {
        print("test");
        if (thing.tag == "Player")
        {
            thing.GetComponent<Player>().ModHealth(-damage);
            Destroy(gameObject);
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
