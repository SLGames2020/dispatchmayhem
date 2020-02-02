using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherEvents : MonoBehaviour
{
    void RainOrSnow()
    {
        int temperature = 0;
        float rainAmount = 0f;
        float snowAmount = 0f;
        bool isRain = false;
        bool isSnow = false;

        if(temperature >= 0)
        {
            isRain = true;
            rainAmount = Random.Range(0f, 100f);
            if(rainAmount >= 30f)
            {
                Debug.Log("Heavy rain is expected be careful");
            }
            else if(rainAmount >= 50f)
            {
                Debug.Log("Chance of roads being flooded be careful");
            }
            else if(rainAmount >= 100f)
            {
                Debug.Log("Roads are flooded do not drive through here");
            }
        }
        else if(temperature <= 0)
        {
            isRain = true;
            snowAmount = Random.Range(0f, 100f);
            if(snowAmount >= 10f)
            {
                Debug.Log("Minor snow is occuring");
            }
            else if(snowAmount >= 20f)
            {
                Debug.Log("Moderate amount of snow fallening be careful driving");
            }
            else if(snowAmount >= 50f)
            {
                Debug.Log("Blizzard occuring do not drive!");
            }
            
        }
    }

    void Wind()
    {
        float windSpeed = Random.Range(0f, 100f);
        bool isWindStrom = false;
        if(windSpeed >= 30)
        {
            Debug.Log("Strong winds are occuring be cautious on road");
        }
        else if(windSpeed >= 40)
        {
            Debug.Log("Very strong winds, driving conditions greatly impared");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        RainOrSnow();
        Wind();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
