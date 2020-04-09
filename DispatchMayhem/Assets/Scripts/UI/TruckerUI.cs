using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckerUI : MonoBehaviour
{
    const int SHORT_INFO_IN = -25;
    const int SHORT_INFO_OUT = -285;

    const int NUM_INFO_ITEMS = 4;

    public GameObject[] InfoItems;

    public Text[] Texts;

    public int TruckerID;
    public string name;

    public GameObject TruckerPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GM.inst.Trucks[TruckerID] != null && GM.inst.Trucks[TruckerID].GetComponent<Movement>() != null)
        {
            Texts[3].text = "ETA: " + (int)(GM.inst.Trucks[TruckerID].GetComponent<Movement>().timeRemaining / 60) + "m " + (int)(GM.inst.Trucks[TruckerID].GetComponent<Movement>().timeRemaining % 60) + "s";
        }
    }


    public void AnimateIn()
    {
        TruckerPanel.GetComponent<TruckerPanel>().DriverID = TruckerID;
        UIM.inst.LoadPanel(TruckerPanel);
        if (GM.inst.Trucks[TruckerID].GetComponent<Load>() == null)
        {
            //popup load selection to deliver.

        }
        else
        {
            Debug.Log("UpdateAnimIn 0");
            StartCoroutine("UpdateAnimIn");
        }
    }

    IEnumerator UpdateAnimIn()
    {
        //Name:
        Texts[0].text = name;
        Texts[1].text = "Source:" + GM.inst.Trucks[TruckerID].GetComponent<Load>().originLabel;
        Texts[2].text = "Dest:" + GM.inst.Trucks[TruckerID].GetComponent<Load>().destinationLabel;

        // animate each info section for the trucker in
        for (int i = 0; i < NUM_INFO_ITEMS; i++)
        {
            while (InfoItems[i].transform.localPosition.x < SHORT_INFO_IN)
            {
                yield return new WaitForSeconds(0.025f);
                // increase X with an Ease animation
                InfoItems[i].transform.Translate(((SHORT_INFO_IN + 20) - InfoItems[i].transform.localPosition.x) / 25, 0, 0);
            }
        }
        yield return new WaitForSeconds(5f);

        // animate each info section for the trucker out
        for (int i = NUM_INFO_ITEMS - 1; i >= 0; i--)
        {
            while (InfoItems[i].transform.localPosition.x > SHORT_INFO_OUT)
            {
                yield return new WaitForSeconds(0.01f);
                InfoItems[i].transform.Translate(((SHORT_INFO_OUT - 20) + InfoItems[i].transform.localPosition.x) / 25, 0, 0);
            }
        }

        yield return null;

    }

    public void AnimateOut()
    {

    }
}
