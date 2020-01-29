using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignTruck : MonoBehaviour
{
    /******************************************************
        AssignLoadToTruck

        This method is used by the UI menus to take 
        the selected load and assign it to the selected
        truck.

        The selected truck will them be moved from the 
        idle to the active list and begin the delivery
        sequence for the load

    *******************************************************/
    public void AssignLoadToTruck()
    {
    /*    if (UIM.inst.state != UIM.UIState.SELECT)
        {
            Debug.Log("Load and/or Truck not selected");
            //TODO: Error message or sound as the load or truck has not been selected
            //maybe prevent this by disabling the button until both are selected first
        }
        else
        {
            if (UIM.inst.trucks[UIM.inst.truckIdx].status != Truck.TruckStates.IDLE)
            {
                Debug.Log("Selected Truck is unavailable");
                //TODO: error message that they have selected the wrong truck
            }
            else
            {

            }
        }*/
    }
}
