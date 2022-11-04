using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPowerup : MonoBehaviour
{
    TankController tankController;
    [SerializeField]
    GameObject player1;
    [SerializeField]
    GameObject player2;


    // Start is called before the first frame update
    void Start()
    {
        tankController = player1.GetComponent<TankController>();
        tankController = player2.GetComponent<TankController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            tankController.player1PowerUp = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player2"))
        {
            tankController.player2PowerUp = false;
            Destroy(gameObject);
        }
    }
}
