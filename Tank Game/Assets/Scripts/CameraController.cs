using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    TurnManager turnManager;
    [SerializeField] GameObject gameManager;
    BulletController bulletController;
    [SerializeField] GameObject bullet;
    public bool bulletEnabled = false;

    private Vector2 p1Position;
    private Vector2 p2Position;
    private Vector2 bulletPosition;

    private void Start()
    {
        turnManager = gameManager.GetComponent<TurnManager>();
        bulletController = bullet.GetComponent<BulletController>();
    }

    // Update is called once per frame
    void Update()
    {
        p1Position = GameObject.Find("Player1").transform.position;
        p2Position = GameObject.Find("Player2").transform.position;
        if(bulletPosition != null && bulletEnabled == true)
        {
            bulletPosition = GameObject.FindGameObjectWithTag("Bullet").transform.position;
        }
        

        print("bulletEnabled is " + bulletEnabled);

        //If it's player one's turn then have the camera follow player one
        if (turnManager.spelerBeurt == 1 && bulletEnabled == false)
        {
            transform.position = new Vector3(p1Position.x, p1Position.y, transform.position.z);
        }

        //If it's player two's turn then have the camera follow player two
        if (turnManager.spelerBeurt == 2 && bulletEnabled == false)
        {
            transform.position = new Vector3(p2Position.x, p2Position.y, transform.position.z);
        }

        //If a bullet has been shot then have the camera follow the bullet
        if (bulletEnabled)
        {
            transform.position = new Vector3(bulletPosition.x, bulletPosition.y, transform.position.z);
        }
    }
}
