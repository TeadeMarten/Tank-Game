using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    Camera m_MainCamera;
    public Camera m_CameraTwo;

    // Start is called before the first frame update
    void Start()
    {
        //Get main camera
        m_MainCamera = Camera.main;
        //Enable main camera
        m_MainCamera.enabled = true;
        //Disable secondary camera
        m_CameraTwo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (m_MainCamera.enabled)
            {
                //Enable secondary camera
                m_CameraTwo.enabled = true;
                //Disable main camera
                m_MainCamera.enabled = false;
            }
            else if (!m_MainCamera.enabled)
            {
                //Disable secondary camera
                m_CameraTwo.enabled = false;
                //Enable main camera
                m_MainCamera.enabled = true;
            }
        }
    }
}
