using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public string label = "unkdfnown city";
    public Vector2 gps;

    [HideInInspector] public List<Load> loads;

    private float lastTim;

    // Start is called before the first frame update
    void Start()
    {
        //loads.Clear();

        lastTim = Time.time + 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastTim)
        {
            lastTim = Time.time + 1.0f;

           if (Random.Range(0, 60) == 0) // GM.inst.LOADSPAWNTIME) == 0)
           {
                Vector2 Des;
                do
                {
                    Des = GM.inst.openCities[Random.Range(0, GM.inst.openCities.Count)].gps;
                }
                while (Des == gps);

                Load ld = new Load(gps, Des);
                loads.Add(ld);
           }
        }
    }
}
