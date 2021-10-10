﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovementTest : MonoBehaviour
{
    [SerializeField]
    private InputSchema control;
   
    
    public GameObject otherPlayer;
    
    private GameObject otherMeow;

    [SerializeField]
    private string tagFish;
    [SerializeField]
    private string tagFishUI;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private int fish;
    [SerializeField]
    private AudioSource meowAudio;

    private bool isGrounded;
    private bool jumped;
    private bool meowed;
    private float dir;
    private Rigidbody2D rb;

    private Vector2 meowDir;

    public Vector2 position;

    private Animator animator;

    private SpriteRenderer sprite;


    public Sprite meowRight;
    public Sprite meowLeft;

   



    
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        jumped = false;

        rb = GetComponent<Rigidbody2D>();




       


       animator = GetComponent<Animator>();
       sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        InputsUpdate();
    }

    private void InputsUpdate()
    {
        Animations();

        Move();
        Meow();
        Jump();
    }

    private void Animations()
    {
         
        if(dir < 0)
        {
            sprite.flipX = true;
        }
        else if(dir > 0)
        {
            sprite.flipX = false;
        }

        if(control.IsJumping() && isGrounded)
        {
            animator.SetTrigger("Jump");
        }

        if(control.IsMeowning() && isGrounded)
        {
            animator.SetTrigger("Meow");
        }

        
        if(dir > 0 || dir < 0 )
        {
            animator.SetBool("Walk",true);
        }
        else
        {
            animator.SetBool("Walk",false);
        }

    }

    private void Meow()
    {
        meowed = control.IsMeowning();

        if(meowed)
        {

            meowAudio.Play();
        }
    }

    IEnumerator DownMeow()
    {
    
        yield return new WaitForSeconds(0.6f);
        
       otherMeow.GetComponent<Image>().color = new Color(0,0,0,0);
    }


    private void Move()
    {
        dir = Input.GetAxis(control.axis);

        transform.position += new Vector3(dir * Time.deltaTime * speed, 0, 0);
    }

    private void Jump()
    {
        jumped = control.IsJumping();

        if (jumped)
        {

            if(isGrounded)
            {
                rb.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

       
    }


    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


}