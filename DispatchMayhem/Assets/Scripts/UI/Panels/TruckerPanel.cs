﻿using System;
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
    public GameObject Box;
    public GameObject Food;
    public GameObject Milk;
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

    public void UpdateHours(System.DateTime pTime)
    {
        Load hasLoad = GM.inst.Trucks[DriverID].GetComponent<Load>();
        //TruckData[DriverID].playhrs = hasLoad;
        if (hasLoad != null && hasLoad.state == Load.LoadState.DELIVERING)
        {
            System.DateTime playerHour = new System.DateTime(pTime.Hour);

            int pHours = TruckData[DriverID].playhrs;

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

            if (LoadIcon == null)
            {
                Debug.Log("Image not found");
                Debug.Log("LoadIcon: " + LoadIcon);
            }
            else if (LoadIcon != null)
            {
                Oil.SetActive(false);
                Box.SetActive(false);
                Food.SetActive(false);
                Milk.SetActive(false);
                Debug.Log("LoadIcon: " + LoadIcon);

                if (LoadIcon == Oil)
                {
                    Debug.Log("You are delivering Oil");
                    Oil.SetActive(true);
                    Debug.Log("LoadIcon: " + LoadIcon);

                }
                else if (LoadIcon == Box)
                {
                    Debug.Log("You are delivering Boxed Goods");
                    Box.SetActive(true);
                    Debug.Log("LoadIcon: " + LoadIcon);
                }
                else if (LoadIcon == Food)
                {
                    Debug.Log("You are delivering Food");
                    Food.SetActive(true);
                    Debug.Log("LoadIcon: " + LoadIcon);
                }
                else if (LoadIcon == Milk)
                {
                    Debug.Log("You are delivering Milk");
                    Milk.SetActive(true);
                    Debug.Log("LoadIcon: " + LoadIcon);
                }

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
        UpdateHours(pTime);
    }
}

