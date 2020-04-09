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
    public Text ETA;
    public Text Hours;

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

    public static TruckerPanel instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    void levelup()
    {
        TruckData[DriverID].playlvl = 1;
        TruckData[DriverID].playwage = 1;
        TruckData[DriverID].playexp = TruckData[DriverID].totalxp / 2;

        while (TruckData[DriverID].playexp >= (150 * TruckData[DriverID].playlvl))
        {
            TruckData[DriverID].playwage = TruckData[DriverID].playwage + (TruckData[DriverID].playlvl /2);
            TruckData[DriverID].playexp -= (150 * TruckData[DriverID].playlvl);
            TruckData[DriverID].playlvl++;
        }
    }

    void UpdateHours()
    { 
        //Load currLoad = GM.inst.Trucks[DriverID].GetComponent<Movement>().currLoad;
        //playhrs = loadMove;

        hours.text = "" + TruckData[DriverID].playhrs;
    }

    void UpdateLoads()
    {
        Load currLoad =  GM.inst.Trucks[DriverID].GetComponent<Movement>().currLoad;

        /*
         //Might not be needed
         string txt = ld.originLabel + " to " + ld.destinationLabel;
                template.transform.SetParent(ActiveloadBoxContent.transform, false);
                template.GetComponentInChildren<Text>().text = txt;

                //replace this with source and see if works.
                Text SrcText = template.transform.Find("Pickup").gameObject.GetComponent<Text>();
                SrcText.text = ld.originLabel;
                //replace this with destination see if works.
                Text DestText = template.transform.Find("DropOff").gameObject.GetComponent<Text>();
                DestText.text = ld.destinationLabel;

                //Might not need this two
                Text timeText = template.transform.Find("Time").gameObject.GetComponent<Text>();
                timeText.text = ld.DueDate.ToShortDateString() + " " + ld.DueDate.ToShortTimeString();

                Text LoadText = template.transform.Find("LoadText").gameObject.GetComponent<Text>();
                LoadText.text = ld.value.ToString();

                GameObject LoadTypeIcon = template.transform.Find("LoadType").gameObject;

                Texture2D Loadedtexture = Resources.Load<Texture2D>("Textures pack/UI Icons/" + ld.productIcon);
                LoadTypeIcon.GetComponent<RawImage>().texture = Loadedtexture;
         */
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
        xp.text = "" + TruckData[DriverID].totalxp;
        lvl.text = "" + TruckData[DriverID].playlvl;
        wage.text = "" + TruckData[DriverID].playwage;
        TruckData[DriverID].totalxp = (int)truck.GetComponent<Movement>().LifeTimehaulDistance;
        levelup();
        UpdateLoads();
        UpdateHours();
    }
}
