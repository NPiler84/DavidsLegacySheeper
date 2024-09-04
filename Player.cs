using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    // declaracao das variaveis do game
    
    [Header("Movement Variables")]
    public float speed = 10f;
    public float jumpForce = 200f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public bool isOnFloor = false;
    public bool isJumping = false;
    public float radius = 0.2f;
    
    int extraJumps = 1;

    [Header("Attack Variables")]
    public Transform attackCheck;
    public float radiusAttack;
    public LayerMask layerEnemy;
    float timeNextAttack;




    Rigidbody2D body;
    SpriteRenderer sprite;
    Animator anim;





    //funcao start atribui os valores das variaveis e associa aos metodos do MonoBehaiour
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }




    // monitoramento das entradas de controle e animacoes
    void Update()
    {
        
        //condicional de pulo

        isOnFloor = Physics2D.OverlapCircle( groundCheck.position, radius, whatIsGround );
        //   if (Input.GetButtonDown("Jump") && isOnFloor == true)
        //   isJumping = true;


        //condicional pulo duplo
        if(Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            isJumping = true;
            extraJumps --;
        }
        if(isOnFloor)
        {
            extraJumps = 1;
        }

        if(Input.GetButtonDown ("Fire1"))
        {
            anim.SetTrigger ("Attack");
            PlayerAttack();
        }
        /*
        if(timeNextAttack <= 0f)
        {
            if(Input.GetButtonDown("Fire1") && body.velocity == new Vector2 (0, 0))
            {
                anim.SetTrigger ("Attack");
                timeNextAttack = 0.2f;
                PlayerAttack();
            }
                else {

                    timeNextAttack -= Time.deltaTime;

                    }


            
        }*/
        PlayerAnimation();
        
    }





    //implemantacao de movimento
    void FixedUpdate()
    {

        float move = Input.GetAxis("Horizontal");

        body.velocity = new Vector2( move * speed, body.velocity.y );
        if ((move > 0 && sprite.flipX == true) || (move < 0 && sprite.flipX == false))
        {
            Flip();
        }


        if (isJumping)//pulo alto, pulo baixo

            {
                body.velocity = new Vector2 (body.velocity.x, 0f);
                body.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }


        if(body.velocity.y > 0f && !Input.GetButton("Jump")) 
        {
            body.velocity += Vector2.up * -0.8f;
        }

    }


    //funcoes para ajustes de sprites
    void Flip()
    {
        sprite.flipX = !sprite.flipX;
        attackCheck.localPosition = new Vector2( -attackCheck.localPosition.x, attackCheck.localPosition.y);
    }   
       void PlayerAnimation()
    {
        anim.SetFloat("VelX", Mathf.Abs(body.velocity.x));
        anim.SetFloat("VelY", Mathf.Abs(body.velocity.y));        
    }

     void PlayerAttack()
    {
        Collider2D[] enemiesAttack = Physics2D.OverlapCircleAll (attackCheck.position, radiusAttack, layerEnemy);
            for (int i = 0; i < enemiesAttack.Length; i ++)
            {
                enemiesAttack [i].SendMessage ("EnemyHit");
                Debug.Log(enemiesAttack [i].name);
            }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radius);
        Gizmos.DrawWireSphere(attackCheck.position, radiusAttack);
    }


  

}
