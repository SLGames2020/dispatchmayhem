using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityM : MonoBehaviour
{
    private static CityM instance = null;
    public static CityM inst { get { return instance; } }

    public GameObject cityPrefab;
    public List<GameObject> openCities;

    //**** CONSTANTS DECLRATIONS ****
    public int LOADSPAWNTIME = 120;                                           //number of seconds (on average) between load creations in a city

    void Awake()
    {
        Debug.Log("City Manager Awake");

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        openCities.Clear();
    }


    // Start is called before the first frame update
    void Start()
    {
        //foreach (Country cntry in GM.inst.countriesSupported)
        //{
        //    string tmppath = "F:/School/Semester 4/Game 403 - Capstone Project Management/dispatchmayhem/DispatchMayhem/Assets/Prefabs/City/CitiesLists/" + cntry.ISOcode + ".json";
        //    TextAsset jsonfile = Resources.Load<TextAsset>(tmppath);

        //    Debug.Log(tmppath);
        //    Debug.Log(jsonfile);

        //    CitylistJSONobj cntrydata = JsonUtility.FromJson<CitylistJSONobj>(jsonfile.text);
        //    foreach (CityJSONobj cty in cntrydata.cities)
        //    {
        //        if (cty.population >= cntry.POPcutoff)
        //        {
        //            GameObject ctyGO = Instantiate(cityPrefab, Vector3.zero, Quaternion.identity);
        //            ctyGO.transform.parent = this.transform;
        //            City ctyscript = ctyGO.GetComponent<City>();
        //            ctyscript.label = cty.city;
        //            ctyscript.stillOpen = true;
        //            ctyGO.GetComponent<MapSupport>().gps = cty.GetGPS();
        //            openCities.Add(ctyGO);
        //            Debug.Log("City: " + cty.city + " @" + cty.GetGPS());
        //        }
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
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
