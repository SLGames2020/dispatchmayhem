using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckerPanel : BasePanel
{
    public Text xp;
    public Text lvl;
    public Text wage;
    public Text hours;
    public Text source;
    public Text destination;


    public GameObject Oil;
    public GameObject Gas;
    public GameObject Box;
    public GameObject Food;
    public GameObject Milk;
    public GameObject Dairy;
    public GameObject Logs;
    public GameObject ibeams;
    public GameObject LoadIcon = null;

    private DateTime pTime;
    private int pHour;

    public GameObject truck;

    struct TruckerInfo
    {
        public int playexp;
        public int totalxp;
        public int playlvl;
        public int playwage;
        public int playhrs;
    }

    TruckerInfo[] TruckData;

    public int DriverID;

   // public static TruckerPanel instance = null;

    private void Awake()
    {
        /*if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }*/

        TruckData = new TruckerInfo[3];
        //DontDestroyOnLoad(gameObject);
    }


    public void levelup()
    {
        TruckData[DriverID].playlvl = 1;
        TruckData[DriverID].playwage = 1;
        TruckData[DriverID].playexp = TruckData[DriverID].totalxp / 2;

        while (TruckData[DriverID].playexp >= (150 * TruckData[DriverID].playlvl))
        {
            TruckData[DriverID].playwage = TruckData[DriverID].playwage + (TruckData[DriverID].playlvl / 2);
            TruckData[DriverID].playexp -= (150 * TruckData[DriverID].playlvl);
            TruckData[DriverID].playlvl++;
        }
    }


    public void SetIcon(string productName, int DriveID)
    {
        if (productName == "milk")
        {
            LoadIcon = Milk;
        }
        else if (productName == "oil")
        {
            LoadIcon = Oil;
        }
        else if (productName == "boxes")
        {
            LoadIcon = Box;
        }
        else if (productName == "fruit")
        {
            LoadIcon = Food;
        }
        else if (productName == "gas")
        {
            LoadIcon = Gas;
        }
        else if (productName == "dairy")
        {
            LoadIcon = Dairy;
        }
        else if (productName == "wood")
        {
            LoadIcon = Logs;
        }
        else if (productName == "ibeams")
        {
            LoadIcon = ibeams;
        }
    }

    public void UpdateLoads()
    {
        Load currLoad = GM.inst.Trucks[DriverID].GetComponent<Movement>().currLoad;
        if (currLoad != null)
        {
            source.text = currLoad.originLabel;

            destination.text = currLoad.destinationLabel;
            
            Oil.SetActive(false);
            Box.SetActive(false);
            Food.SetActive(false);
            Milk.SetActive(false);

            if (LoadIcon == Oil)
            {
                Oil.SetActive(true);
            }
            else if (LoadIcon == Box)
            {
                Box.SetActive(true);
            }
            else if (LoadIcon == Food)
            {
                Food.SetActive(true);
            }
            else if (LoadIcon == Milk)
            {
                Milk.SetActive(true);
            }
                

            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        xp.text = "" + TruckData[DriverID].playexp;
        lvl.text = "" + TruckData[DriverID].playlvl;
        wage.text = "" + TruckData[DriverID].playwage;
    }

    // Update is called once per frame
    void Update()
    {
        float wage1;
        xp.text = "" + TruckData[DriverID].totalxp;
        lvl.text = "" + TruckData[DriverID].playlvl;
        wage.text = "" + TruckData[DriverID].playwage;
        TruckData[DriverID].totalxp = (int)truck.GetComponent<Movement>().LifeTimehaulDistance;
        wage1 = GameTime.inst.gmHoursToRealSeconds(TruckData[DriverID].playwage) * Time.deltaTime;
        Finances.inst.AddMoney(-wage1);
        levelup();
        UpdateLoads();
    }
}

