using System;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
//using System.Text.RegularExpressions;

using UnityEngine;

public class NM : MonoBehaviour
{
    private static NM instance = null;
    public static NM inst { get { return instance; } }


    private TcpClient mapboxClient;
    private StreamReader reader;
    private StreamWriter writer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
    {
        instance = null;
    }
}
