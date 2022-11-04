using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField]
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio.volume = 0.3f;
    }
}
