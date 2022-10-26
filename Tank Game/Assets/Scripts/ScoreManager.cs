using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;
    public Text p1Text;
    public Text p2Text;
    public Text charged;
    TankController tankController;
    [SerializeField]
    GameObject player1;
    [SerializeField]
    GameObject player2;

    private void Start()
    {
        tankController = player1.GetComponent<TankController>();
        tankController = player2.GetComponent<TankController>();
    }
    private void Update()
    {
        if(player1Score < 11)
        {
            p1Text.text = player1Score.ToString();
        }
        else
        {
            Debug.Log("Error, Score may not exceed 10");
        }
        if (player2Score < 11)
        {
            p2Text.text = player2Score.ToString();
        }
        else
        {
            Debug.Log("Error, Score may not exceed 10");
        }

    }


}
