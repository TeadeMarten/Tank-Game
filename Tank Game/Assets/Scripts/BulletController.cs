using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Ttl = Time to life
    public float BulletTtl = 5;
    public ParticleSystem bulletImpact;

    CameraController cameraController;
    [SerializeField] GameObject mainCamera;
    TurnManager turnManager;
    [SerializeField] GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        turnManager = gameManager.GetComponent<TurnManager>();
        mainCamera = GameObject.FindGameObjectWithTag("CameraHolder");
        cameraController = mainCamera.GetComponent<CameraController>();
        cameraController.bulletEnabled = true;
        print("Ran start on bullet");
    }

    // Update is called once per frame
    void Update()
    {
        //BulletTtl = BulletTtl - Time.deltaTime;
        BulletTtl -= Time.deltaTime;
        if (BulletTtl <= 0)
        {
            cameraController.bulletEnabled = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        cameraController.bulletEnabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        bulletImpact.Play();
        Destroy(gameObject);
    }
}
