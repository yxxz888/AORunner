using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    private bool isStepped = false;
    private bool isOut = false; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    internal bool isPlayerPass()
    {
        return isStepped && isOut;
    }


    internal void reset()
    {
        isStepped = false;
        isOut = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isStepped = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isOut = true;
        }
    }
}
