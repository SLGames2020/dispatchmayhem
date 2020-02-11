using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trailer : MonoBehaviour
{
    public int age;
    public int maxSpace;
    public float health;
    public float cost;

    public enum TrailerType
    {
        FLATBED,
        DROPDECK,
        DRYVAN,
        REFERVAN,
        TANKER
    }

    public GameObject trailer;
    public TrailerType type;

    public void maxLoads(int space, int weight)
    {
         
    }

    public void TrailerCost()
    {
        if (age >= 1)
        {
            cost = 70000;
        }
        else if (age >= 5)
        {
            cost = 70000 / 2;
        }
        else if (age >= 10)
        {
            cost = 70000 / 4;
        }
        else if (age >= 15)
        {
            cost = 70000 / 6;
        }
        else if (age >= 20)
        {
            cost = 70000 / 8;
        }
    }

    public void TrailerAge()
    {
        if (age >= 1)
        {
            health = 15000f;
        }
        else if (age >= 5)
        {
            health = 15000f / 2;
        }
        else if (age >= 10)
        {
            health = 15000f / 4;
        }
        else if (age >= 15)
        {
            health = 70000 / 6;
        }
        else if (age >= 20)
        {
            health = 70000 / 8;
        }
        else if (age >= 25)
        {
            health = 0;
            Debug.Log("This trailer is no longer good");
        }
    }

    public void TrailerSpace()
    {
        if(maxSpace >= 20000)
        {
            cost = cost * 2;
        }
        else if(maxSpace >= 30000)
        {
            cost = cost * 3;
        }
        else if(maxSpace >= 40000)
        {
            cost = cost * 4;
        }
        else if(maxSpace >= 50000)
        {
            cost = cost * 5;
        }
    }
}

