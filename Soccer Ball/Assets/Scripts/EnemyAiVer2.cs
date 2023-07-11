using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiVer2 : MonoBehaviour,IDamagable
{
    private Rigidbody2D rb2d;
    private GameObject player;
    private Animator animator;
    public EnemyData enemyBrain;
    [SerializeField]
    private EnemyAiVer2 enemyAi;
    private SpriteRenderer spriteRenderer;
    private AttackComponent attackComponent;
    [SerializeField]
    public AudioClip[] DeathSounds;
    #region attacking
    public bool canAttack;
    float timeUntileAttack = 1.5f;
    float attackTimer;

    public bool agressive;
    float timeUntileAgrresive = 1f;
    float aggresivetimer;

    bool isgetingDirection;
    bool pursue;
    bool isAlive;
     float avoidradius = 3f;
    public GameObject ui;

    public Vector2 directionToPlayer;

    public AudioClip[] deathSounds;


    private AudioSource audioplayer;
    public bool PlayDeathSound;

    Vector2 speedadjustment;
    #endregion

    public float Health;
    private Transform Exit;
    private bool isGambling;
    

    private void Start()
    {
        attackTimer = Random.Range(0.1f, timeUntileAttack);
        aggresivetimer=Random.Range(0.2f,  timeUntileAgrresive);
        agressive= false;
        canAttack= false;
        rb2d= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAi = this;
        spriteRenderer= GetComponent<SpriteRenderer>();
        isgetingDirection = true;
        ui = GameObject.FindGameObjectWithTag("GameController");
        Exit = GameObject.FindGameObjectWithTag("Exit").transform;
        attackComponent = GetComponentInChildren<AttackComponent>();

    }

    private void Update()
    {
        if (!canAttack)
        {

            enemyBrain.CheckiFCanPersue(enemyAi);
        }
     
        if(isgetingDirection)
        {
            if (pursue)
            {
                GetDirection(player.transform);

            }
            else
            {
                GetDirection(Exit);
            }

        }

        

        if(pursue)
        {
            Attack();
        }
        else
        {
            Defend();
        }
    }
    private void FixedUpdate()
    {
    }
    
    public void Attack()
    {
        if( !agressive)
        {
            if(Vector2.Distance(transform.position, player.transform.position) > 8)
            {
               MoveToTarget(7,directionToPlayer, true);
                AgreasiveTimer();

            }
            else
            {
                MoveToTarget(0, directionToPlayer, true);

                AgreasiveTimer();
            }


        }
        else if(agressive)
        {
            if( !canAttack)
            {
                if (Vector2.Distance(transform.position, player.transform.position) > 5)
                {
                    MoveToTarget(10, directionToPlayer,true);
                    AttackTimer();

                }
                else
                {
                    MoveToTarget(0, directionToPlayer,true);
                    AttackTimer();

                }

            }
            else if (canAttack)
            {
               // Death();
               attackComponent.AattackTheEnemy(5);
                animator.SetBool("Kick", true);
            }
        }
    }
    public void Defend()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < 6 && ! canAttack)
        {
            AttackTimer();
        }
        else if(canAttack)
        {

            animator.SetBool("Kick", true);

        }
        else
        {
        }
       
    }
    public void MoveToTarget(int speed, Vector2 direction, bool IsAttacking)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        animator.SetBool("Up", (angle >= 45 && angle <= 135));
        animator.SetBool("Down", (angle <= -45 && angle >= -135));
        animator.SetBool("Left", (angle >= 135 || angle <= -135));

        

        if(angle >= 135 || angle <= -135)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        animator.SetBool("Right", (angle <= 45 && angle >= -45));
        //animator.SetBool("Kick", kick);
        if (IsAttacking)
        {
        rb2d.velocity = (direction + speedadjustment )* speed;

        }
        else
        {
            rb2d.velocity = (direction + speedadjustment) * speed;

        }
    }
    public void EnablePursue()
    {
        pursue = true;
    }
    public void AgreasiveTimer()
    {
        aggresivetimer -= Time.deltaTime;
        if (aggresivetimer < 0)
        {
            agressive= true;
        }
    }
    public void AttackTimer()
    {
        attackTimer -= Time.deltaTime;
        if(attackTimer < 0)
        {
            canAttack= true;
        }
        if(attackTimer < 0.6f)
        {
            spriteRenderer.color = Color.red;
        }
        if(attackTimer < 0.5f)
        {
            isgetingDirection= false;
        }

    }
  
    public void DisableAttackAnimation()
    {
        canAttack = false;
        agressive = false;
        attackTimer = Random.Range(1f, timeUntileAttack);
        aggresivetimer = timeUntileAttack;
        animator.SetBool("Kick", false);
        spriteRenderer.color = Color.white;
        isgetingDirection = true;

    }
    public void AddforceToDirection()
    {
        rb2d.velocity = directionToPlayer * 40;
    }
    /**
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isGambling)
            {
                Debug.Log("kicked");
                //player.GetComponent<PlayerBallSliding>().TakeDamage(5);
                //player.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Transform>().position - gameObject.transform.position * 1.5f * 3;

            }
            else
            {
                

            }
        }

    }
    **/
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0) {
            Death();
            gameObject.SetActive(false);
            
        
        }
        Debug.Log("Damage: "+ damage);
    }

  
    public void GetDirection(Transform target)
    {
        directionToPlayer = target.transform.position - transform.position;
        directionToPlayer.Normalize();
        speedadjustment = Vector2.zero;
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, avoidradius, 7);
        foreach (Collider2D enemy in nearbyEnemies)
        {
            if (enemy != gameObject) // Ignore self
            {
                if (enemy.transform.position.x > transform.position.x)
                {
                    speedadjustment = new Vector2(-0.5f, 0);
                }
                else if (enemy.transform.position.x < transform.position.x)
                {
                    speedadjustment = new Vector2(1f, 0);
                }
            }
        }
    }
    public void Death()
    {
        
            enemyBrain.EnemyDead();
            DiablePursue();

        
    }
    public bool GetPursue()
    {
        return pursue;
    }
    public void DiablePursue()
    {
        pursue = false;
    }
   
}












