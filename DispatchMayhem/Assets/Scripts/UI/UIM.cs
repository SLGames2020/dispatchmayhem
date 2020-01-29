using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIM : MonoBehaviour
{
    private static UIM instance = null;
    public static UIM inst { get { return instance; } }

    public enum UIState { UNDEFINED, SELECT, ADD, DELETE };


    public GameObject listItemTemplate;
    public GameObject loadBoxContent;
    public GameObject vehicleBoxContent;

    [HideInInspector] public GameObject loadSelected;
    [HideInInspector] public GameObject vehicleSelected;

//    public List<Load> loads;
//    public List<Truck> trucks;

//    public UIState state = UIState.UNDEFINED;
//    public int loadIdx = -1;
//    public int truckIdx = -1;

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
    //void Update()
    //{
    //
    //}

    private void OnApplicationQuit()
    {
        instance = null;
    }

    /****************************************************************************
       AddTruckToList

        This method will add a truck gameobject to the truck list and 
        await being assigned a load

        Note that if this isn't a truck gameobject it will generate errors
        (TODO: error checking of the gameobject type)

    *****************************************************************************/
    public bool AddToTruckList(GameObject truckGO)
    {

        Debug.Log("Truck being added");
        GameObject template = Instantiate(listItemTemplate, this.transform.position, Quaternion.identity);
        template.GetComponent<ListObject>().listGO = truckGO;

        string txt = truckGO.GetComponent<Truck>().rigLabel;
        template.transform.parent = vehicleBoxContent.transform;
        template.GetComponentInChildren<Text>().text = txt;
        return true;
    }

    /****************************************************************************
       AddLoadToList 

        This method takes a load and adds it to the load lists to await assignment

        Note that if this isn't a load gameobject it will generate errors
        (TODO: error checking of the gameobject type)
    *****************************************************************************/
    public bool AddToLoadList(GameObject ldgo)
    {
        GameObject template = Instantiate(listItemTemplate, this.transform.position, Quaternion.identity);
        template.GetComponent<ListObject>().listGO = ldgo;

        Load ld = ldgo.GetComponent<Load>();
        string txt = ld.originLabel + " to " + ld.destinationLabel;
        template.transform.parent = loadBoxContent.transform;
        template.GetComponentInChildren<Text>().text = txt;
        return true;
    }

    public bool AssignLoad(GameObject assLoad)
    {
        loadSelected = assLoad;
        return true;
    }

    public bool AssignTruck(GameObject assTruck)
    {
        vehicleSelected = assTruck;
        return true;
    }
}
