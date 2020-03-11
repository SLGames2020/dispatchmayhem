using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobsPanel : BasePanel
{
    public GameObject listItemTemplate;
    public GameObject loadBoxContent;
    public GameObject ActiveloadBoxContent;

    public GameObject NewLoadBox;
    public GameObject ActiveLoadBox;

    public GameObject ActivelistItemTemplate;

    public bool ActiveShown = false;

    public GameObject ActiveBtn;
    public GameObject NewBtn;

    int numListItems = 0;


    public bool LoadActiveItems()
    {
        Debug.Log("Loading Active Items");
        foreach (Transform child in ActiveloadBoxContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Load ld in GM.inst.ActiveJobs)
        {
            Debug.Log("Loading Active Items 1");
            GameObject template = Instantiate(ActivelistItemTemplate, this.transform.position, Quaternion.identity);
//            template.GetComponent<ListObject>().listGO = ldgo;

            string txt = ld.originLabel + " to " + ld.destinationLabel;
            template.transform.SetParent(ActiveloadBoxContent.transform, false);
            template.GetComponentInChildren<Text>().text = txt;

            Text SrcText = template.transform.Find("Pickup").gameObject.GetComponent<Text>();
            SrcText.text = ld.originLabel;
            Text DestText = template.transform.Find("DropOff").gameObject.GetComponent<Text>();
            DestText.text = ld.destinationLabel;
            Text timeText = template.transform.Find("Time").gameObject.GetComponent<Text>();
            timeText.text = ld.DueDate.DayOfWeek.ToString() + " " + ld.DueDate.ToShortTimeString();

            GameObject LoadTypeIcon = template.transform.Find("LoadType").gameObject;
            
            GameObject driver1 = template.transform.Find("Driver1").gameObject;
            GameObject driver2 = template.transform.Find("Driver2").gameObject;
            GameObject driver3 = template.transform.Find("Driver3").gameObject;
            SetupDriverButton(driver1, 0, ld);
            SetupDriverButton(driver2, 1, ld);
            SetupDriverButton(driver3, 2, ld);

        }

        return true;
    }

    private void SetupDriverButton(GameObject driver, int DriverID, Load load)
    {
        driver.SetActive(false);
        if (load.assigned == true)
        {
            if (load.driverID == DriverID)
            {
                driver.SetActive(true);
            }
        }
        else
        {
            if (GM.inst.Trucks[DriverID] != null)
            {
                driver.SetActive(true);
                if (GM.inst.Trucks[DriverID].GetComponent<Movement>().currLoad == null)
                {
                    driver.GetComponent<Button>().onClick.AddListener(delegate { AssignLoad(load, DriverID); });
                }
                else
                {
                    Color FadeColor = driver.GetComponent<Image>().color;
                    FadeColor.r = 0.5f;
                    FadeColor.g = 0.5f;
                    FadeColor.b = 0.5f;
                    FadeColor.a = 0.25f;
                    driver.GetComponent<Image>().color = FadeColor;
                    driver.transform.GetChild(0).GetComponent<Image>().color = FadeColor;
                }
            }
        }
    }

    public void AssignLoad(Load assLoad, int driverIndex)
    {
        GM.inst.AssignJob(assLoad, driverIndex);
        LoadActiveItems();
        Close();
    }

    public void TakeLoad(GameObject assLoad, GameObject listItem)
    {
        GM.inst.TakeJob(assLoad);
        Destroy(listItem);
        numListItems--;
        ShowActiveLoads();
    }

    public override void Open()
    {
        this.gameObject.SetActive(false);
        ActiveShown = false;
        ShowActiveLoads();
    }
    

    public void ShowActiveLoads()
    {
        LoadActiveItems();
        ActiveBtn.GetComponent<Button>().interactable = false;
        NewBtn.GetComponent<Button>().interactable = true;
        NewLoadBox.SetActive(false);
        ActiveLoadBox.SetActive(true);

    }

    public void ShowNewLoads()
    {
        ActiveBtn.GetComponent<Button>().interactable = true;
        NewBtn.GetComponent<Button>().interactable = false;
        NewLoadBox.SetActive(true);
        ActiveLoadBox.SetActive(false);
        LoadActiveItems();
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
        template.transform.SetParent(loadBoxContent.transform, false);
        template.GetComponentInChildren<Text>().text = txt;

        Text SrcText = template.transform.Find("Pickup").gameObject.GetComponent<Text>();
        SrcText.text = ld.originLabel;
        Text DestText = template.transform.Find("DropOff").gameObject.GetComponent<Text>();
        DestText.text = ld.destinationLabel;
        Text timeText = template.transform.Find("Time").gameObject.GetComponent<Text>();
        timeText.text = ld.DueDate.DayOfWeek.ToString() + " " + ld.DueDate.ToShortTimeString();

        GameObject LoadTypeIcon = template.transform.Find("LoadType").gameObject;

        GameObject Btn = template.transform.Find("Button").gameObject;
        Btn.GetComponentInChildren<Text>().text = ld.value.ToString();

        Btn.GetComponent<Button>().onClick.AddListener(delegate { TakeLoad(ldgo, template); });
        numListItems++;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowActiveLoads();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
