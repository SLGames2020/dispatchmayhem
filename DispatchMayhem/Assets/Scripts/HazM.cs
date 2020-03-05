using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazM : MonoBehaviour
{

    private static HazM instance = null;
    public static HazM Inst { get { return instance; } }

    public GameObject inspectorPrefab;

    public List<Vector2> locsOnRoads;

    public int maxInspectors = 1;


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

        Debug.Log("Hazard Manager Awake");
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
                GameObject insp = Instantiate(inspectorPrefab, this.transform);
                MapSupport maps = insp.GetComponent<MapSupport>();
                maps.gps = locsOnRoads[Random.Range(0, locsOnRoads.Count)];
                maps.baseScale = new Vector3( 0.450f, 0.10f, 0.30f );
                numOfInspectors++;
                Debug.Log("inspector spawned");
            }
        }
    }

    private void OnApplicationQuit()
    {
        instance = null;
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
        Debug.Log("Hazard Location added: " + loc);
    }
}
