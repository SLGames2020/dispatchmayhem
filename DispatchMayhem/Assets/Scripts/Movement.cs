using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    //public delegate void NM.inst.routeCallBack(List<Vector2> route, float distance);
    private List<Vector2> route;
    public routeCallBack FoundRoute;

    //public GameObject map;
    //public List<Vector2> cityArray = new List<Vector2>();
   // Vector2 CurrentPosition;
    int DestinationMarker = 0;

    [HideInInspector]public GameObject load;
    public Button assButt;
    //public Vector2 gps = new Vector2(-75.6973f, 45.4215f);
    AudioSource button;
    bool button_play;

    //private Vector2 s = new Vector2(-75.6973f, 45.4215f);
    //private Vector2 e = new Vector2(-74.7303f, 45.0213f);

    private Vector2 destination;
    private float lastTime;
    //private bool gettingRoute;

    private MapSupport mapSupport;

    //// Start is called before the first frame update
    void Start()
    {
        route = new List<Vector2>();
        route.Clear();

        FoundRoute = RouteCallBack;

        Debug.Log("Adding Truck");
        UIM.inst.AddToTruckList(this.gameObject);
        assButt.onClick.AddListener(delegate { loadTruck(); } );
        button = GetComponent<AudioSource>();
        button_play = false;


        mapSupport = this.gameObject.GetComponent<MapSupport>();
        destination = mapSupport.gps;

        lastTime = Time.time + 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            //button.Play();
            button_play = false;
        }
        else
        {
            button_play = false;
        }

        //Vector2 tempvec = new Vector2(this.transform.position.x, this.transform.position.y);


        if (DestinationMarker < route.Count)
        {
            Vector2 tv2 = new Vector2(route[DestinationMarker].x, route[DestinationMarker].y);

            //Debug.Log("DestinationMarker: " + DestinationMarker);
            mapSupport.gps = Move(mapSupport.gps, tv2, 0.05f*Time.deltaTime);
            Debug.Log("cityArray: " + route[0] + " " + route[1] + " tempvec: " + mapSupport.gps);

            //this.transform.position = new Vector3(mapSupport.gps.x, mapSupport.gps.y, this.transform.position.z);

            //Debug.Log("newPos: " + newPos);
            Vector3 newPos = new Vector3(route[DestinationMarker].x, route[DestinationMarker].y, this.transform.position.z);
            this.transform.LookAt(newPos);

            if ((this.transform.rotation.x < 0 && this.transform.rotation.y > 0) ||
                (this.transform.rotation.x > 0 && this.transform.rotation.y > 0))
            {

                this.transform.Rotate(new Vector3(0, 0, 1), 180);
            }

            if ((mapSupport.gps - route[DestinationMarker]).magnitude < 0.1f)
            {
                DestinationMarker++;
            }

            Debug.Log("gps: " + mapSupport.gps + " Dest: " + destination + " mag: " + (mapSupport.gps - destination).magnitude);
        }


        if (((mapSupport.gps - destination).magnitude < 0.1f) && (DestinationMarker > 0))
        {
            //route.Clear();
            //DestinationMarker = 0;
            Debug.Log("We have arrived!");
        }
        //else   ((route.Count == 0) && (!gettingRoute))
        //{
        //    if ((lastTime < Time.time) || (destination != Vector2.zero))
        //    {
        //        Debug.Log("Getting new route");
        //        lastTime = Time.time + 1.0f;
        //        NM.Inst.GetRoute(mapSupport.gps, destination, FoundRoute);
        //        gettingRoute = true;
        //    }
        //}
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
        //Debug.Log("Load assigned");

        DestinationMarker = 0;
        //cityArray.Clear();

        destination = load.GetComponent<Load>().destination;
        Debug.Log("Load Destination: " + destination);

        if ((lastTime < Time.time) || (destination != Vector2.zero))
        {
            Debug.Log("Getting new route");
            lastTime = Time.time + 1.0f;
            NM.Inst.GetRoute(mapSupport.gps, destination, FoundRoute);
            //gettingRoute = true;
        }

        Destroy(UIM.inst.loadSelectedListItem);
    }

    /*******************************
    test callback routine

    **********************************/
    public void RouteCallBack(List<Vector2> rte, float dst)
    {
        route.Clear();
        foreach (Vector2 pnt in rte)
        {
            //Debug.Log("Waypoint List: [" + pnt.x + "," + pnt.y + "]");
            route.Add(pnt);
        }
        DestinationMarker = 0;
        Debug.Log("Distance: " + dst + " Waypoints: " + route.Count);
        //gettingRoute = false;
    }
}


