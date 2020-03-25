using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavM : MonoBehaviour
{
    private static SavM instance = null;
    public static SavM inst { get { return instance; } }

    public GameObject[] trucks;

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

        if (PlayerPrefs.HasKey("money"))
        {
            Finances.inst.currCurrency = PlayerPrefs.GetFloat("money");
        }
        else
        {
            PlayerPrefs.SetFloat("money", 1500.0f);
            Finances.inst.currCurrency = 1500.0f;
        }

        //foreach (GameObject truck in trucks)
        //{
        //    Load load = truck.GetComponent < "Load" > ();
        //    MapSupport mapsup;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("money", Finances.inst.currCurrency);
        PlayerPrefs.Save();
        instance = null;
    }
}
