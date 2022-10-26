using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    int timesShot = 0;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timesShot <9)
        {
            animator.SetBool("isOnFire", false);
        }
        if (timesShot == 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
        if(timesShot == 1)
        {
            spriteRenderer.sprite = sprites[1];
        }
        if(timesShot == 2)
        {
            spriteRenderer.sprite = sprites[2];
        }
        if (timesShot == 3)
        {
            spriteRenderer.sprite = sprites[3];
        }
        if (timesShot == 4)
        {
            spriteRenderer.sprite = sprites[4];
        }
        if (timesShot == 5)
        {
            spriteRenderer.sprite = sprites[5];
        }
        if (timesShot == 6)
        {
            spriteRenderer.sprite = sprites[6];
        }
        if (timesShot == 7)
        {
            spriteRenderer.sprite = sprites[7];
        }
        if (timesShot == 8)
        {
            spriteRenderer.sprite = sprites[8];
        }
        if (timesShot == 9)
        {
            spriteRenderer.sprite = sprites[9];
        }
        if (timesShot >9)
        {
            animator.SetBool("isOnFire", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            timesShot++;
        }
    }
}
