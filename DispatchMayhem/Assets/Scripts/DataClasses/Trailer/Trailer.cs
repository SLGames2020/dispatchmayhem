using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trailer : MonoBehaviour
{
    public int age;
    public int maxSpace;
    public float health;
    public float cost;

    //public enum TrailerType
    //{
    protected const int FLATBED = 0;
    protected const int DROPDECK = 1;
    protected const int DRYVAN = 2;
    protected const int REFERVAN = 3;
    protected const int TANKER = 4;
    protected const int UNDEFINEDTRAILER = 5;
    protected const int TOTALTRAILERS = 5;

    //}

    public GameObject trailer;
    public int type;

    public int GetTrailerType()
    {
        return type;
    }

    public int GetMaxSpace()
    {
        return maxSpace;
    }

    public void TrailerCost(float cost)
    {
        if (age >= 1)
        {
            cost = 100000;
        }
        else if (age >= 5)
        {
            cost = cost / 2;
        }
        else if (age >= 10)
        {
            cost = cost / 4;
        }
        else if (age >= 15)
        {
            cost = cost / 6;
        }
        else if (age >= 20)
        {
            cost = cost / 8;
        }

        TrailerSpace(Random.Range(20000, 50000));
    }

    public void TrailerAge(float health)
    {
        if (age >= 1)
        {
            health = 1500;
        }
        else if (age >= 5)
        {
            health = health / 2;
        }
        else if (age >= 10)
        {
            health = health / 4;
        }
        else if (age >= 15)
        {
            health = health / 6;
        }
        else if (age >= 20)
        {
            health = health / 8;
        }
        else if (age >= 25)
        {
            health = 0;
            Debug.Log("This trailer is no longer good");
        }
    }

    public void TrailerSpace(int SpaceSize)
    {
        maxSpace = SpaceSize;

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

