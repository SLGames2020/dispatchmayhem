﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTesting : MonoBehaviour
{
    public AudioClip test1;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            SoundManager.instance.Warning(test1);
        }
    }
}
