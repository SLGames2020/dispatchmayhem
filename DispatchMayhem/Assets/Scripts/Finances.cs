2[using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finances : MonoBehaviour
{
    private static Finances instance = null;
    public static Finances inst { get { return instance; } }

    public Text cashText;

    public float currCurrency;
    //int spentCurrency;
    //int TotalCurrencyMade = currCurency + spentCurreny;

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

        if (PlayerPrefs.HasKey("money"))
        {
            currCurrency = PlayerPrefs.GetFloat("money");
        }
        else
        {
            PlayerPrefs.SetFloat("money", 1500.0f);
            currCurrency = 1500.0f;
        }

    }
        // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
        void Update()
        {
        int tmpcur = (int)currCurrency;
        cashText.text = tmpcur.ToString();
        }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("money", currCurrency);
        instance = null;
    }

    /***********************************************************************
        AddMoney

        This method adds (positive or negative) money to the player's total
        with a check to prevent values less than 0

        Note: A possible place to check for game Over?

    **************************************************************************/        
    public void AddMoney(float mny)
    {
        currCurrency += mny;

        if (currCurrency <= 0.0f)
        {
            Debug.Log("Player out of Money!");
        }
    }
}
