using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckType : MonoBehaviour
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
    public TruckType(int a, int h, int minSP, int maxSP,  int maxS, float c, float maxF)
    {
        age = a;
        health = h;
        maxSpeed = maxSP;
        maxSpace = maxS;
        cost = c;
        maxFuel = maxF;
    }

    /* public void SemiTrucks()
     {
         TruckType[] SemiTruck =
         {
             new SemiTruck1(0, 500, 0, 0, 115, 125000.00f, 500.00f);
             new SemiTruck2(0, 550, 0, 0, 115, 150000.00f, 600.00f);
             new SemiTruck3(0, 600, 0, 0, 115, 175000.00f, 700.00f);
             new SemiTruck4(0, 650, 0, 0, 115, 200000.00f, 800.00f);
             new SemiTruck5(0, 700, 0, 0, 115, 225000.00f, 900.00f);
             new SemiTruck6(0, 750, 0, 0, 115, 250000.00f, 1000.00f);
         }
     }*/

    /*public void BoxTruck()
    {
            new Boxtruck1(0, 200, 0, 200, 70, 25000.00f, 100.00f);
            new BoxTruck2(0, 250, 0, 250, 80, 50000.00f, 200.00f);
            new BoxTruck3(0, 300, 0, 300, 90, 75000.00f, 300.00f);
            new BoxTruck4(0, 350, 0, 350, 100, 100000.00f, 400.00f);
            new BoxTruck5(0, 400, 0, 400, 110, 125000.00f, 450.00f);
            new BoxTruck6(0, 450, 0, 450, 120, 150000.00f, 500.00f);
    }*/

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