using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public GameObject loadPrefab;

    public string label = "unknown city";
    public Vector2 gps;

    //[HideInInspector]
    public List<GameObject> loads;

    private float lastTim;


    private void Awake()
    {
        loads.Clear();
    }
    // Start is called before the first frame update
    void Start()
    {
        GM.inst.AddCityToMasterList(this.gameObject);
        lastTim = Time.time + 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastTim)
        {
            lastTim = Time.time + 1.0f;

           if (Random.Range(0, GM.inst.LOADSPAWNTIME) == 0)
           {
                GameObject go;
                Vector2 des;
                do
                {
                    go = GM.inst.openCities[Random.Range(0, GM.inst.openCities.Count - 1)];
                    des = go.GetComponent<City>().gps;
                }
                while (des == gps);
                
                GameObject ldgo = Instantiate(loadPrefab, this.transform.position, Quaternion.identity);
                Load ld = ldgo.GetComponent<Load>();
                ld.origin = gps;
                ld.destination = des;
                ldgo.name = label + "load: " + loads.Count;
                ldgo.transform.parent = this.transform;
                loads.Add(ldgo);
                GM.inst.AddLoadToMasterList(ldgo);
                Debug.Log("Load Added for " + label);
           }
        }
    }
}
