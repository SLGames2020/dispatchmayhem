/*******      City Manager        *******
Responsible for loading city data and
placing the supported cities on the map.
Deactivating cities will also be done
here as well
**************************************/
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyM : MonoBehaviour
{
    private static CyM instance = null;
    //public TextAsset jsonfile;
    public static CyM inst { get { return instance; } }

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
        foreach (Country cntry in GM.inst.countriesSupported)
        {
            string tmppath = "./Assets/Scripts/City Manager Stuff/CitiesLists/" + cntry.ISOcode + ".json";
            //string tmppath = cntry.ISOcode;
            //TextAsset jsonfile1 = Resources.Load<TextAsset>(tmppath);
            string jsonstr = File.ReadAllText(tmppath, Encoding.UTF8);

            //Debug.Log(tmppath);
            //Debug.Log(jsonstr);

            //CitylistJSONobj cntrydata = JsonUtility.FromJson<CitylistJSONobj>(jsonfile.text);
            CitylistJSONobj cntrydata = JsonUtility.FromJson<CitylistJSONobj>(jsonstr);
            int cnt = 0;
            int cnt2 = 0;
            foreach (CityJSONobj cty in cntrydata.cities)
            {
                //if (cty.population >= cntry.POPcutoff)
                if (cty.admin == "Ontario")
                {
                    GameObject ctyGO = Instantiate(cityPrefab, Vector3.zero, Quaternion.identity);
                    ctyGO.transform.parent = this.transform;
                    ctyGO.name = cty.city;
                    City ctyscript = ctyGO.GetComponent<City>();
                    ctyscript.label = cty.city;
                    ctyscript.stillOpen = true;
                    ctyGO.GetComponent<MapSupport>().gps = cty.GetGPS();
                    openCities.Add(ctyGO);
                    //Debug.Log("City: " + cty.city + " @" + cty.GetGPS());
                    cnt++;
                }
                cnt2++;
                if (cnt > 10) break;               //only 10 cities for now
            }
            //Debug.Log("place " + cnt + "of " + cnt2);
        }
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
