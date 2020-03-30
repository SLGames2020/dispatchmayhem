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

}