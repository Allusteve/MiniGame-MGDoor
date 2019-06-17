using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class DogMovement : MonoBehaviour
{

    private CharacterController2D controller;
    private UnityArmatureComponent armatureComponent;
    bool jump = false;
    bool crouch = false;
    public float RunSpeed = 60f;
    float horizontalMove = 0f;
    bool running = false;
    private float timer = 0.0f;
    public float RunTime = 2.0f;

    int Direction = 1;
    bool Startrun = false;


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
        timer -= Time.deltaTime;
        timer = timer > 0.0f ? timer : 0.0f;

        if(Startrun)
        {

            if (timer > 0.0f)
            {
                running = true;
            }
            else
            {
                running = false;
                this.armatureComponent.animation.Stop();
                Startrun = false;
            }


            if (running)
                horizontalMove = Direction * RunSpeed;
            else
                horizontalMove = 0.0f;
        }


        //@Test
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //   OnRunning();
        //}


        

    }



    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }


    public void OnRunning()
    {
        Startrun = true;
        timer = RunTime;
        this.Direction = -1;
        this.armatureComponent.animation.FadeIn("run", -1.0f, -1, 0, "normal").resetToPose = false;


    }


    public void OnIdle()
    {
        this.armatureComponent.animation.FadeIn("idle", -1.0f, -1, 0, "normal").resetToPose = false;
        running = false;
    }

    public void OnEating()
    {
        running = false;
        this.armatureComponent.animation.FadeIn("eat", -1.0f, -1, 0, "normal").resetToPose = false;
    }
}
