using UnityEngine;
using System.Collections;

//requires the object to have a collider
[RequireComponent(typeof(Collider2D))]
public class Object : MonoBehaviour {

    private bool hazardous = false; //is the object harmful to the player
    private int damage = 0; //if it is hazardous how much damage will it do the player
    [SerializeField] private bool moveable = true; //can the player move the object
    [SerializeField] private bool collectable = false; //can the player collect it/if the player collides with it will it dissapear
    [SerializeField] private int scoreBonus;
    private GameObject obj;
    private Rigidbody2D objRigid;

	// Use this for initialization
	void Start () {
        obj = gameObject;
	    if(moveable == true)
        {
            objRigid = obj.AddComponent<Rigidbody2D>();
            objRigid.mass = 10;
            
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    //check if colliding with player and if it's collectable desctroy it
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && collectable)
        {
            other.GetComponent<Player>().addScore(scoreBonus);
            other.GetComponent<Player>().PulpCurrent += 20;
            other.GetComponent<Player>().Health += 25;
            DestroyObject();
        }
    }

    //destroys the fobject
    private void DestroyObject()
    {
        Destroy(obj);
    }

    //get damage
    public int Damage()
    {
        return damage;
    }

    //get hazardous
    public bool Hazardous()
    {
        return hazardous;
    }
}
