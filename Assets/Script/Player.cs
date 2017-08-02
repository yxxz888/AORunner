using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Animator animator;

    private bool isStart;
 
	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startGame()
    {
        isStart = true;
        animator.SetBool("Start", true);
    }

    public void forward()
    {
        if(isStart)
            this.transform.Translate(new Vector3(0,0,0.1f));
    }
}
