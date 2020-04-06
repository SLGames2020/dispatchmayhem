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
    public GameObject truck;

    

    void levelup()
    {
        truck.GetComponent<Truck>();

        while( >= 100)
        {
            //wage.text += 1;
            //lvl.text += 1;
        }
    }

    void UpdateHours()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        xp.text = "1";
        lvl.text = "1";
        wage.text = "1";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            xp.text += 1;
        }
    }
}
