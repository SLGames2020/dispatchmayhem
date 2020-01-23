using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load
{
    public City origin;
    public City destination;
    public int  typeIDX;

    public int weight = 0;
    public float weightLiquid = 0.0f;
    public float pay = 0.0f;


    class Liquid:Load
    {
        void Gas()
        {
            weightLiquid = 2000.00f;
            pay = 2000.00f;
        }

        void Milk()
        {
            weightLiquid = 1000.00f;
            pay = 1500.00f;
        }

        void Water()
        {
            weightLiquid = 1500.00f;
            pay = 2500.00f;
        }
    }

    class Solid:Load
    {
        void TP()
        {
            weight = 5;
            pay = 10.00f;
        }

        void Soup()
        {
            weight = 20;
            pay = 20.00f;
        }

        void SteelBar()
        {
            weight = 200;
            pay = 2000.00f;
        }

        void Contruction()
        {
            weight = 2000;
            pay = 10000.00f;
        }
    }

}
