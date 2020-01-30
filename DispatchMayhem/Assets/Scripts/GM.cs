using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    private static GM instance = null;
    public static GM inst { get { return instance; } }

    public List<GameObject> openCities;

    //**** CONSTANTS DECLRATIONS ****
    public int LOADSPAWNTIME = 120;                                           //number of seconds (on average) between load creations in a city

    void Awake()
    {
        Debug.Log("Game Manager Awake");

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        openCities.Clear();
        //wrldLoads.Clear();
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //
    //}

    // Update is called once per frame
    void Update()
    {
        //loadCount = wrldLoads.Count;
    }

    private void OnApplicationQuit()
    {
        instance = null;
    }

    /*****************************************************************************
        AddCityTo/RemoveCityFromMasterList

        This methods are for updating the master list of all the cities
        currently available to the player. This lists are for the sake of
        convience and cities are responsible for adding themselves to the 
        list (when they are instantiated in their Start method).

        Perhaps through competition with other players, cities could be lost
        and removed from the list.

    ******************************************************************************/
    public void AddCityToMasterList(GameObject cty)
    {
        if (cty != null)
        {
            openCities.Add(cty);
        }
        else
        {
            Debug.LogError("attempt to add Null city to master");
        }
    }

    public void RemoveCityFromMasterList(GameObject cty)
    {
        if (cty != null)
        {
            openCities.Remove(cty);
        }
        else
        {
            Debug.LogError("attempt to remove Null city from master");
        }
    }
    
}