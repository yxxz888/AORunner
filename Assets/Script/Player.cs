using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Animator animator;

    private bool isGround = true;
    private int stepPlatformCount = 0;

    private bool isMoving = false;
    private float targetRotation;
    private float curRotation;



    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {

        if (isGround == false)//坠落
            transform.Translate(0, -0.1f, 0);
        else if (isMoving)
        {
            curRotation = Mathf.Lerp(curRotation, targetRotation, Time.deltaTime * 25);
            if (Mathf.Abs(curRotation - targetRotation) < 1)
            {
                isMoving = false;
                curRotation = targetRotation;
                if (curRotation >= 360)
                    curRotation -= 360;
                else if (curRotation < 0)
                    curRotation += 360;
            }
            transform.localEulerAngles = new Vector3(0, curRotation, 0);
        }
    }

    internal void startGame()
    {
        animator.SetTrigger("Start");
    }

    internal void forward()
    {
        if (isGround)
            this.transform.Translate(0, 0, 0.1f);
    }


    internal bool canAction()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        return info.IsTag("Normal") && isMoving == false;
    }


    internal void jump()
    {
        animator.SetTrigger("Jump");
    }


    internal void slide()
    {
        Debug.Log(transform.position);
        animator.SetTrigger("Slide");
    }

    internal void turnLeft()
    {
        isMoving = true;
        curRotation = transform.localEulerAngles.y;
        targetRotation = transform.localEulerAngles.y - 90;
    }

    internal void turnRight()
    {
        isMoving = true;
        curRotation = transform.localEulerAngles.y;
        targetRotation = transform.localEulerAngles.y + 90;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Platform")
            return;

        stepPlatformCount++;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Platform")
            return;

        stepPlatformCount--;
        if(stepPlatformCount <= 0)
        {
            handleFall();
        }
    }

    private void handleFall()
    {
        isGround = false;
        animator.SetBool("Grounded", false);
    }
}
