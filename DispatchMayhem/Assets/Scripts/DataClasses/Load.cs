using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public string originLabel;
    public Vector2 origin;
    public string destinationLabel;
    public Vector2 destination;

    // Commented out due to incorrect syntax preventing game from running
    /*int payouts[NUM_LOAD_TYPES][2] = 
        // MIN    //MAX
    {
        { 0.5f ,  1.0f}, // MILK
        { 0.5f ,  1.0f}, // OIL
        { 0.5f ,  1.0f}, // WATER
        { 0.5f ,  1.0f}, // MILK
        { 0.5f ,  1.0f}, // MILK
    }*/



    public int typeIDX;
    public float weight = 0.0f;

    // Commented out due to incorrect syntax preventing game from running
    /*public float Pay()
    {
        return Random.Range(payouts[typeIDX][0], payouts[typeIDX][1]);
    }*/

    public Load() { }
    //public Load(Vector2 o, Vector2 d, int t=0, float w=1000.0f, float p=1.5f)
    //           { origin = o; destination = d; typeIDX = t; weight = w; pay = p; }

}
