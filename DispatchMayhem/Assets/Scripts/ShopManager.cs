using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject TruckSeller;
    public GameObject TrailerSeller;
    public GameObject FinanceCase;
    public GameObject HelpPhone;

    // Start is called before the first frame update
    /*void Start()
    {
        
    } */

    // Update is called once per frame
    void Update()
    {
        Rotate();
    } 

    void TruckShop()
    {

    }

    void TrailerShop()
    {

    }

    void FinanceWall()
    {

    }

    void HelpInfo()
    {

    }

    void Rotate()
    {
        TruckSeller.transform.Rotate(0, 20 * Time.deltaTime, 0); //rotates 50 degrees per second around z axis
        TrailerSeller.transform.Rotate(0, 20 * Time.deltaTime, 0); //rotates 50 degrees per second around z axis
    }

}
