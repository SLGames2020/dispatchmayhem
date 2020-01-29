using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    List<Vector2> cityArray = new List<Vector2>();
   // Vector2 CurrentPosition;
    int DestinationMarker = 0;

    //// Start is called before the first frame update
    void Start()
    {
        Debug.Log("Adding Truck");
        UIM.inst.AddToTruckList(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if (cityArray.Count == 0)
        {
            foreach (GameObject currCity in GM.inst.openCities)
            {
                Vector2 cityLoc = currCity.GetComponent<City>().transform.position;
                cityArray.Add(cityLoc);
            }
        }

        Vector2 tempvec = new Vector2(this.transform.position.x, this.transform.position.y);
        //Vector2 newvec = new Vector2(100, 100);

        Debug.Log("DestinationMarker: " + DestinationMarker);
        tempvec = Move(tempvec, cityArray[DestinationMarker], 100.0f);

        Vector3 newPos = new Vector3(cityArray[DestinationMarker].x, cityArray[DestinationMarker].y, this.transform.position.z );

        this.transform.position = new Vector3(tempvec.x, tempvec.y, this.transform.position.z);

        Debug.Log("newPos: " + newPos);

        this.transform.LookAt( newPos);

        if (this.transform.rotation.x < 0)
        {
            this.transform.Rotate(new Vector3(0, 0, 1), 180);
        }


        if((tempvec - cityArray[DestinationMarker]).magnitude < 1.0f )
        {
            DestinationMarker++;

            if(DestinationMarker >= cityArray.Count)
            {
                DestinationMarker = 0;
            }
        }
    }

    public Vector2 Move(Vector2 CurrentPosition, Vector2 Destination, float speed)
    {
        Vector2 NewDestination = Destination - CurrentPosition;
        Vector2 NewLoc = (NewDestination.normalized * speed)*Time.deltaTime + CurrentPosition;
        Debug.Log("Destination: " + Destination);
        Debug.Log("CurrentPosition: " + CurrentPosition);
        return NewLoc;
    }
}
