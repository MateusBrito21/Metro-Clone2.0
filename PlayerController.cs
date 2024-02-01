using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;

    public float moveSpeed;
    public float jumpForce;

    public Transform groundPoint;
    private bool isOnGround;
    public LayerMask whatIsGround;

    public Animator anim;

    public BulletController shorToFire;
    public Transform shotPoint;

    private bool canDoubleJump;

    public float dashSpeed, dashTime;
    private float dashCounter;

    public SpriteRenderer theSR, afterImage;
    public float afterImageLifetime, timeBetweenAfterImages;
    private float afterImageCounter;
    public Color afterImageColor;

    public float waitAfterDashing;
    private float dashRechargeCounter;

    public GameObject standing, ball;
    public float waitToBall;
    private float ballCounter;
    public Animator ballAnim;

    public Transform bombPoint;
    public GameObject bomb;
    private PlayerAbilityTracker abillities;

    public bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        abillities = GetComponent<PlayerAbilityTracker>();

        canMove = true;
    }

    // Update is called once per frame (Dash)
    void Update()
    {   
        if(canMove && Time.timeScale !=0)
        {

            if(dashRechargeCounter > 0)
            {
                dashRechargeCounter -= Time.deltaTime;
            }
            else
            {

                if(Input.GetButtonDown("Fire2") && standing.activeSelf && abillities.canDash)
                {
                    dashCounter = dashTime;

                    ShowAfterimage();

                    AudioManager.instance.PlaySFXAdjusted(7);
                }
            } 

            if(dashCounter >0)
            {
                dashCounter = dashCounter - Time.deltaTime;

                theRB.velocity = new Vector2(dashSpeed * transform.localScale.x, theRB.velocity.y);

                afterImageCounter -= Time.deltaTime;
                if(afterImageCounter <=0)
                {
                    ShowAfterimage();
                }

                dashRechargeCounter = waitAfterDashing;
            }
            else
            {

            
                //Mover Para o Lado
                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);
            
                //Esquerda Direita
                if(theRB.velocity.x < 0)
                {
                transform.localScale = new Vector3(-1f,1f ,1f);
                }
                else if(theRB.velocity.x > 0)
                {
                transform.localScale = Vector3.one;
                }

            }

            // Checando se esta no chão
            isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

            //$$ =  end
            //Pulando
            // || = or

            if (Input.GetButtonDown("Jump") && (isOnGround || ((canDoubleJump && abillities.canDoubleJump ))))
            {
                if (isOnGround)
                {
                    canDoubleJump = true;

                    AudioManager.instance.PlaySFXAdjusted(12);
                }
                else
                {
                    canDoubleJump = false;

                    anim.SetTrigger("doubleJump");

                    AudioManager.instance.PlaySFXAdjusted(9);
                }
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }

            //shoting
            if(Input.GetButtonDown("Fire1"))
            {
                if(standing.activeSelf)
                {
                Instantiate(shorToFire,shotPoint.position,shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);

                anim.SetTrigger("shotFired");

                AudioManager.instance.PlaySFXAdjusted(14);

                } else if(ball.activeSelf && abillities.canDropBomb)
                {
                    Instantiate(bomb, bombPoint.position, bombPoint.rotation);

                    AudioManager.instance.PlaySFXAdjusted(13);
                }
            }
            //ball mode
            if(!ball.activeSelf)
            {
                if(Input.GetAxisRaw("Vertical") < -.9f && abillities.canBecomeBall)
                {
                    ballCounter -= Time.deltaTime;
                    if(ballCounter <= 0)
                    {
                        ball.SetActive(true);
                        standing.SetActive(false);

                        AudioManager.instance.PlaySFX(6);
                    }

                }
                else
                {
                    ballCounter = waitToBall;
                }
            } 
            else
            {
                if(Input.GetAxisRaw("Vertical") > -.9f)
                {
                    ballCounter -= Time.deltaTime;
                    if(ballCounter <= 0)
                    {
                        ball.SetActive(false);
                        standing.SetActive(true);

                        AudioManager.instance.PlaySFX(10);
                    }
                    

                }
                else
                {
                    ballCounter = waitToBall;
                }
            }


        }  
        else
        {
            theRB.velocity =  Vector2.zero; 
        }


        if(standing.activeSelf)
        {
        //Abs = absolute value of something
        anim.SetBool("isOnGround", isOnGround);
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        }
        if(ball.activeSelf)
        {
            ballAnim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        }
    }

    public void ShowAfterimage()
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = theSR.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;
        
        Destroy(image.gameObject, afterImageLifetime);

        afterImageCounter = timeBetweenAfterImages;

    }
}
