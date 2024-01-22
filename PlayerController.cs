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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame (Dash)
    void Update()
    {
        if(dashRechargeCounter > 0)
        {
            dashRechargeCounter -= Time.deltaTime;
        }
        else
        {

            if(Input.GetButtonDown("Fire2"))
            {
                dashCounter = dashTime;

                ShowAfterimage();
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

        if (Input.GetButtonDown("Jump") && (isOnGround || canDoubleJump))
        {
            if (isOnGround)
            {
                canDoubleJump = true;
            }
            else
            {
                canDoubleJump = false;

                anim.SetTrigger("doubleJump");
            }
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }


        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(shorToFire,shotPoint.position,shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);

            anim.SetTrigger("shotFired");
        }








        //Abs = absolute value of something
        anim.SetBool("isOnGround", isOnGround);
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
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
