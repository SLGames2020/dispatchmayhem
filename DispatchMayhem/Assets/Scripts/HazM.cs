using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazM : MonoBehaviour
{

    private static HazM instance = null;
    public static HazM Inst { get { return instance; } }

    public GameObject inspectorPrefab;
    public AudioClip warning;

    public List<Vector2> locsOnRoads;
    public float baseInspectionTime = 0.5f;

    public int maxInspectors = 1;

    public const int NORMINSPECTION = 7;
    public const int LOGCHECKINSPECTION = 9;
    public const int FULLINSPECTION = 10;


    private int numOfInspectors = 0;

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

        locsOnRoads = new List<Vector2>();
        locsOnRoads.Clear();

        //Debug.Log("Hazard Manager Awake");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (locsOnRoads.Count != 0)
        {
            if (numOfInspectors < maxInspectors)
            {
                OpenInspectionStation();
            }
        }
    }

    private void OnApplicationQuit()
    {
        instance = null;
    }

    /*********************************************************
        OpenInspectionStation

        This method will instantiate an inspection station
        and place it on a highway using the locsOnRoads, a list
        created from known good waypoints

    **********************************************************/
    private void OpenInspectionStation()
    {
        GameObject insp = Instantiate(inspectorPrefab, this.transform);
        MapSupport maps = insp.GetComponent<MapSupport>();
        maps.gps = locsOnRoads[Random.Range(0, locsOnRoads.Count)];
        maps.baseScale = new Vector3(6.0f, 1.5f, 4.5f);
        numOfInspectors++;
        Debug.Log("inspector spawned");
    }

    /*********************************************************
        CloseInspectionStation

        this method is called when an inspection station has 
        finished it's lifetime and needs to be removed.

        Cleanup is done here and a new inspection station
        will be spawned in the update above once numOfInspectors
        is drops below maxInspectors

    ***********************************************************/
    public void CloseInspectionStation(GameObject stn)
    {
        Destroy(stn);
        if (numOfInspectors > 0)
        {
            numOfInspectors -= 1;
        }
    }

    /***********************************************************
        GetInspectionTime

        This method randomly determines how long the inspection
        is and returns the time delay for the inspection

    ************************************************************/
    public float GetInspectionTime()
    {
        float waittime = 0.0f;
        int inspecttype = Random.Range(0, FULLINSPECTION);

        if (inspecttype < NORMINSPECTION)
        {
            waittime = baseInspectionTime;
        }
        else if (inspecttype < LOGCHECKINSPECTION)
        {
            waittime = baseInspectionTime * 2.0f;
        }
        else
        {
            waittime = baseInspectionTime * 4.0f;
        }
        SoundManager.instance.SoundEffect(warning);                 //for now just one warning for all, but different sounds could be used for different levels of inspections
        return waittime;
    }


    /********************************************************
        AddRoadLoc

        This method takes in a route vec2 from the network's
        route API call and adds it to a list so we can have
        a valid set of locations to place hazards on the road

        note that locations are randomly pulled from a route
        list so checking for repeats isn't really important

    **********************************************************/
    public void AddRoadLoc(Vector2 loc)
    {
        locsOnRoads.Add(loc);
        //Debug.Log("Hazard Location added: " + loc);
    }
}
