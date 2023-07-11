using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesPlayersAI : MonoBehaviour
{

    public Rigidbody2D rb;

    public BoxCollider2D bC1;
    public BoxCollider2D bC2;
    private SpriteRenderer spriteRenderer;
    public float xv;
    public float yv;

    public float speed;

    public GameObject ball;
    public GameObject ballColider;
    private float distance;
    public Animator anime;
    public bool kick = false;

    public float vunrable = 0;

    public GameObject ui;
    public int health = 1;
    public AudioClip[] deathSounds;
   

    private AudioSource audioplayer;
    public bool PlayDeathSound;

    public float kickCoolDown;
    public float kickCooltime = 5;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        ballColider = GameObject.FindGameObjectWithTag("PlayerColider");
        ui = GameObject.FindGameObjectWithTag("GameController");
        PlayDeathSound = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioplayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
            Vector2 direction = ball.transform.position - transform.position;
        if (vunrable <= 0)
        {
            distance = Vector2.Distance(transform.position, ball.transform.position);
            rb.velocity = direction.normalized * speed;

        }
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Debug.Log(angle);

            if (vunrable > 0)
            {
                vunrable -= Time.deltaTime;
            }
            else {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.rotation = 0;
            }
            if (kickCoolDown > 0)
            {
                kickCoolDown -= Time.deltaTime;
            }
        anime.SetBool("Up", (angle >= 45 && angle <= 135));
        anime.SetBool("Down", (angle <= -45 && angle >= -135));
        anime.SetBool("Left", (angle >= 135 || angle <= -135));
        if((angle >= 135 || angle <= -135))
        {
            spriteRenderer.flipX= true;
        }
        else
        {
            spriteRenderer.flipX= false;
        }
        anime.SetBool("Right", (angle <= 45 && angle >= -45));
        anime.SetBool("Kick", kick);
        
    }

    public void TakeDamage()
    {
        ///takes damge
      //  ui.GetComponent<GameUI>().score++;
        /*Destroy(gameObject);*/
        //Debug.Log("im Hit!!!");
        vunrable = 3;
        rb.constraints = RigidbodyConstraints2D.None;
        health--;
        if(health < 0 && PlayDeathSound)
        {
            PlayDeathSound= false;
            int number = Random.Range(0, deathSounds.Length);
            AudioClip deathsound = deathSounds[number];
            audioplayer.clip= deathsound;
            audioplayer.Play();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
          
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == ball && kickCoolDown == 0) {
            Debug.Log("kicked");
            kick = true;
           // ball.GetComponent<PlayerBallSliding>().TakeDamage();
            ball.GetComponent<Rigidbody2D>().velocity = ball.GetComponent<Transform>().position - gameObject.transform.position * speed * 3;
            kickCoolDown = kickCooltime;
        }
        if (collision.gameObject.CompareTag("wall") && vunrable > 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {

    }
    public void OnCollisionStay2D(Collision2D collision)
    {

    }
    public void OnTriggerExit2D(Collider2D collision)
    {

    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == ball)
        {
            kick = false;
        }
    }
    public IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(1f);
    }
}
