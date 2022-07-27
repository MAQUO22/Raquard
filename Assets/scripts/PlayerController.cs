using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool alive = true;
    public float speed = 6.0F;
    public float jumpF = 5.0f;
    public int addJump = 1;
    public bool onGround;
    public bool inWater;
    public bool onWall;
    public Transform GroudCheck;
    public Transform MetalCheck;
    public float checkRadius = 0.2F;
    public LayerMask Ground;
    public LayerMask Water;
    public LayerMask MetalWall;
    public Rigidbody2D rbody;
    public Animator animator;
    public int batteries = 0;
    public Vector3 savePoint = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroudCheck.position, checkRadius, Ground);
        animator.SetBool("onGround", onGround);
    }
    void CheckingWater()
    {
        inWater = Physics2D.OverlapCircle(GroudCheck.position, checkRadius, Water);
        animator.SetBool("inWater", inWater);
        if (inWater)
        {
            alive = false;
        }
    }

    void CheckingMetalWall()
    {
        onWall = Physics2D.OverlapCircle(MetalCheck.position, checkRadius, MetalWall);
        animator.SetBool("onWall", onWall);
    }

    private float timePower = 5f;
    private float powerTimer = 0;
    public int power = 0;
    void Walk()
    {
        if (!blockMove)
        {
            float movement = Input.GetAxis("Horizontal");

            if (movement < 0)
            {
                powerTimer = 0;
                animator.SetBool("isRunning", true);
                transform.localScale = new Vector3(-1, 1, 1);
                rbody.velocity = new Vector2(movement * speed, rbody.velocity.y);

            }
            else if (movement > 0)
            {
                powerTimer = 0;
                animator.SetBool("isRunning", true);
                transform.localScale = new Vector3(1, 1, 1);
                rbody.velocity = new Vector2(movement * speed, rbody.velocity.y);
            }
            else
            {
                animator.SetBool("isRunning", false);
                rbody.velocity = new Vector2(0, rbody.velocity.y);
                TimerAFK();
            }
        }
    }

    void TimerAFK()
    {
        if (power < 3)
        {
            if ((powerTimer += Time.deltaTime) >= timePower)
            {
                power++;
                powerTimer = 0;
            }
        }
    }
    
    private bool blockMove = false;
    private float jumpWallTime = 0.5f;
    private float timerJump;
    private Vector2 jumpVector = new Vector2(3.0F,6.0F);

    void WallJump()
    {
        if (Input.GetKey(KeyCode.H) && onWall && !onGround && Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("jump");
            powerTimer = 0;
            blockMove = true;

            transform.localScale *= new Vector2(-1, 1);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);

            rbody.velocity = new Vector2(transform.localScale.x * jumpVector.x, jumpVector.y);
        }

        if (blockMove && (timerJump += Time.deltaTime) >= jumpWallTime)
        {
            if (onWall || onGround)
            {
                rbody.velocity = new Vector2(0, 0);
                blockMove = false;
                timerJump = 0;
            }
        }
    }

    void SpendPowerOnJump()
    {
        if(addJump != 1)
        {
            if(power > 0)
            {
                addJump = 1;
            }
            if(power <= 0)
            {
                power = 0;
            }
        }
        
        
    }
    void Jump()
    {   
        if (!onWall)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                animator.SetTrigger("jump");
                powerTimer = 0;
                if (onGround)
                {
                    rbody.velocity = new Vector2(0, jumpF);
                }

                if (!onGround && addJump > 0)
                {
                    rbody.velocity = new Vector2(0, jumpF);
                    power--;
                    addJump--;

                }
                 
            }   
        }
        if (onGround)
        {
            SpendPowerOnJump();
        }
    }
    void MoveOnWall()
    {
        
        float movement = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.H))
        {   
            powerTimer = 0;
            if (onWall)
            {

                if (!blockMove)
                {
                    rbody.gravityScale = 0;
                    animator.SetFloat("upDown", 0);
                    rbody.velocity = new Vector2(0, 0);
                }


            }

            if (movement > 0 && onWall)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, movement * (speed / 2));
                animator.SetFloat("upDown", 1);
            }

            if (movement < 0 && onWall)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, movement * (speed / 2));
                animator.SetFloat("upDown", -1);
            }
        }

        if (Input.GetKeyUp(KeyCode.H) || onWall == false)
        {
            rbody.gravityScale = 1;
        }
    }

    private float deathTime = 5f;
    private float timerDeath;

    void Update()
    {
        if (alive)
        {
            CheckingGround();
            CheckingMetalWall();
            CheckingWater();
            Walk();
            MoveOnWall();
            Jump();
            WallJump();
        }
        else
        {
            if ((timerDeath += Time.deltaTime) >= deathTime)
            {
                transform.position = savePoint;
                alive = true;
                timerDeath = 0;
            }
        }
    }
}
