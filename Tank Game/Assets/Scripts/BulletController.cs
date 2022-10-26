using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Ttl = Time to life
    public float BulletTtl = 5;
    public ParticleSystem bulletImpact;

    // Update is called once per frame
    void Update()
    {
        //BulletTtl = BulletTtl - Time.deltaTime;
        BulletTtl -= Time.deltaTime;
        if (BulletTtl <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        bulletImpact.Play();
        Destroy(gameObject);
    }
}
