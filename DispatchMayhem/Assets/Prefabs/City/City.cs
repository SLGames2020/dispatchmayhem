using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
 //   public GameObject loadPrefab;
 //   public GameObject listItemTemplate;
 //   public GameObject loadBoxContent;

    public string label = "unknown city";
    public bool stillOpen = true;

    //[HideInInspector]
 //   public List<GameObject> loads;
    public MapSupport mapSupport;

    //private float lastTime;

    private void Awake()
    {
        //loads.Clear();
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
            LoadM.inst.CreateNewLoad(label, mapSupport.gps);
            yield return new WaitForSeconds(Random.Range(1, CyM.inst.LOADSPAWNTIME));       //have the yield at the end so we get loads right away
        }
    }


    /*****************************************************************************
        AddLoadToList

        This method will take the newly spawned 
 
    /*****************************************************************************
        
    
    */



}
