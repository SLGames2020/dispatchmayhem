using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckType
{
    public int age;
    public int health;
    public int minSpeed = 0;
    public int maxSpeed;
    public int minSpace = 0;
    public int maxSpace;
    public float cost;
    public float minFuel = 0.0f;
    public float maxFuel;

    public void UsedTruck()
    {
        if (age >= 5)
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
    }

    public void Repair()
    {
        if (age >= 1)
        {
            health = health - 1;

        }
        else if (age >= 5)
        {
            health = health - 5;
        }
        else if (age >= 10)
        {
            health = health - 10;
        }
        else if (age >= 15)
        {
            health = health - 15;
        }
        else if (age >= 20)
        {
            health = health - 20;
        }

        if (health <= 75)
        {
            Debug.Log("Your Truck needs a check up");
        }
        else if (health <= 50)
        {
            Debug.Log("Your Truck needs a repair");
        }
        else if (health <= 25)
        {
            Debug.Log("Your truck is falling apart");
        }
        else if (health == 0)
        {
            Debug.Log("Your Truck broke down");
        }

        if (age > 25)
        {
            health = health - health;
            Debug.Log("This truck is too old to be put on the road it's time to replace it");
        }
    }
}
}
