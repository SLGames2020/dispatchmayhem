using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    private static GM instance = null;
    public static GM inst { get { return instance; } }

    public List<Truck> plrTrucks;
    public List<City> openCities;
    public List<Load> wrldLoads;

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

        plrTrucks = new List<Truck>();
        openCities.Clear();
        wrldLoads = new List<Load>();

    }
    // Start is called before the first frame update
    void Start()
    {



    }

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
    public void AddLoadToMasterList(Load ld)
    {
        wrldLoads.Add(ld);
    }

    public void RemoveLoadFromMasterList(Load ld)
    {
        wrldLoads.Remove(ld);
    }

}
