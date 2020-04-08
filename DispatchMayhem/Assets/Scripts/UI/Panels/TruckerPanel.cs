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
    public int playexp = 0;
    public int totalxp = 0;
    public int playlvl = 1;
    public int playwage = 1;

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
        
        while(playexp >= 100)
        {
             playlvl++;
             playwage++;
             playexp = 0;
        }
    }

    void UpdateHours()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        xp.text = "" + playexp;
        lvl.text = "" + playlvl;
        wage.text = "" + playwage;
    }

    // Update is called once per frame
    void Update()
    {
        xp.text = "" + totalxp;
        lvl.text = "" + playlvl;
        wage.text = "" + playwage;
        totalxp = (int)truck.GetComponent<Movement>().LifeTimehaulDistance;
        levelup();
    }
}
