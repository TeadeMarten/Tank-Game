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

    public float MoveSpeed;

    public int PlayerNumber;

    public Material actiefMat;
    public Material inactiefMat;
    public bool isAanDeBeurt = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().material = inactiefMat;
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

        if (Input.GetKeyDown(KeyCode.Space) && isAanDeBeurt)
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
        if(PlayerNumber == 1 && isAanDeBeurt)
        {
            transform.Translate(Vector2.right * Input.GetAxisRaw("Horizontal") * MoveSpeed * Time.deltaTime);
        }
        if (PlayerNumber ==2 && isAanDeBeurt)
        {
            transform.Translate(Vector2.right * Input.GetAxisRaw("Horizontal") * MoveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isAanDeBeurt)
        {
            Invoke("WisselBeurt", 0.1f);
        }
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
}