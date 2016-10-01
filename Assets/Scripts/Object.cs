using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour {

    private bool hazardous = false; //is the object harmful to the player
    private int damage = 0; //if it is hazardous how much damage will it do the player
    private bool moveable = false; //can the player move the object
    private GameObject obj;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

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
