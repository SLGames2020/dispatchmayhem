using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    public Text nameLabel;                      //Hovering Text label from Holistic 3d https://www.youtube.com/watch?v=0bvDmqqMXcA

    public string label = "unknown city";
    public bool stillOpen = true;

    public MapSupport mapSupport;

    private void Awake()
    {
        mapSupport = this.gameObject.GetComponent<MapSupport>();
    }
    // Start is called before the first frame update
    void Start()
    {
        nameLabel.text = label;
        StartCoroutine(SpawnLoad());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 namepos = Camera.main.WorldToScreenPoint(this.transform.position);
        namepos.z = 1.0f;
        nameLabel.transform.position = namepos;
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
}
