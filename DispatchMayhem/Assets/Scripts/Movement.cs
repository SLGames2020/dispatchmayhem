using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2[] cityArray = new Vector2[4];
    Vector2 CurrentPosition;
    int DestinationMarker = 0;

    //// Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            cityArray[i] = new Vector2( Random.Range(0, 100), Random.Range(0, 100));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 tempvec = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 newvec = new Vector2(100, 100);

        tempvec = Move(tempvec, cityArray[DestinationMarker],10.0f);

        this.transform.position = new Vector3(tempvec.x, this.transform.position.y, tempvec.y);
        if((tempvec - cityArray[DestinationMarker]).magnitude < 1.0f )
        {
            DestinationMarker++;

            if(DestinationMarker > 3)
            {
                DestinationMarker = 0;
            }
        }
    }

    public Vector2 Move(Vector2 CurrentPosition, Vector2 Destination, float speed)
    {
        Vector2 NewDestination = Destination - CurrentPosition;
        Vector2 NewLoc = (NewDestination.normalized * speed)*Time.deltaTime + CurrentPosition;


        return NewLoc;
    }
}
