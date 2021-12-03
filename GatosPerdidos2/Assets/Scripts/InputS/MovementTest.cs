using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;

public class MovementTest : NetworkBehaviour
{
    [SerializeField]
    private InputSchema control;

    [SerializeField]
    private Camera playerCamera;
    
    public GameObject otherPlayer;
    
    private GameObject otherMeow;

   // [SerializeField]
    //private string tagFish;
   // [SerializeField]
    //private string tagFishUI;

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

    [SerializeField]
    private CameraConfig cameraConfig;

    private int PlayerID;

    private string fishTag;

    private GameManager gameManager;

    //public Sprite meowRight;
    //public Sprite meowLeft;

    
    // Start is called before the first frame update
    void Start()
    {
        fish = 0;

        fishTag = "Fish1";

        isGrounded = true;
        jumped = false;

        rb = GetComponent<Rigidbody2D>();

       animator = GetComponent<Animator>();
       sprite = GetComponent<SpriteRenderer>();

      

        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

        InitiateCamera();
    }



    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
              InputsUpdate();
        }
          
    }

    private void InitiateCamera()
    {
        if(isLocalPlayer)
        cameraConfig.EnableCamera();

        switch (netId)
        {
            case 1: 
                cameraConfig.NoCatMask(1);
                this.gameObject.layer = LayerMask.NameToLayer("Cat1");
                PlayerID = 1;

                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("RealArt/Cats/StarCat/Player1");

                fishTag = "Fish1";

                gameManager.gato1 = this.gameObject;

                //trocar player mask
                break;
            case 2: 
                cameraConfig.NoCatMask(0);
                this.gameObject.layer = LayerMask.NameToLayer("Cat2");
                cameraConfig.IsPlayerTwo();//change rotation camera
                PlayerID = -1;

                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("RealArt/Cats/MoonCat/Player2");

                fishTag = "Fish2";

                gameManager.gato2 = this.gameObject;
                // trocar player mask
                break;
            case 3:
                cameraConfig.NoCatMask(1);
                this.gameObject.layer = LayerMask.NameToLayer("Cat1");
                PlayerID = 1;

                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("RealArt/Cats/StarCat/Player1");

                fishTag = "Fish1";

                gameManager.gato1 = this.gameObject;
                break;
            case 4:
                cameraConfig.NoCatMask(0);
                this.gameObject.layer = LayerMask.NameToLayer("Cat2");
                cameraConfig.IsPlayerTwo();//change rotation camera
                PlayerID = -1;

                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("RealArt/Cats/MoonCat/Player2");

                fishTag = "Fish2";

                gameManager.gato2 = this.gameObject;
                break;

        }
        
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
            if(PlayerID == 1)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
            
        }
        else if(dir > 0)
        {
            if(PlayerID == 1)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
            
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
            //can win?
            
            meowAudio.Play();
            gameManager.TryWin();
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

        transform.position += new Vector3(PlayerID * dir * Time.deltaTime * speed, 0, 0);
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
        
        if(collision.gameObject.CompareTag(fishTag))
        {
            fish += 1;

            //preciso chamar o gamemanager
            gameManager.ChangePexe(fish, PlayerID);

            Destroy(collision.gameObject);
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