﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject loadingPanel;
    public GameObject gameOverPanel;
    public GameObject jobsButton;

    public Reputation repStatus;

    public Country[] countriesSupported = { new Country("CA", 400000) };         //the ISO 3166-2 codes, and minimum supported city size for the countries we're supporting (eventually should be made user configurable)
                                                                                 //this came from here (https://simplemaps.com/data/ca-cities) and is free as long as we credit them                                                                                

    public GameObject[] Trucks;
    public GameObject[] Trailers;
    public GameObject[] Drivers;
    public int totalDrivers = 1;

    public GameObject loadPrefab;
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
    void Start()
    {
        haveSave = PlayerPrefs.HasKey("HaveSave");
        Loading();
    }

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

    /************************************************************
        CullJobs

        This method removes loads for the active jobs list 
        if they are within 4 hours of deliver (ie no chance of
        being delivered on time). This will not only clean up the list
        but also free up space as we seem to only be able to present so
        many at once.

    *************************************************************/
    public void CullJobs()
    {
        List<Load> culls = new List<Load>();
        culls.Clear();

        foreach (Load ld in ActiveJobs)
        {
            DateTime tmchk = GameTime.inst.gmTime;
            tmchk.AddHours(3.0f);
            if (ld.DueDate < tmchk)
            {
                culls.Add(ld); 
            }
        }
        foreach (Load ld in culls)
        {
            ActiveJobs.Remove(ld);
        }
    }

    /****************************************************************************
       AddTruckToList
       As Trucks are added to the map they are added to our GameManager Script.

    *****************************************************************************/
    public void AssignLoadToTruck(int truckIndex, Load loaddata)
    {
        Trucks[truckIndex].GetComponent<Movement>().loadTruck(loaddata);
        this.GetComponent<TruckerPanel>().SetIcon(loaddata.productIcon, loaddata.driverID);
    }

    /****************************************************************************
        ActivateTruck

        This method activates a new truck and increase the number of trucks supported

    ******************************************************************************/
    public void ActivateTrucknDriver(int trkid)
    {
        Trucks[trkid].SetActive(true);
        Drivers[trkid].SetActive(true);
        totalDrivers = trkid + 1;
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

    //----------------------------------------------------------//
    //                   Loading Screen                         //
    //----------------------------------------------------------//

    public void Loading()
    {
        loadingPanel.SetActive(true);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        //string txt = loadingPanel.GetComponentInChildren<Text>().text;
        Text SrcText_0 = loadingPanel.transform.Find("Text (TMP)").gameObject.GetComponent<Text>();
        SrcText_0.text = "Loading ";

        for (int counter = 0; counter < 4; counter++)
        {
            SrcText_0.text = "Loading .";

            yield return new WaitForSecondsRealtime(0.25f);
            //Text SrcText_1 = loadingPanel.transform.Find("Loading .").gameObject.GetComponent<Text>();
            SrcText_0.text = "Loading . .";

            yield return new WaitForSecondsRealtime(0.25f);
            //Text SrcText_2 = loadingPanel.transform.Find("Loading ...").gameObject.GetComponent<Text>();
            SrcText_0.text = "Loading . . .";

            yield return new WaitForSecondsRealtime(0.25f);
            SrcText_0.text = "Loading ";
            yield return new WaitForSecondsRealtime(0.25f);
        }
        loadingPanel.SetActive(false);
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

        PlayerPrefs.SetFloat("money", Finances.inst.currCurrency);

        int numoftrucks = Trucks.Length;
        PlayerPrefs.SetInt("NumOfTrucks", numoftrucks);

        for (x = 0; x < numoftrucks; x++)
        {
            if (Trucks[x] != null)
            {
                PlayerPrefs.SetInt("TruckType" + x, 0);                         //only one truck type for now

                MapSupport trkmap = Trucks[x].GetComponent<MapSupport>();
                PlayerPrefs.SetFloat("PosX" + x, trkmap.gps.x);
                PlayerPrefs.SetFloat("PosY" + x, trkmap.gps.y);

                Movement trkmov = Trucks[x].GetComponent<Movement>();
                PlayerPrefs.SetInt("OnDuty" + x, trkmov.onDuty ? 1 : 0);
                PlayerPrefs.SetInt("HasLoad" + x, trkmov.hasLoad ? 1 : 0);

                if (trkmov.onDuty) 
                {
                    Load trkload = trkmov.currLoad;
                    PlayerPrefs.SetInt("ProdType" + x, trkload.productType);
                    PlayerPrefs.SetFloat("OriginX" + x, trkload.origin.x);
                    PlayerPrefs.SetFloat("OriginY" + x, trkload.origin.y);
                    PlayerPrefs.SetFloat("DestX" + x, trkload.destination.x);
                    PlayerPrefs.SetFloat("DestY" + x, trkload.destination.y);
                    PlayerPrefs.SetFloat("LoadVal" + x, trkload.value);
                    PlayerPrefs.SetFloat("HaulingCost" + x, trkload.haulingCost);
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

        if (haveSave)
        {
            Finances.inst.currCurrency = PlayerPrefs.GetFloat("money");

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

                    if (trkmov.onDuty)
                    {
                        GameObject ldgo = Instantiate(loadPrefab, Trucks[x].transform.position, Quaternion.identity);
                        Load trkload = ldgo.GetComponent<Load>();
                        trkload.productType = PlayerPrefs.GetInt("ProdType" + x);

                        if (trkmov.hasLoad)                        //if the load is already picked up, then use the current truck's location
                        {                                          //as the origin for finding the route (in "loadTruck" below)
                            trkload.origin.x = trkmap.gps.x;
                            trkload.origin.y = trkmap.gps.y;
                        }
                        else
                        {
                            trkload.origin.x = PlayerPrefs.GetFloat("OriginX" + x);
                            trkload.origin.y = PlayerPrefs.GetFloat("OriginY" + x);
                        }
                        trkload.destination.x = PlayerPrefs.GetFloat("DestX" + x);
                        trkload.destination.y = PlayerPrefs.GetFloat("DestY" + x);
                        trkload.value = PlayerPrefs.GetFloat("LoadVal" + x);
                        trkload.haulingCost = PlayerPrefs.GetFloat("HaulingCost" + x);

                        trkmov.loadTruck(trkload);
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
}