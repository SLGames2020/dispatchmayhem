using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country
{
    public string ISOcode;
    public int POPcutoff;

    public Country(string iso, int popcut = 50000) { ISOcode = iso; POPcutoff = popcut; }
}


public class GM : MonoBehaviour
{
    private static GM instance = null;
    public static GM inst { get { return instance; } }

    public GameObject gameOverPanel;
    public GameObject jobsButton;

    public Country[] countriesSupported = { new Country("CA", 400000) };         //the ISO 3166-2 codes, and minimum supported city size for the countries we're supporting (eventually should be made user configurable)
                                                                                 //this came from here (https://simplemaps.com/data/ca-cities) and is free as long as we credit them                                                                                

    public GameObject[] Trucks;
    public GameObject[] Trailers;
    [HideInInspector] public List<Load> ActiveJobs = new List<Load>();

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

    public void TakeJob(GameObject assLoad)
    {
        ActiveJobs.Add(assLoad.GetComponent<Load>());
    }

    public void AssignJob(Load load, int DriverIndex)
    {
        load.driverID = DriverIndex;
        load.assigned = true;
        AssignLoadToTruck(DriverIndex, load);
    }

    /****************************************************************************
       AddTruckToList
       As Trucks are added to the map they are added to our GameManager Script.

    *****************************************************************************/
    public void AssignLoadToTruck(int truckIndex, Load loaddata)
    {
        Trucks[truckIndex].GetComponent<Movement>().loadTruck(loaddata);
    }


    /*****************************************************************************
        GameOver

        This method displays the game over message when the player's money reaches
        0. Note that for now, the map is disabled so it doesn't hide this panel

    ******************************************************************************/
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        jobsButton.SetActive(false);
    }

    /*****************************************************************************
        SaveGame

        This method rounds up all the active game data and stores it to the 
        Player prefs for now. 

        This should eventually be moved to a save file with support for multiple
        saves

    *****************************************************************************/
    public void SaveGame()
    {
        int x = 0;

        int numoftrucks = Trucks.Length;        
        PlayerPrefs.SetInt("NumOfTrucks", numoftrucks);
        
        for (x = 0; x < numoftrucks; x++)
        {
            PlayerPrefs.SetInt("TruckType" + x, 0);         //only one truck type for now

            MapSupport trkmap = Trucks[x].GetComponent<MapSupport>();
            PlayerPrefs.SetFloat("PosX" + x, trkmap.gps.x);
            PlayerPrefs.SetFloat("PosY" + x, trkmap.gps.y);

            Movement trkmov = Trucks[x].GetComponent<Movement>();
            PlayerPrefs.SetInt("HasLoad" + x, trkmov.hasLoad ? 1 : 0);

            Load trkload = Trucks[x].GetComponent<Load>();
            PlayerPrefs.SetInt  ("ProdType"+ x, trkload.productType  );
            PlayerPrefs.SetFloat("OriginX" + x, trkload.origin.x     );
            PlayerPrefs.SetFloat("OriginY" + x, trkload.origin.y     );
            PlayerPrefs.SetFloat("DestX"   + x, trkload.destination.x);
            PlayerPrefs.SetFloat("DestY"   + x, trkload.destination.y);
            PlayerPrefs.SetFloat("LoadVal" + x, trkload.value        );
        }

        int numoftrailers = Trailers.Length;
        PlayerPrefs.SetInt("NumOfTrailers", numoftrailers);

        for (x = 0; x < numoftrailers; x++)
        {
            Trailer trltrail = Trailers[x].GetComponent<Trailer>();
            PlayerPrefs.SetInt("TrailType" + x, trltrail.GetTrailerType());
        }
    }
}