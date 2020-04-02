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

    public bool haveSave = false;

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
            if (Trucks[x] != null)
            {
                PlayerPrefs.SetInt("TruckType" + x, 0);         //only one truck type for now

                MapSupport trkmap = Trucks[x].GetComponent<MapSupport>();
                PlayerPrefs.SetFloat("PosX" + x, trkmap.gps.x);
                PlayerPrefs.SetFloat("PosY" + x, trkmap.gps.y);

                Movement trkmov = Trucks[x].GetComponent<Movement>();
                PlayerPrefs.SetInt("OnDuty" + x, trkmov.onDuty ? 1 : 0);
                PlayerPrefs.SetInt("HasLoad" + x, trkmov.hasLoad ? 1 : 0);

                if (trkmov.hasLoad) 
                {
                    Load trkload = trkmov.currLoad;
                    PlayerPrefs.SetInt("ProdType" + x, trkload.productType);
                    PlayerPrefs.SetFloat("OriginX" + x, trkload.origin.x);
                    PlayerPrefs.SetFloat("OriginY" + x, trkload.origin.y);
                    PlayerPrefs.SetFloat("DestX" + x, trkload.destination.x);
                    PlayerPrefs.SetFloat("DestY" + x, trkload.destination.y);
                    PlayerPrefs.SetFloat("LoadVal" + x, trkload.value);
                }
            }
        }

        int numoftrailers = Trailers.Length;
        PlayerPrefs.SetInt("NumOfTrailers", numoftrailers);

        for (x = 0; x < numoftrailers; x++)
        {
            if (Trailers[x] != null)
            {
                Trailer trltrail = Trailers[x].GetComponent<Trailer>();
                PlayerPrefs.SetInt("TrailType" + x, trltrail.GetTrailerType());
            }
        }

        haveSave = true;
        PlayerPrefs.SetInt("HaveSave", 1);
    }

    /*****************************************************************************
        LoadGame

        This method retrieves the last saved data from the Player Prefs, and 
        restore the state of the trucks and loads as they were saved.

        Note for now the list of loads from the cities is not restored and
        will be recreated as normal 

    *****************************************************************************/
    public void LoadGame()
    {
        int x = 0;
        int tmpint = 0;
        float tmpfloat = 0.0f;
        int numoftrucks = 0;

        if (PlayerPrefs.HasKey("NumOfTrucks"))
        {
            numoftrucks = PlayerPrefs.GetInt("NumOfTrucks");
        }

        if (numoftrucks > 1)
        {
            //TODO: Spawn in other trucks/drivers when we have them
        }

        for (x = 0; x < numoftrucks; x++)
        {
            if (Trucks[x] != null)
            {
                tmpint = PlayerPrefs.GetInt("TruckType" + x);                     //only one truck type for now

                MapSupport trkmap = Trucks[x].GetComponent<MapSupport>();
                trkmap.gps.x = PlayerPrefs.GetFloat("PosX" + x);
                trkmap.gps.y = PlayerPrefs.GetFloat("PosY" + x);

                Movement trkmov = Trucks[x].GetComponent<Movement>();
                trkmov.onDuty = (PlayerPrefs.GetInt("OnDuty" + x) == 1) ? true : false;
                trkmov.hasLoad = (PlayerPrefs.GetInt("HasLoad" + x) == 1) ? true : false;

                if (trkmov.hasLoad)
                {
                    Load trkload = Trucks[x].GetComponent<Load>();
                    trkload.productType = PlayerPrefs.GetInt("ProdType" + x);
                    trkload.origin.x = PlayerPrefs.GetFloat("OriginX" + x);
                    trkload.origin.y = PlayerPrefs.GetFloat("OriginY" + x);
                    trkload.destination.x = PlayerPrefs.GetFloat("DestX" + x);
                    trkload.destination.y = PlayerPrefs.GetFloat("DestY" + x);
                    trkload.value = PlayerPrefs.GetFloat("LoadVal" + x);
                }
            }
        }

        int numoftrailers = 0;
        if (PlayerPrefs.HasKey("NumOfTrailers"))
        {
            numoftrailers = PlayerPrefs.GetInt("NumOfTrailers");
        }

        for (x = 0; x < numoftrailers; x++)
        {
            if (Trailers[x] != null)
            {
                Trailer trltrail = Trailers[x].GetComponent<Trailer>();
                trltrail.type = PlayerPrefs.GetInt("TrailType" + x);
            }
        }
    }
}