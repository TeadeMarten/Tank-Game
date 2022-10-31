using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class TankController : MonoBehaviour
{
    [SerializeField]
    Transform barrelRotator;

    [SerializeField]
    Transform firePoint;

    [SerializeField]
    GameObject bulletToFire;
    [SerializeField]
    GameObject chargedBulletToFire;
    [SerializeField]
    float slopeCheckDistance;
    [SerializeField]
    float groundCheckRadius;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask whatIsGround;
    ScoreManager scoreManager;
    [SerializeField]
    GameObject score;

    public float chargeTime;
    private bool isCharging;
    private bool canShoot;
    private bool isGrounded;
    private float slopeDownAngle;

    public float MoveSpeed;

    public int PlayerNumber;

    public Material actiefMat;
    public Material inactiefMat;
    public bool isAanDeBeurt = false;

    private Vector2 colliderSize;
    private BoxCollider2D collider;
    private Vector2 slopeNormalPerp;

    // Start is called before the first frame update
    void Start()
    {
        //collider = colliderTransform.GetChild(0).GetComponent<boxcol>();
        GetComponent<SpriteRenderer>().material = inactiefMat;
        scoreManager = score.GetComponent<ScoreManager>();
        collider = GetComponent<BoxCollider2D>();

        colliderSize = collider.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerNumber == 1 && isAanDeBeurt)
        {
            barrelRotator.RotateAround(Vector3.forward, Input.GetAxis("Vertical") * Time.deltaTime);
        }
        if (PlayerNumber == 2 && isAanDeBeurt )
        {
            barrelRotator.RotateAround(Vector3.forward, Input.GetAxis("Vertical") * -Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Space) && chargeTime < 2 && isAanDeBeurt)
        {
            isCharging = true;
            if(isCharging == true)
            {
                chargeTime += Time.deltaTime;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space) && chargeTime < 2)
        {
            chargeTime = 0;
        }

        if (Input.GetKeyUp(KeyCode.Space) && isAanDeBeurt && chargeTime <=2)
        {
            GameObject b = Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            if (PlayerNumber == 1) 
            {
                b.GetComponent<Rigidbody2D>().AddForce(barrelRotator.right * 10, ForceMode2D.Impulse);
            }

            if (PlayerNumber == 2)
            {
                b.GetComponent<Rigidbody2D>().AddForce(barrelRotator.right * -10, ForceMode2D.Impulse);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) && chargeTime >= 2 && isAanDeBeurt)
        {
            GameObject cb = Instantiate(chargedBulletToFire, firePoint.position, firePoint.rotation);
            if(PlayerNumber == 1)
            {
                cb.GetComponent<Rigidbody2D>().AddForce(barrelRotator.right * 15, ForceMode2D.Impulse);
            }
            if(PlayerNumber == 2)
            {
                cb.GetComponent<Rigidbody2D>().AddForce(barrelRotator.right * -15, ForceMode2D.Impulse);
            }
            isCharging = false;
            chargeTime = 0;
        }
        if (PlayerNumber == 1 && isAanDeBeurt)
        {
            transform.Translate(Vector2.right * Input.GetAxisRaw("Horizontal") * MoveSpeed * Time.deltaTime);
        }
        if (PlayerNumber ==2 && isAanDeBeurt)
        {
            transform.Translate(Vector2.right * Input.GetAxisRaw("Horizontal") * MoveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isAanDeBeurt)
        {
            Invoke("WisselBeurt", 0.1f);
        }
    }
    private void FixedUpdate()
    {
        SlopeCheck();
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    void SlopeCheck()
    {
        Vector2 checkPosition = transform.position - new Vector3(0.0f, colliderSize.y / 2);

        SlopeCheckVertical(checkPosition);
    }

    void SlopeCheckHorizontal(Vector2 checkPosition)
    {

    }

    void SlopeCheckVertical(Vector2 checkposition)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkposition, Vector2.down, slopeCheckDistance, whatIsGround);

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal);
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }

    void ReleaseCharge()
    {
        GameObject cb = Instantiate(chargedBulletToFire, firePoint.position, firePoint.rotation);
        isCharging = false;
        chargeTime = 0;
        canShoot = true;
    }

    void WisselBeurt()
    {
        GameObject.Find("GameManager").GetComponent<TurnManager>().WisselBeurt();
    }

    public void ZetActief(bool b)
    {
        if (b)
        {
            isAanDeBeurt = true;
            GetComponent<SpriteRenderer>().material.color = Color.red;
        }
        else
        {
            isAanDeBeurt = false;
            GetComponent<SpriteRenderer>().material.color = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (PlayerNumber == 1)
            {
                scoreManager.player2Score++;
            }
            if(PlayerNumber == 2)
            {
                scoreManager.player1Score++;
            }
                
        }
    }
}