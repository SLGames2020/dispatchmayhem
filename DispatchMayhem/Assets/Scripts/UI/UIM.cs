using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIM : MonoBehaviour
{
    private static UIM instance = null;
    public static UIM inst { get { return instance; } }

    public enum UIState { UNDEFINED, SELECT, ADD, DELETE };

    public GameObject AnalyticsPanel;
    public GameObject ShopPanel;
    public GameObject SettingsPanel;
    public GameObject TrucksPanel;
    public GameObject JobsPanel;

    public GameObject[] Trucks;

    // JD TODO: revisit these being in here. Should be within a Jobs file on it's own, not in the main menu manager
    // for now just get this working with new architecture
    public GameObject listItemTemplate;
    public GameObject loadBoxContent;
    public GameObject vehicleBoxContent;

    [HideInInspector] public GameObject loadSelected;
    [HideInInspector] public GameObject loadSelectedListItem;
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

    // JD TODO: revisit these being in here. Should be within a Jobs file on it's own, not in the main menu manager
    // for now just get this working with new architecture
    /****************************************************************************
       AddTruckToList

        This method will add a truck gameobject to the truck list and 
        await being assigned a load

        Note that if this isn't a truck gameobject it will generate errors
        (TODO: error checking of the gameobject type)

    *****************************************************************************/
    public bool AddToTruckList(GameObject truckGO)
    {
        GameObject template = Instantiate(listItemTemplate, this.transform.position, Quaternion.identity);
        template.GetComponent<ListObject>().listGO = truckGO;

        string txt = truckGO.GetComponent<Truck>().rigLabel;
        template.transform.SetParent(vehicleBoxContent.transform, false);
        template.GetComponentInChildren<Text>().text = txt;

        template.GetComponent<Button>().onClick.AddListener(delegate { AssignTruck(truckGO); });

        return true;
    }
    // JD TODO: revisit these being in here. Should be within a Jobs file on it's own, not in the main menu manager
    // for now just get this working with new architecture
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
        template.transform.SetParent(loadBoxContent.transform,false);
        template.GetComponentInChildren<Text>().text = txt;

        template.GetComponent<Button>().onClick.AddListener(delegate { AssignLoad(ldgo, template); });

        return true;
    }
    // JD TODO: revisit these being in here. Should be within a Jobs file on it's own, not in the main menu manager
    // for now just get this working with new architecture
    public void AssignLoad(GameObject assLoad, GameObject listItem)
    {
        loadSelected = assLoad;
        loadSelectedListItem = listItem;
        JobsPanel.transform.Find("LoadBox").gameObject.SetActive(false);
        JobsPanel.transform.Find("VehicleBox").gameObject.SetActive(true);
        Debug.Log("Received assLoad");
    }
    // JD TODO: revisit these being in here. Should be within a Jobs file on it's own, not in the main menu manager
    // for now just get this working with new architecture
    public bool AssignTruck(GameObject assTruck)
    {
        vehicleSelected = assTruck;
        //JobsPanel.transform.Find("VehicleBox").gameObject.SetActive(false);
        JobsPanel.transform.Find("Assign").gameObject.SetActive(true);

        Debug.Log("Received assTruck");
        return true;
    }


    public void LoadPanel(GameObject Panel)
    {
        ClosePanel();
        Panel.SetActive(true);

        //JD TODO each panel should have it's own script and call load on it.
        JobsPanel.transform.Find("VehicleBox").gameObject.SetActive(false);
        JobsPanel.transform.Find("LoadBox").gameObject.SetActive(true);
        JobsPanel.transform.Find("Assign").gameObject.SetActive(false);
    }

    public void ClosePanel()
    {
        Debug.Log("close Clicked");
        AnalyticsPanel.SetActive(false);
        ShopPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        TrucksPanel.SetActive(false);
        JobsPanel.SetActive(false);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
