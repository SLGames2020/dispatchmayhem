using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public Vector2 origin;
    public Vector2 destination;
    public int  typeIDX;

    public float weight = 0.0f;
    public float pay = 0.0f;

    public Load() { }
    public Load(Vector2 o, Vector2 d, int t=0, float w=1000.0f, float p=1.5f)
               { origin = o; destination = d; typeIDX = t; weight = w; pay = p; }

}
