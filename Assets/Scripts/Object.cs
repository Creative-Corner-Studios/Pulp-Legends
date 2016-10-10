using UnityEngine;
using System.Collections;

//requires the object to have a collider
[RequireComponent(typeof(Collider2D))]
public class Object : MonoBehaviour {

    private bool hazardous = false; //is the object harmful to the player
    private int damage = 0; //if it is hazardous how much damage will it do the player
    private bool moveable = true; //can the player move the object
    private bool collectable = true; //can the player collect it/if the player collides with it will it dissapear
    private GameObject obj;
    private Rigidbody2D objRigid;

	// Use this for initialization
	void Start () {
        obj = gameObject;
	    if(moveable == true)
        {
            objRigid = obj.AddComponent<Rigidbody2D>();
            objRigid.mass = 5;
            
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    //check if colliding with player and if it's collectable desctroy it
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && collectable)
        {
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
