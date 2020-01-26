using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    private static GM instance = null;
    public static GM inst { get { return instance; } }

    public List<GameObject> plrTrucks;
    public List<GameObject> openCities;
    public List<GameObject> wrldLoads;

    //**** CONSTANTS DECLRATIONS ****
    public int LOADSPAWNTIME = 60;                                           //number of seconds (on average) between load creations in a city

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

        plrTrucks.Clear();
        openCities.Clear();
        wrldLoads.Clear();
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //
    //}

    // Update is called once per frame
    //void Update()
    //{
    //
    //}

    private void OnApplicationQuit()
    {
        instance = null;
    }

    /******************************************************************************
        AddLoadToMaster/RemoveLoadFRomMasterList

        These methods are used to maintain a master list of all the loads 
        (ie info all in one place). Loads are added to the list when they
        are spawned in a city, and loads are removed from the list when 
        they are assigned to a truck

        Eventually the loads should be entirely in the city's domain and the
        list removed from the game manager (when we can click on a city)

    *******************************************************************************/
    public void AddLoadToMasterList(GameObject ld)
    {
        if (ld != null)
        {
            Debug.Log("load added to master");
            wrldLoads.Add(ld);
        }
        else
        {
            Debug.LogError("attempt to add Null load to master");
        }
    }

    public void RemoveLoadFromMasterList(GameObject ld)
    {
        if (ld != null)
        {
            wrldLoads.Remove(ld);
        }
        else
        {
            Debug.LogError("attempt to remove Null load from master");
        }
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