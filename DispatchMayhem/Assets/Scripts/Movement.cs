using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public routeCallBack FoundRoute;

    [HideInInspector]public GameObject load;
    public Button assButt;
    public AudioSource button;

    public bool button_play;

    private MapSupport mapSupport;
    private List<Vector2> route;
    private Vector3 lastPosition;
    private Vector2 origin;
    private Vector2 destination;


    public float loadDelayTime;                                //the time we will be finished loading/unloading 
    public float myTime;
    private float lastTime;
    private int destinationMarker = 0;
    private int loadMark = -1;                                   //the point in the route list to delay for loading
    private bool travellingToOrigin = true;

    //// Start is called before the first frame update
    void Start()
    {
        route = new List<Vector2>();
        route.Clear();

        FoundRoute = RouteCallBack;

        //Debug.Log("Adding Truck");
        UIM.inst.AddToTruckList(this.gameObject);
        assButt.onClick.AddListener(delegate { loadTruck(); } );
        button = GetComponent<AudioSource>();
        button_play = false;


        mapSupport = this.gameObject.GetComponent<MapSupport>();
        origin = mapSupport.gps;

        loadDelayTime = Time.time;
        lastTime = Time.time + 1.0f;                            //temporary 1s blocking of mapbox calls so we don't chew up our free alotment

        lastPosition = this.transform.position;
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

        myTime = Time.time;

        if (destinationMarker < route.Count)                                    //On Route
        {
            if (Time.time < loadDelayTime )                                      //if we are loading, don't move
            {
                //Need a loading graphic/state here
                Debug.Log("loading/unloading");
            }
            else if (destinationMarker == loadMark)                             //if we're at the loading point
            {
                Debug.Log("Reached Marker");
                loadMark = -1;                                                  //flush out the load point until we get a new point
                loadDelayTime = Time.time + 6.0f;                               //wait an hour for unloading (this needs to reference a proper Time Manager Delay reference)
                if ((mapSupport.gps - destination).magnitude > 0.1f)            //if we're not at the destination
                {
                    Debug.Log("Getting route to Destination");                  
                    NM.Inst.GetRoute(mapSupport.gps, destination, FoundRoute);  //reroute to the destination
                }
            }
            else if (loadMark != -1)                        //only move if we've received a loading point
            {
                Vector2 tv2 = new Vector2(route[destinationMarker].x, route[destinationMarker].y);

                mapSupport.gps = Move(mapSupport.gps, tv2, 20.0f * Time.deltaTime);
                //Debug.Log("cityArray: " + route[0] + " " + route[1] + " tempvec: " + mapSupport.gps);

                Vector3 newlook = this.transform.position - lastPosition;
                Quaternion newrot = Quaternion.FromToRotation(this.transform.forward, newlook);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,newrot, 1.0f * Time.deltaTime);
                lastPosition = this.transform.position;

                if ((this.transform.rotation.x < 0 && this.transform.rotation.y > 0) ||
                    (this.transform.rotation.x > 0 && this.transform.rotation.y > 0))
                {

                    this.transform.Rotate(new Vector3(0, 0, 1), 180);
                }

                if ((mapSupport.gps - route[destinationMarker]).magnitude < 0.1f)
                {
                    destinationMarker++;
                    //Debug.Log("Route change: " + destinationMarker);
                }
                //Debug.Log("gps: " + mapSupport.gps + " Dest: " + destination + " mag: " + (mapSupport.gps - destination).magnitude);
            }
            else
            {
                if ((mapSupport.gps - destination).magnitude < 0.1f)   //if we're close to the destination, and we have travelled a route
                {
                    Debug.Log("Load has been delivered!");
                }
                destinationMarker = route.Count;                       //when all is done, stop everything
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
    /****************************************************************
        loadTruck
        
        This method is called for the assign button's onclick event.
        It is responsible for getting the start and end route data 
        and calling the Network Manager for getting the route.

        Note that if we are not at the start position, we'll get a 
        route from our current position to the start position.

    *****************************************************************/
    public void loadTruck()
    {
        if (Time.time < loadDelayTime)          //if we are currently being loaded/unloaded
        {
            Debug.Log("We are still Loading");  //icon and/or error sound is needed here
        }
        else
        {
            load = UIM.inst.loadSelected;
            route.Clear();
            travellingToOrigin = false;
            destinationMarker = 0;
            loadMark = -1;                           //flag that we don't have a route (loading point) yet
            loadDelayTime = Time.time;                  //default to moving right away (to the load origin)

            Load ld = load.GetComponent<Load>();
            origin = ld.origin;
            destination = ld.destination;
            string name = ld.destinationLabel;
            Debug.Log("Load Destination: " + name);

            if ((lastTime < Time.time) || (destination != Vector2.zero))
            {
                if ((mapSupport.gps - origin).magnitude > 0.1f)             //if we are not close to the loads origin
                {
                    travellingToOrigin = true;
                    Debug.Log("Getting route to origin");
                    NM.Inst.GetRoute(mapSupport.gps, origin, FoundRoute);
                    loadDelayTime = Time.time;                              //no delaying to go pick up the load
                    lastTime = Time.time + 1.0f;                            //block us from calling mapbox more than once per second
                }
                else
                {
                    Debug.Log("Waiting To Load");
                    loadDelayTime = Time.time + 6.0f;                      //1 minute hard coded for now, but this should reference a Time Manager "1 Hour" time value
                }
            }
            Destroy(UIM.inst.loadSelectedListItem);                           //remove the load from the selection list
        }
    }

    /*******************************
    test callback routine

    **********************************/
    public void RouteCallBack(List<Vector2> rte, float dst)
    {
        foreach (Vector2 pnt in rte)
        {
            //Debug.Log("Waypoint List: [" + pnt.x + "," + pnt.y + "]");
            route.Add(pnt);
        }
        loadMark = route.Count - 1;                                     //set the loading point (delay) to the last entry
        Debug.Log("Distance: " + (int)(dst/1000.0f) + " Waypoints: " + route.Count);
    }
}


