using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM : MonoBehaviour
{
    /*******************************************************************************************
        CM (Company/Player Manager)

        This singleton is responsible for keeping track of player and company specific data
        such as money earned, trucks and trailers in play, company name, server account, etc...

    *******************************************************************************************/

    private static CM instance = null;
    public static CM inst { get { return instance; } }

    public List<GameObject> plrTrucks;
    public List<GameObject> plrTrailers;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        plrTrucks.Clear();
        plrTrailers.Clear();
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //
    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    private void OnApplicationQuit()
    {
        instance = null;
    }

    /******************************************************************************
        AddTrailerToCompanyList/RemoveTrailerFromCompanyList

        These methods are used to maintain a company list of all trailers
        the player has bought. 

    *******************************************************************************/
    public void AddTrailerToMCompanyList(GameObject tr)
    {
        if (tr != null)
        {
            //Debug.Log("load added to master");
            plrTrailers.Add(tr);
        }
        else
        {
            Debug.LogError("attempt to add Null trailer to company");
        }
    }

    public void RemoveTrailerFromCompanyList(GameObject tr)
    {
        if (tr != null)
        {
            plrTrailers.Remove(tr);
        }
        else
        {
            Debug.LogError("attempt to remove Null trailer from companyr");
        }
    }
    
    /*****************************************************************************
        AddTruckTo/RemoveTruckFromCompanyList

        This methods are for updating the company list of all the trucks
        currently available to the player. 

    ******************************************************************************/
    public void AddTruckToCompanyList(GameObject trk)
    {
        if (trk != null)
        {
            plrTrucks.Add(trk);
        }
        else
        {
            Debug.LogError("attempt to add Null truck to company");
        }
    }

    public void RemoveTruckFromCompanyList(GameObject trk)
    {
        if (trk != null)
        {
            plrTrucks.Remove(trk);
        }
        else
        {
            Debug.LogError("attempt to remove Null truck from company");
        }
    }
}
