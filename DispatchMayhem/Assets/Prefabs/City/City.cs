using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    public GameObject loadPrefab;
    public GameObject listItemTemplate;
    public GameObject loadBoxContent;

    public string label = "unknown city";
    public bool stillOpen = true;

    //[HideInInspector]
    public List<GameObject> loads;
    public MapSupport mapSupport;

    //private float lastTime;

    private void Awake()
    {
        loads.Clear();
        mapSupport = this.gameObject.GetComponent<MapSupport>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //CityM.inst.AddCityToMasterList(this.gameObject);
        StartCoroutine(SpawnLoad());
        //lastTime = Time.time + 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //float newx = this.transform.position.x;
        //newx += (30.0f * Time.deltaTime);
        //this.transform.position = new Vector3(newx, this.transform.position.y, this.transform.position.z);
    }

    /**************************************************************************
        SpawnLoad

        This method creates a load, randomly creates it's parameters, addes
        it to the GM wrldLoads list

    ****************************************************************************/
    private IEnumerator SpawnLoad()
    {
        GameObject go;
        Vector2 des;

        while (stillOpen)
        {
            do
            {
                go = CyM.inst.openCities[Random.Range(0, CyM.inst.openCities.Count - 1)];
                des = go.GetComponent<MapSupport>().gps;
            }
            while (des == mapSupport.gps);                     //loop till we find a city that isn't us

            GameObject ldgo = Instantiate(loadPrefab, this.transform.position, Quaternion.identity);
            Load ld = ldgo.GetComponent<Load>();
            ld.originLabel = label;
            ld.origin = mapSupport.gps;
            ld.destinationLabel = go.GetComponent<City>().label;
            ld.destination = des;
            ldgo.name = label + " load: " + loads.Count;
            ldgo.transform.parent = this.transform;
            if (UIM.inst.AddToLoadList(ldgo))
            {
                loads.Add(ldgo);
            }
            else
            {
                Destroy(ldgo);
                Debug.Log("Failed to add load to list for " + label);
            }
            yield return new WaitForSeconds(Random.Range(1, CyM.inst.LOADSPAWNTIME));       //have the yield at the end so we get loads right away
        }
    }

    /*****************************************************************************
        AddLoadToList

        This method will take the newly spawned 
 
    /*****************************************************************************
        
    
    */
     


}
