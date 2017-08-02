using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Player player;
    public Camera followCamera;
	// Use this for initialization
	void Start () {
        followCamera.transform.parent = player.transform;

    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey("Jump"))
        {
            
        }
        player.forward();
	}
}
