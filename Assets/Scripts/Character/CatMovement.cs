using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class CatMovement : MonoBehaviour
{

    private CharacterController2D controller;
    private UnityArmatureComponent armatureComponent;
    bool jump = false;
    bool crouch = false;
    public float WalkSpeed = 20f;
    float horizontalMove = 0f;
    bool walking = false;
    private float timer = 0.0f;
    private bool loop = false;
    public float WalkTime = 2.0f;


    int Direction = 1;
    


    // Start is called before the first frame update
    void Start()
    {
     
        this.armatureComponent = this.GetComponent<UnityArmatureComponent>();
        this.controller = this.GetComponent<CharacterController2D>();
        this.armatureComponent.animation.FadeIn("idle", -1.0f, -1, 0, "normal").resetToPose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(loop)
        {
            timer -= Time.deltaTime;
            if(timer>0.0f)
            {
                walking = true;
            }
            else
            {
                timer = WalkTime;
                if (this.Direction == -1)
                    this.Direction = 1;
                else
                    this.Direction = -1;
           
            }
        }
        if (walking)
            horizontalMove = Direction * WalkSpeed;
        else
            horizontalMove = 0.0f;

        //@Test
       /* if(Input.GetKeyDown(KeyCode.T))
        {
            OnWalking();
            
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            OnStanding();
        }
        */
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void OnWalking()
    {
        timer = WalkTime;
        this.Direction = -1;
        loop = true;
        this.armatureComponent.animation.FadeIn("walk", -1.0f, -1, 0, "normal").resetToPose = false;


    }

    
    public void OnIdle()
    {
        loop = false;
        this.armatureComponent.animation.FadeIn("idle", -1.0f, -1, 0, "normal").resetToPose = false;
        walking = false;
    }
}
