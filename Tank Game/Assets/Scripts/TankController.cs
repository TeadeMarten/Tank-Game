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

    ScoreManager scoreManager;
    [SerializeField]
    GameObject score;

    public float chargeTime;
    private bool isCharging;
    private bool canShoot;

    public float MoveSpeed;

    public int PlayerNumber;

    public Material actiefMat;
    public Material inactiefMat;
    public bool isAanDeBeurt = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().material = inactiefMat;
        scoreManager = score.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("chargeTime is " + chargeTime);

        if(chargeTime == 2 || chargeTime > 2)
        {
            scoreManager.charged.text = ("Charged!");
        }
        else
        {
            scoreManager.charged.text = ("");
        }

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

    void ReleaseCharge()
    {
        GameObject cb = Instantiate(chargedBulletToFire, firePoint.position, firePoint.rotation);
        isCharging = false;
        chargeTime = 0;
        canShoot = true;
    }

    void WisselBeurt()
    {
        GameObject.Find("GameManager").GetComponent<turnManager>().WisselBeurt();
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