/* The coin manager is used as a quick
 * reference to display and hide the 
 * coins when loads are delivered. Coin
 * indices here must line up with the
 * Truck/drivers indices of the
 * LeftButtons                              */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinM : MonoBehaviour
{
    private static CoinM instance = null;
    public static CoinM inst { get { return instance; } }

    public GameObject[] coins;

    //private RectTransform[] origins;

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

        //int totcoins = coins.Length;
        //origins = new RectTransform[totcoins];

        //for (int x = 0; x < totcoins; x++)                      //Record the coin starting positions as we'll
        //{                                                       //use this to resotre them after the "animation"
        //    origins[x] = coins[x].GetComponent<RectTransform>();
        //}

    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}

    private void OnApplicationQuit()
    {
        instance = null;
    }

    /*****************************************************************************
        SetCoinValue

        This method takes a coin index and a value and set the appropriate coin
        value to this amount. It returns true or false depending on if the index
        is correct

        Typically called in the movement script when a load is delivered. If we
        return false, the value should just be depositied straight into the 
        players bank

    *******************************************************************************/
    public bool SetCoinValue(int cidx, float val)
    {
        bool retval = false;

        if ((cidx > -1) && (cidx < coins.Length))
        {
            coins[cidx].GetComponent<CoinUp>().value = val;
            retval = true;
            Debug.Log("Coin " + cidx + " value set to " + val);
        }

        return retval;
    }

    /*****************************************************************************
        EnableCoin

        This method will allow the indecated coin to be displayed.

        Once visible the coin will wait to be clicked on to be deposited in the 
        players bank accont

    ******************************************************************************/
    public void EnableCoin(int cidx)
    {
        coins[cidx].SetActive(true);
        Debug.Log("Coin " + cidx + " activated at: " + coins[cidx].transform.position);
    } 

}
