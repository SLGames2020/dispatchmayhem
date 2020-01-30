using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public List<Vector2> cityArray = new List<Vector2>();
   // Vector2 CurrentPosition;
    int DestinationMarker = 0;

    [HideInInspector]public GameObject load;
    public Button assButt;
    AudioSource button;
    bool button_play;

    //// Start is called before the first frame update
    void Start()
    {
        Debug.Log("Adding Truck");
        UIM.inst.AddToTruckList(this.gameObject);
        assButt.onClick.AddListener(delegate { loadTruck(); } );
        button = GetComponent<AudioSource>();
        button_play = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            button.Play();
            button_play = true;
        }
        else
        {
            button_play = false;
        }


        if (cityArray.Count != 0)
        {
            /*foreach (GameObject currCity in GM.inst.openCities)
            {
                Vector2 cityLoc = currCity.GetComponent<City>().transform.position;
                cityArray.Add(cityLoc);
            }*/


            Vector2 tempvec = new Vector2(this.transform.position.x, this.transform.position.y);
            //Vector2 newvec = new Vector2(100, 100);

            Vector2 tv2 = new Vector2(cityArray[DestinationMarker].x, cityArray[DestinationMarker].y);

            //Debug.Log("DestinationMarker: " + DestinationMarker);
            //tempvec = Move(tempvec, cityArray[DestinationMarker], 100.0f);

            tempvec = Move(tempvec, tv2, 100.0f);

            Vector3 newPos = new Vector3(cityArray[DestinationMarker].x, cityArray[DestinationMarker].y, this.transform.position.z);

            Debug.Log("cityArray: " + cityArray[0] + " " + cityArray[1] + " tempvec: " + tempvec);

            this.transform.position = new Vector3(tempvec.x, tempvec.y, this.transform.position.z);

            //Debug.Log("newPos: " + newPos);

            this.transform.LookAt(newPos);

            if (this.transform.rotation.x < 0)
            {
                this.transform.Rotate(new Vector3(0, 0, 1), 180);
            }


            if ((tempvec - cityArray[DestinationMarker]).magnitude < 1.0f)
            {
                DestinationMarker++;

                if (DestinationMarker >= cityArray.Count)
                {
                    DestinationMarker = 0;
                    cityArray.Clear();
                }
            }
        }
    }

    public Vector2 Move(Vector2 CurrentPosition, Vector2 Destination, float speed)
    {
        Vector2 NewDestination = Destination - CurrentPosition;
        Vector2 NewLoc = (NewDestination.normalized * speed)*Time.deltaTime + CurrentPosition;
        //Debug.Log("Destination: " + Destination);
        //Debug.Log("CurrentPosition: " + CurrentPosition);
        return NewLoc;
    }

    public void loadTruck()
    {
        load = UIM.inst.loadSelected;
        Debug.Log("Load assigned");

        DestinationMarker = 0;
        cityArray.Clear();
        //cityArray.Add(load.GetComponent<Load>().origin);
        //cityArray.Add(load.GetComponent<Load>().destination;

        string origString = load.GetComponent<Load>().originLabel;
        cityArray.Add(GameObject.Find(origString).transform.position);

        string destString = load.GetComponent<Load>().destinationLabel;
        cityArray.Add(GameObject.Find(destString).transform.position);

        Destroy(UIM.inst.loadSelectedListItem);
    }
}
