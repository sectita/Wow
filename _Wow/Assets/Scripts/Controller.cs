using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    Rigidbody2D rb;
    private float _move;
    private Animator animator;
    [SerializeField] private float Speed;
    private bool rightflip = true; //if right flip = true and right flip = false

    public float _JumpForce;
    private bool IsGrounded;
    public Transform feetPos;
    public float circleRadius;
    public LayerMask layerGround;
    private SpriteRenderer spriterender;
    [SerializeField] private Slider EnergySlider;

    public float JumpTime;
    private bool IsJumping;
    private float JumpTimeCounter;

    private bool top;
    private float GravityTime = 0; // reference variable
    private float GravityTimeCounter = 0;
    public float Energy;

    void Start()
    {
        EnergySlider = FindObjectOfType<Slider>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriterender = GetComponent<SpriteRenderer>();

        JumpTimeCounter = JumpTime;

        //GravityTimeCounter = GravityTime;
        EnergySlider.maxValue = Energy;
        EnergySlider.value = Energy;

    }

    void Update()
    {
        JumpFun();
        MoveFun();
        TeleportSide();
        //if (Input.GetKey(KeyCode.LeftControl))
        //{
           GravityOn();
        //}
        //else if(GravityTime > GravityTimeCounter || GravityTime <= 0)
        //{
        //    GravityOff();
        //}
    }

    void GravityOn()
    {
        if (Input.GetMouseButton(0)/*GetKey(KeyCode.LeftControl)*/ && Energy > .1f)
        {
            rb.gravityScale *= -1;
            GravityTimeCounter += Time.deltaTime;
        }
        else
        {
            rb.gravityScale *= 1;
            GravityTime += Time.deltaTime;
        }
        if (GravityTimeCounter > .5)
        {
            GravityTimeCounter = 0;
            Energy -= .5f;
            EnergySlider.value -= .5f;
            
        }
        if ((GravityTime > 1.3) && Energy < 4f)
        {
            GravityTime = 0;
            Energy += .3f;
            EnergySlider.value += .3f;

        }
        if (GravityTime > 1.5 || GravityTime < 2 || Energy == 0f)
        {
            rb.gravityScale *= 1;
        }
    }

    void Flip()
    {
        rightflip = !rightflip;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void MoveFun()
    {
        //animator.SetFloat("_speed", Mathf.Abs(_move));

        _move = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
        rb.velocity = new Vector2(_move * Speed, rb.velocity.y);

        //if(_move == 0)
        //{
        //    animator.SetBool("running", false);
        //}

        if (rightflip == false && _move > 0)
        {
            SoundManager.PlaySound("foot", true);
            Flip();
        }
        else if (rightflip == true && _move < 0)
        {
            SoundManager.PlaySound("foot", true);
            Flip();
        }
        else if (_move > 0 || _move < 0)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
    }
    void JumpFun()
    {
        IsGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius, layerGround);
        if (IsGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.PlaySound("Jump", true);
            animator.SetTrigger("jump");
            IsJumping = true;
            JumpTimeCounter = JumpTime;
            rb.velocity = Vector2.up * _JumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && IsJumping == true)
        {
            if (JumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * _JumpForce;
                JumpTimeCounter -= Time.deltaTime;
                IsJumping = true;
            }
            else
            {
                IsJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            IsJumping = false;
            animator.SetBool("jumping", true);
        }
        else
        {
            animator.SetBool("jumping", false);
        }
    }
    

    //void GravityOn()
    //{
    //    if (GravityTime > 0 || GravityTimeCounter < GravityTime)
    //    {
    //        rb.gravityScale *= -1;
    //        Rotation();
    //    }
    //    GravityTime -= Time.deltaTime;
    //    if (GravityTime <= 0)
    //    {
    //        GravityOff();
    //    }
    //}
    //void GravityOff()
    //{
    //    if (GravityTime <= 0/* && GravityTime != GravityTimeCounter)
    //    {
    //        GravityTime = 0;
    //        rb.gravityScale *= 1;
    //        //Rotation();
    //    }
    //    GravityTime += Time.deltaTime;
    //    //if (GravityTime > GravityTimeCounter)
    //    //{
    //    //    GravityTimeCounter = GravityTime;
    //    //}
    //}

    //void Rotation()
    //{
    //    if (top == false)
    //    {
    //        transform.eulerAngles = new Vector3(0, 0, 180f);
    //        spriterender.flipX = false;
    //    }
    //    else
    //    {
    //        transform.eulerAngles = Vector3.zero;
    //        spriterender.flipX = true;
    //    }
    //    top = !top;
    //}

    readonly float offsetLeftX = -5f;
    readonly float offsetRightX = 8f;

    //void TeleportSide()
    //{
    //    if (transform.position.x < offsetLeftX)
    //    {
    //        SoundManager.PlaySound("Blah", true);
    //        transform.position = new Vector3(offsetRightX, transform.position.y, transform.position.z);
    //    }
    //    else if (transform.position.x > offsetRightX)
    //    {
    //        SoundManager.PlaySound("Blah", true);
    //        transform.position = new Vector3(offsetLeftX, transform.position.y, transform.position.z);
    //    }
    //}
    
    void TeleportSide()
    {
        if (transform.position.y < offsetLeftX)
        {
            SoundManager.PlaySound("Blah", true);
            transform.position = new Vector3(transform.position.x, offsetRightX, transform.position.z);

            transform.eulerAngles = Vector3.zero;
            spriterender.flipX = true;
        }
        else if (transform.position.y > offsetRightX)
        {
            SoundManager.PlaySound("Blah", true);
            transform.position = new Vector3(transform.position.x, offsetLeftX, transform.position.z);

            transform.eulerAngles = new Vector3(0, 0, 180f);
            spriterender.flipX = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.transform.SetParent(collision.transform);
        }
        if (collision.gameObject.CompareTag("FinishCheck"))
        {
            this.GetComponent<SpriteRenderer>().material.color = Color.red;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.transform.SetParent(null);
        }
    }

}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            