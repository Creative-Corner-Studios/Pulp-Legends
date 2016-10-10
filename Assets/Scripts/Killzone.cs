using UnityEngine;
using System.Collections;

public class Killzone : MonoBehaviour {

    private BoxCollider2D killzone;

	// Use this for initialization
	void Start () {
        killzone = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D thing)
    {
        if(thing.tag == "Player")
        {
            thing.GetComponent<Player>().DestroyPlayer();
        }
    }
}
