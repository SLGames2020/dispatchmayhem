using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public routeCallBack FoundRoute;

    //[HideInInspector] public GameObject load;
    //public Button assButt;

    [HideInInspector] public Load currLoad;

    public float haulDistance = 0.0f;
    public float LifeTimehaulDistance = 0.0f;
    public float haulCost = 0.0f;
    public float truckSpeed = 65.244f;               //legislated 105kph in miles

    public AudioClip idle;
    public AudioClip moving;
    public AudioClip loading;
    public AudioClip unloading;
    public AudioClip warning;

    public bool onDuty = false;
    public bool hasLoad = false;

    private MapSupport mapSupport;
    private List<Vector2> route;
    private Vector3[] lastPosition;
    private Vector2 origin;
    private Vector2 destination;


    public DateTime loadDelayTime;                                //the time we will be finished loading/unloading (should come from game manager)
    private float lastTime;
    private float haulingCost;
    private DateTime hazardWaitTime;
    private Vector2 lastgps;

    private int destinationMarker = 0;
    private int loadMark = -1;                                   //the point in the route list to delay for loading

    private const float closeEnough = 0.05f;

    [HideInInspector] public float timeRemaining;

    //// Start is called before the first frame update
    void Start()
    {
        route = new List<Vector2>();
        route.Clear();

        FoundRoute = RouteCallBack;

        mapSupport = this.gameObject.GetComponent<MapSupport>();
        origin = mapSupport.gps;
        lastgps = mapSupport.gps;

        loadDelayTime = GameTime.inst.gmTime;                   //just some safety initialization
        hazardWaitTime = GameTime.inst.gmTime;
        lastTime = Time.time + 1.0f;                            //temporary 1s blocking of mapbox calls so we don't chew up our free alotment
        lastPosition = new Vector3[] { this.transform.position, this.transform.position, this.transform.position, this.transform.position, this.transform.position,
                                       this.transform.position, this.transform.position, this.transform.position, this.transform.position, this.transform.position};
    }

    // Update is called once per frame
    void Update()
    {

        //myTime = Time.time;

        if (destinationMarker < route.Count)                                    //On Route
        {
            currLoad.state = Load.LoadState.DELIVERING;

            for (int x = 0; x < lastPosition.Length - 1; x++)                     //save an array of last positions to smooth out the movement
            {                                                                   //slerp/lerp does not work very well with the mapmanager controlling an objects transform
                lastPosition[x + 1] = lastPosition[x];
            }
            lastPosition[0] = this.transform.position;

            if (GameTime.inst.gmTime < loadDelayTime)                          //if we are loading, don't move
            {
                //Need a loading graphic/state here
            }
            else if (destinationMarker == loadMark)                             //if we're at the loading point
            {
                loadMark = -1;                                                  //flush out the load point until we get a new point

                Truck trk = this.gameObject.GetComponent<Truck>();              //check to see if the player sent the right truck
                if (trk.productType != currLoad.productType)                    //if not, notify of an error 
                {                                                               //TODO: add error graphic and sounds
                    SoundManager.instance.SoundEffect(warning);
                    string mess = "Cannot transport " + currLoad.productLabel + " load \n";
                    mess += "with " + trk.rigLabel + "\n\n";
                    mess += "(driver refused at gate, \nno delivery available)";
                    UIM.inst.MessageBox("Wrong Truck", "Warning!", mess, "Close");

                    Destroy(currLoad);
                    currLoad = null;
                    hasLoad = false;
                    onDuty = false;
                    destinationMarker = route.Count;
                }
                else
                {
                    loadDelayTime = GameTime.inst.gmTime.AddHours(1.0f);            //wait an hour for unloading (this needs to reference a proper Time Manager Delay reference)
                    if ((mapSupport.gps - destination).magnitude > closeEnough)     //if we're not at the destination
                    {
                        NM.Inst.GetRoute(mapSupport.gps, destination, FoundRoute);  //reroute to the destination
                        SoundManager.instance.SoundEffect(loading);
                        hasLoad = true;
                    }
                }
            }
            else if (GameTime.inst.gmTime < hazardWaitTime)                     //the highway wait timing is seperate here so we can have
            {                                                                   //different hooks for the hazards and the loading/unloading delay times
                //Debug.Log("Hazard Waiting");                                  //add a sound here
            }
            else if (loadMark != -1)                                            //only move if we've received a loading point
            {
                Vector2 tv2 = route[destinationMarker];
                lastgps = mapSupport.gps;
                mapSupport.gps = Move(mapSupport.gps, tv2, truckSpeed);

                Vector3 newlook = this.transform.position - lastPosition[lastPosition.Length - 1]; // lastpossum; // lastPosition[1];

                if (newlook.magnitude != 0)                                     //filter out needless warnings from LookRotation
                {
                    Quaternion newrot = Quaternion.LookRotation(newlook);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newrot, GameTime.inst.timeScale * Time.deltaTime);
                }                

                if ((mapSupport.gps - route[destinationMarker]).magnitude < closeEnough)
                {
                    destinationMarker++;
                }
            }
            else
            {
                if ((mapSupport.gps - destination).magnitude < closeEnough)   //if we're close to the destination, and we have travelled a route
                {
                    SoundManager.instance.SoundEffect(unloading);
                    if (currLoad.state != Load.LoadState.DELIVERED)
                    {
                        LifeTimehaulDistance += haulDistance;
                        //Debug.Log("LifeTimehaulDistance: " + LifeTimehaulDistance);
                    }

                    currLoad.state = Load.LoadState.DELIVERED;
                                                             
                    if (currLoad.DueDate < GameTime.inst.gmTime)                    //late deliveries suffer a 20% penalty
                    {                                                               //and the company reputation is lowered
                        int penalty = (int)(currLoad.value * 0.20f);
                        string mess = "The " + currLoad.productLabel + " load from " + currLoad.originLabel + " to " + currLoad.destinationLabel;
                        mess += " was late. Reputation is reduced with a payment penalty of $" + penalty;
                        
                        UIM.inst.MessageBox("Late Delivery", "Warning!", mess, "Close");
                        GM.inst.repStatus.Rep(false);
                        currLoad.value *= 0.80f;
                    }
                    else                                                         
                    {
                        GM.inst.repStatus.Rep(true);
                    }

                    if (CoinM.inst.SetCoinValue(currLoad.driverID, currLoad.value)) //Display a coin for the player to click to get paid
                    {
                        CoinM.inst.EnableCoin(currLoad.driverID);
                    }                    
                    else
                    {
                        Debug.Log("Could not find driver: " + currLoad.driverID + " using direct deposit");
                        Finances.inst.AddMoney(currLoad.value);
                    }
                    Destroy(currLoad);
                    currLoad = null;
                    hasLoad = false;
                    onDuty = false;
                }
                destinationMarker = route.Count;                       //when all is done, stop everything
            }

            float tmpdis = CalcDistance(lastgps, mapSupport.gps);       //having this out here means all inspections and loading times also cost money
            float tmpcost = tmpdis * haulingCost;
            Finances.inst.AddMoney(-tmpcost);
            haulDistance += tmpdis;                                         //for now tracking costs for analytics(?)
            haulCost += (tmpcost);
        }
    }
    /****************************************************************
        Move

        This method is responsible to advancing object forward given
        it's current position, where it's going, and a speed value

        Here the speed is converted from MPH in game time to 
        degs/sec in real time so that it works properly with Mapbox
        and the GPS system

    ******************************************************************/
    public Vector2 Move(Vector2 CurrentPosition, Vector2 Destination, float speed)
    {
        Vector2 todest = Destination - CurrentPosition;
        Vector2 degspeed = CalcAngleSpeed(speed, todest, CurrentPosition.y);        //calculates the deg/sec as a (lat,long) vector

        degspeed.x = GameTime.inst.gmHoursToRealSeconds(degspeed.x);
        degspeed.y = GameTime.inst.gmHoursToRealSeconds(degspeed.y);

        Vector2 NewLoc = CurrentPosition + degspeed * Time.deltaTime;
        timeRemaining = (todest / degspeed).magnitude;

        return NewLoc;
    }

    /***********************************************************************
    CalcAngleSpeed

    This method takes in a speed in mph, a direction cartisean), and an
    earth based latitude and will returns a Vec2 of degrees/second (lat/long)

    note there is a small error here as our direction is based on the surface
    of the earth/sphere, not a flat plane, and will not create a true 
    cartisean direction, but we're well within the accuracy for this project

    **************************************************************************/
    public Vector2 CalcAngleSpeed(float spd, Vector2 dir, float lat)
    {
        float empdate = 69.171f;                                //Earth Miles Per Degree At The Equator
        float dr = Mathf.Atan2(dir.y, dir.x);                   //(this is the same value for distances between lattitudes)
        float empdalat = empdate / Mathf.Cos(DegsToRads(lat));  //Earth Miles Per Degree At LATitude
                                                                //(distance between longitudes needs to be compensated for the angle from the equator)
        float xdph = Mathf.Cos(dr) * spd / empdalat;            //convert mph to Degrees Per Hour
        float ydph = Mathf.Sin(dr) * spd / empdate;

        return (new Vector2(xdph, ydph));
    }

    /*******************************************************************
        SetWaitTime

        This function is used by the hazards to set a number of hours
        for the truck to wait. 

        Basically it adds the number of hours to wait to the current
        game time and will stop the truck until the game time is
        greater than that time.

    ********************************************************************/
    public void SetWaitTime(float wt)
    {
        hazardWaitTime = GameTime.inst.gmTime;
        hazardWaitTime = hazardWaitTime.AddHours(wt);
    }
    /****************************************************************
        loadTruck
        
        This method is called for the assign button's onclick event.
        It is responsible for getting the start and end route data 
        and calling the Network Manager for getting the route.

        Note that if we are not at the start position, we'll get a 
        route from our current position to the start position.

    *****************************************************************/
    public void loadTruck(Load newLoad)
    {
        if (GameTime.inst.gmTime < loadDelayTime)   //if we are currently being loaded/unloaded
        {
            Debug.Log("We are still Loading");      //icon and/or error sound is needed here
            SoundManager.instance.SoundEffect(idle);
        }
        else
        {
            currLoad = newLoad;
            route.Clear();
            destinationMarker = 0;
            loadMark = -1;                              //flag that we don't have a route (loading point) yet
            haulCost = 0.0f;
            haulDistance = 0.0f;
            onDuty = true;
            SoundManager.instance.SoundEffect(moving);

            haulingCost = currLoad.haulingCost;
            origin = currLoad.origin;
            destination = currLoad.destination;
            string name = currLoad.destinationLabel;

            if ((lastTime < Time.time) || (destination != Vector2.zero))
            {
                if ((mapSupport.gps - origin).magnitude > closeEnough)      //if we are not close to the loads origin
                {
                    NM.Inst.GetRoute(mapSupport.gps, origin, FoundRoute);
                    loadDelayTime = GameTime.inst.gmTime;                   //no delaying to go pick up the load
                }
                else                                                        //if we're already at the right spot
                {
                    NM.Inst.GetRoute(mapSupport.gps, destination, FoundRoute);
                    loadDelayTime = GameTime.inst.gmTime.AddHours(1.0f);    //wait an hour for unloading (this needs to reference a proper Time Manager Delay reference)
                }
                lastTime = Time.time + 1.0f;                                //block us from calling mapbox more than once per second
            }
        }

    }

    /**************************************************************
        RouteCallBack

        This is the call back for when the route has been recieved
        by the Network Manager (through the mapbox api)

    ***************************************************************/
    public void RouteCallBack(List<Vector2> rte, float dst)
    {
        foreach (Vector2 pnt in rte)
        {
            route.Add(pnt);
        }
        loadMark = route.Count - 1;                                     //set the loading point (delay) to the last entry
    }

    /**********************************************************************
        CalcDistance

        The function uses the Haversine forumula to accurately calculate
        the "rhumb" line (shortest distance between two points on a sphere).

        It takes in the origin and destination lat and long, converst them
        to radians, and runs the calc found here:
        https://www.movable-type.co.uk/scripts/latlong.html

        note the return value is in miles (because thats the way trucking works)

    ************************************************************************/
    public float CalcDistance(Vector2 orig, Vector2 dest)
    {
        float phi1 = DegsToRads(orig.y);
        float phi2 = DegsToRads(dest.y);
        float deltaphi = DegsToRads(dest.y - orig.y);
        float deltlambda = DegsToRads(dest.x - orig.x);

        const float r = 3958.755866f;                                       //radius of the earth

        float a = (Mathf.Sin(deltaphi / 2.0f) * Mathf.Sin(deltaphi / 2.0f))
                + (Mathf.Cos(phi1) * Mathf.Cos(phi2)
                * (Mathf.Sin(deltlambda / 2.0f) * Mathf.Sin(deltlambda / 2.0f)));

        float c = 2.0f * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1.0f - a));

        return (r * c);
    }

    /***********************************************************************
        DegsToRads

        This is a quick function to convert degrees to radians, given
        the number of degrees

        ie  deg * pi / 180

    *************************************************************************/
    private float DegsToRads(float degs)
    {
        return (degs * Mathf.PI / 180.0f);
    }
}


