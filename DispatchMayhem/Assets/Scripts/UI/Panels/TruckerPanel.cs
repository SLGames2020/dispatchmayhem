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
    public Text load;
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

        TruckData = new TruckerInfo[3];
        DontDestroyOnLoad(gameObject);
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

    public void UpdateHours()
    {
        Load hasLoad = GM.inst.Trucks[DriverID].GetComponent<Load>();
        //TruckData[DriverID].playhrs = hasLoad;
        if (hasLoad != null && hasLoad.state == Load.LoadState.DELIVERING )
        {
            hours.text = "" + TruckData[DriverID].playhrs;
        }

    }

    public void UpdateLoads()
    {
        Load currLoad = GM.inst.Trucks[DriverID].GetComponent<Movement>().currLoad;
        if (currLoad != null)
        {
            source.text = currLoad.originLabel;

            destination.text = currLoad.destinationLabel;

            //load.text = currLoad.value.ToString();

            GameObject LoadTypeIcon = gameObject.transform.Find("LoadType").gameObject;

            Texture2D Loadedtexture = Resources.Load<Texture2D>("Textures pack/UI Icons/" + currLoad.productIcon);
            LoadTypeIcon.GetComponent<RawImage>().texture = Loadedtexture;
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
        UpdateHours();
    }
}

