using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIM : MonoBehaviour
{
    private static UIM instance = null;
    public static UIM inst { get { return instance; } }

    public enum UIState { UNDEFINED, SELECT, ADD, DELETE};

    public List<Load> loads;
    public List<Truck> trucks;

    public UIState state = UIState.UNDEFINED;
    public int loadIdx = -1;
    public int truckIdx = -1;

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
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //
    //}

    // Update is called once per frame
    //void Update()
    //{
    //
    //}

    private void OnApplicationQuit()
    {
        instance = null;
    }
}
