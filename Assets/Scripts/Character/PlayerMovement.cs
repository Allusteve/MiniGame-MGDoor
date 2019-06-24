using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class PlayerMovement : MonoBehaviour {

	private CharacterController2D controller;
    //public Animator animator;
    private UnityArmatureComponent armatureComponent;
    enum Mark {Stand, Crouching, Walking,Running,Crawling,JumpLeft,JumpRight,Jumping,Locking,Death,Breaking,ClimbInPipe,Down,Nothing}

    private const string NORMAL_ANIMATION_GROUP = "normal";
    private DragonBones.AnimationState _walkState = null;
    private DragonBones.AnimationState _crouchState = null;
    private DragonBones.AnimationState _runState = null;
    private Mark _moveDir = Mark.Nothing;
    private Armature armature;
    private Slot TowelSlot;
    private Slot AxeSlot;
    private List<Slot> list;
    private float timer = 0.0f;
    private AudioSource audiosource;

    public float walkSpeed = 40f;
    public float runSpeed = 60f;
    public bool CanStand=true;
    private AudioClip[] audioclips;

	float horizontalMove = 0f;
	bool jump = false;
	public bool crouch = false;
    bool run = false;
    bool ground = true;
    bool onlock = false;
    bool locker = false;
    bool death = false;
    bool Breaking=false;
    bool ClimbInPipe = false;
    bool down = false;
    bool IsPlayer = true;

    bool isRight;
    bool isLeft;

    int count = 0;
    int show_towel = -1;
    int show_axe = -1;


    // Update is called once per frame

    void Start()
    {

        this.armatureComponent = this.GetComponent<UnityArmatureComponent>();
        this.controller = this.GetComponent<CharacterController2D>();
        this.audiosource = this.GetComponent<AudioSource>();

        this.armatureComponent.AddDBEventListener(DragonBones.EventObject.COMPLETE, this.OnAnimationHandle);
        this.armatureComponent.AddDBEventListener(DragonBones.EventObject.FADE_IN, this.OnAnimationHandle);

       if(GameObject.Find("Dog")!=null)
        {
            Collider2D cat = GameObject.Find("Dog").GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(cat, this.GetComponent<BoxCollider2D>(), true);
            Physics2D.IgnoreCollision(cat, this.GetComponent<CircleCollider2D>(), true);
        }

       if(GameObject.Find("Cat")!=null)
        {
            Collider2D dog = GameObject.Find("Cat").GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(dog, this.GetComponent<BoxCollider2D>(), true);
            Physics2D.IgnoreCollision(dog, this.GetComponent<CircleCollider2D>(), true);

        }



        this.TowelSlot = this.armatureComponent.armature.GetSlot("wet_towel");
        this.AxeSlot = this.armatureComponent.armature.GetSlot("hammer");

        TowelSlot.displayIndex = show_towel;
        AxeSlot.displayIndex = show_axe;

        audioclips = new AudioClip[]
        {
            (AudioClip)Resources.Load("Musics/walk"),
            (AudioClip)Resources.Load("Musics/run"),
            (AudioClip)Resources.Load("Musics/landing"),

        };
    }

    void Update ()
    { 
        if(!locker)
        {

            isRight = Input.GetKey(KeyCode.D);
            isLeft = Input.GetKey(KeyCode.A);


            if (CanStand)
            {
                crouch = Input.GetKey(KeyCode.LeftControl);
            }
            else
                crouch = true;

            if (Input.GetKeyDown(KeyCode.Space) && !crouch)
            {
                jump = true;
            }

            if (run)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            }
            else
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;
            }

        }

        if (!onlock)
        {
         
                if (down)
                {
                    this.Move(Mark.Down);
                }
                else
                {

                    if (isLeft == isRight&&ground)
                    {
                        timer = 0.0f;
                        run = false;
                        if (crouch)
                            this.Move(Mark.Crouching);
                         else if (jump)
                            this.Jump(Mark.Jumping);
                        else
                        {
                            if(Breaking)
                            {
                                this.Move(Mark.Breaking);
                            }
                            else
                                this.Move(Mark.Stand);
                        }
                    }
                    else if (ground&&(isRight||isLeft))
                    {
         

                        if(crouch)
                        {
                            run = false;
                            timer = 0.0f;

                            this.Move(Mark.Crawling);

                        }
                        else
                        {
                            timer += Time.deltaTime;
                            if (timer > 1.2f)
                            {
                                run = true;     
                   
                                if (jump)
                                    this.Jump(Mark.Jumping);
                                else
                                     this.Move(Mark.Running);
                    
                            }
                            else
                            {
                                run = false;                   
                  
                                if (jump)
                                    this.Jump(Mark.Jumping);
                                else
                                {
                                   if(audiosource.clip==audioclips[2]&&audiosource.isPlaying)
                                    {

                                    }
                                   else
                                    this.Move(Mark.Walking);
                                }

                            }
                        }


                    
                }
     


        
            }
        }
        else if(!death&&onlock)
        {
            this.armatureComponent.animation.Stop();
            this._moveDir = Mark.Locking;
            horizontalMove = 0.0f;
            this.audiosource.Pause();
            
        }
        else
        {
            Move(Mark.Death);
            this.audiosource.Pause();
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            controller.OnFlip();
        }
    }

	public void OnLanding ()
	{
       // Debug.Log("OnLanding");
        ground = true;
        timer = 0.0f;

        this.audiosource.loop = false;
        this.audiosource.clip = audioclips[2];
        this.audiosource.Play();

    }

	public void OnCrouching (bool isCrouching)
	{
        crouch = isCrouching;
	}

    public void OnTowelShow()
    {
        if (show_towel == -1)
            show_towel = 0;
        else
            show_towel = -1;

        TowelSlot.displayIndex = show_towel;
    }

    public void OnAxeShow()
    {
        if (show_axe == -1)
            show_axe = 0;
        else
            show_axe = -1;

        AxeSlot.displayIndex = show_axe;
    }

    public void OnDeath()
    {
        death = true;
    }

    public void OnReborn()
    {
        death = false;
        onlock = false;
    }

    public void OnLocking()
    {
       if(onlock==false)
        {
            onlock = true;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
       else if(onlock==true)
        {
            onlock = false;
            this.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
        }
    }

    public void OnBreaking()
    {
        Breaking = true;
      
    }

    public void OnClimbInPipe()
    {
        ClimbInPipe = true;
    }

    public void OnDowning()
    {
        if(down)
        {
            down = false;
            locker = false;
        }
        else
        {
            down = true;
            locker = true;
        }
    }

    public bool IsJumping()
    {
        return !ground;
    }
   

    void OnAnimationHandle(string type, EventObject evt)
    {
        switch (evt.type)
       {
            case DragonBones.EventObject.COMPLETE:
            {
                    if (evt.animationState.name == "break")
                    {
                        Breaking = false;
                        locker = false;
                    }
                    
                    if(evt.animationState.name=="climbInThePipe")
                    {
                        ClimbInPipe = false;
                        locker = false;
                        crouch = true;
                    }
                    
            

            }
                break;

            case DragonBones.EventObject.FADE_IN:
            {
                    if(evt.animationState.name=="walk")
                    {
                        if (this.audiosource.clip ==audioclips[2])
                        {
                           
                        }
                        this.audiosource.clip = audioclips[0];
                        this.audiosource.Play();
                    }

                    else if (evt.animationState.name == "run")
                    {
                        this.audiosource.loop = true;
                        this.audiosource.clip = audioclips[1];
                        this.audiosource.Play();
                    }
                    else
                    {
                        if(this.audiosource.clip !=audioclips[2])
                        {
                            this.audiosource.Pause();
                            this.audiosource.loop = false;
                        }
                        
                      

                    }

                }
                break;

            default:
              
                break;
        }
    }

	void FixedUpdate ()
	{
        // Move our character
       if(!onlock)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        }
		jump = false;
	}


    void Move(Mark flag)
    {


        if (this._moveDir == flag)
        {
            return;
        }

   

        this._moveDir = flag;
        this._UpdateAnimation();
    }

    void Jump(Mark flag)
    {
        this.armatureComponent.animation.FadeIn("jump", -1.0f, 1, 0, NORMAL_ANIMATION_GROUP).resetToPose = false;
        this._moveDir = flag;
        _walkState = null;
        _runState = null;
        ground = false;
       
    }

    void _UpdateAnimation()
    {
        if(!death)
        {


            if(down)
            {
                this._walkState = null;
                this._runState = null;
                this.armatureComponent.animation.FadeIn("down", -1.0f, -1, 0, NORMAL_ANIMATION_GROUP);

            }
           else if(crouch)
            {
                if (this._moveDir == Mark.Crouching)
                {
                    this.armatureComponent.animation.Stop();
                    this._walkState = null;
                    this._runState = null;
                    this.armatureComponent.animation.FadeIn("crawl_idle", -1.0f, -1, 0, NORMAL_ANIMATION_GROUP);
                }else
                {
                    this._crouchState = this.armatureComponent.animation.FadeIn("crawl", -1.0f, -1, 0, NORMAL_ANIMATION_GROUP);
                    this._runState = null;
                    this._walkState = null;
                }

            }
            else
            {

                if(run)
                {
           
                    if(_runState==null)
                    {
                        this._runState=this.armatureComponent.animation.FadeIn("run", -1.0f, -1, 0, NORMAL_ANIMATION_GROUP);
                        this._runState.resetToPose = false;
                        this._walkState = null;
                    }
                }
                else
                {
                    if (this._moveDir == Mark.Stand)
                    {
                        //test, need to remove soon
                        this.armatureComponent.animation.Stop();
                        this._walkState = null;
                        this._runState = null;
                        this.armatureComponent.animation.FadeIn("idle", -1.0f, -1, 0, NORMAL_ANIMATION_GROUP);
                
                    }
                    else if(this._moveDir==Mark.Walking)
                    {
                        if (this._walkState == null)
                        {
                            this._walkState = this.armatureComponent.animation.FadeIn("walk", -1.0f, -1, 0, NORMAL_ANIMATION_GROUP);
                            this._walkState.resetToPose = false;
                        }

            
                    }
                    else if(this._moveDir==Mark.Breaking)
                    {
                        this.armatureComponent.animation.FadeIn("break", -1.0f, 2,0,NORMAL_ANIMATION_GROUP).resetToPose=false;
                        this._walkState = null;
                        this._runState = null;
                        locker = true;
        
                        
                    }
                    else if(this._moveDir==Mark.ClimbInPipe)
                    {
                        this.armatureComponent.animation.FadeIn("climbInThePipe", -1.0f, 1,0, NORMAL_ANIMATION_GROUP).resetToPose = false;
                        this._walkState = null;
                        this._runState = null;
                        locker = true;
                    }

                }
            }
        }
        else
        {
            this.armatureComponent.animation.Stop();
            this._walkState = null;
            this._runState = null;
            this.armatureComponent.animation.FadeIn("dizzy", -1.0f, 1, 0, NORMAL_ANIMATION_GROUP);
        }
    }
}
