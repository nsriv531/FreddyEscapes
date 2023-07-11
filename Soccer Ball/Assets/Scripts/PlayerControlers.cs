using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControlers : MonoBehaviour,IDamagable
{

    #region Refranceses
    private Rigidbody2D rb;
    private Animator anime;
    private AttackComponent attack;
    private TrailRenderer trailRenderer;
    private SpriteRenderer ballSprite;
    public Transform Net;
    public Camera camera;
    #endregion
    public float DashValue;
    float dashChargDuration = 1;
    float elapsetime;

    public bool isDashing;
    public float dashDuration = 0.2f;
    public int Dashspeed = 80;
    float dashElpssetime;
    private float dashAngle;

    public Vector2 DashAimDirection;
    public Vector2Int MovementDirection;

    public float elpseMoveTime;
    public float duration = 1f;
    public bool isHit;

    public bool canmove;
    public int DashAmount;

    private float dashUseReset = 1.5f;

    private float dashUseElapseTime;


    public GameEvent playerEvents;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anime= GetComponent<Animator>();
        attack= GetComponentInChildren<AttackComponent>();
        trailRenderer= GetComponent<TrailRenderer>();
        ballSprite= GetComponent<SpriteRenderer>();
        canmove = true;
        trailRenderer.enabled = false;
        
    }

    private void Update()
    {
        if(canmove && !isHit)
        {
         MovementDirection = new Vector2Int((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));
            anime.SetFloat("YAxisRoll",Mathf.Abs( MovementDirection.y));
            anime.SetFloat("YAxisRoll", Mathf.Abs(MovementDirection.x));


        }

        if (!isHit)
        {
             Dash();

        }
     

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canmove && MovementDirection != Vector2Int.zero && !isHit)
        {
            rb.AddForce(MovementDirection * 45);
            anime.Play("Roll Up");


        }
    }
    public void MoveToDirection(float speed)
    {
        rb.velocity = ( speed * transform.right);
    }
    public void AddForce(float speed)
    {
        rb.AddForce (speed * transform.right, ForceMode2D.Impulse);
    }
    public void TakeDamage(float takeDamage)
    {
            rb.velocity = Vector2.zero;
        Debug.Log("PlayerHit");
        if (!isHit)
        {

            playerEvents.OnPlayerDamaged?.Invoke();
            StopDashing(true);
            StartCoroutine(MoveToGoal());
        }
    }
    public void Dash()
    {
        


            if (Input.GetKey(KeyCode.Space) && !isDashing && DashAmount > 0)
            {
                MovementDirection= Vector2Int.zero;
                float completedTime = elapsetime/dashChargDuration;
                elapsetime += Time.deltaTime ;
                DashValue = Mathf.Lerp(0,1,completedTime);
                dashAngle = MouseDirection();
                rb.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                canmove= false;
                anime.Play("Roll Left");

           


            //playerEvents.onChargeValue?.Invoke(DashValue);
            }
            else if (Input.GetKeyUp(KeyCode.Space) && !isDashing && DashAmount >0)
            {
                DashAmount--;
                dashUseElapseTime = dashUseReset;
                dashElpssetime = Time.time;
                elapsetime= 0;
                rb.transform.rotation = Quaternion.AngleAxis(dashAngle, Vector3.forward);

                isDashing = true;
            }
            if(Time.time < dashElpssetime + dashDuration && isDashing)
             {
                    attack.AattackTheEnemy(0 );
                ballSprite.color = Color.red;
                trailRenderer.enabled = true;
            //change this is a quick fix

            MoveToDirection(Dashspeed );
            
             }
             else if(Time.time > dashElpssetime + dashDuration && isDashing)
              {
               StopDashing(false);
              }
        
        if(DashAmount<= 0)
        {
            dashUseElapseTime -= Time.deltaTime * 1f;
            playerEvents.OnChargCoolDown?.Invoke(dashUseElapseTime);

            if (dashUseElapseTime <= 0)
            {
                DashAmount++;
            }
        }
    }
   
    private IEnumerator MoveToGoal()
    {
        isHit = true;
        Vector2 angleTOnet = Net.transform.position - transform.position;
        float angle = Mathf.Atan2(angleTOnet.y, angleTOnet.x) * Mathf.Rad2Deg;
        rb.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        elpseMoveTime = duration;

        while(elpseMoveTime > 0){
            Debug.Log("start");
            rb.excludeLayers = LayerMask.GetMask("Enemy");
            elpseMoveTime-= Time.deltaTime;

            ballSprite.color= Color.yellow;
            rb.velocity = transform.right * 35;

            if(elpseMoveTime <=0)
            {
            isHit = false;
                break;

            }
           
             yield return null;
        }
        rb.excludeLayers = LayerMask.GetMask("Nothing");

        ballSprite.color = Color.white;


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isDashing)
        {
             attack.AattackTheEnemy(5 * DashValue);

        }
        StopDashing(true);

    }
    public void StopDashing(bool isHitWall)
    {
        AddForce(5);

        trailRenderer.enabled = false;

        canmove = true;
        ballSprite.color = Color.white;

        isDashing = false;
    }
    public float  MouseDirection()
    {
        Vector3 p = Input.mousePosition;
        DashAimDirection = camera.ScreenToWorldPoint(p) - transform.position;
        float angle = Mathf.Atan2(DashAimDirection.y, DashAimDirection.x) * Mathf.Rad2Deg;

        playerEvents.onChargeFirection?.Invoke(angle);
        return angle;
    }
    public bool ReturnPlayerIsHit()
    {
        return isHit;
    }
}


