using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIM : MonoBehaviour
{
    private static UIM instance = null;
    public static UIM inst { get { return instance; } }

    public enum UIState { UNDEFINED, SELECT, ADD, DELETE };

    public GameObject AnalyticsPanel;
    public GameObject ShopPanel;
    public GameObject SettingsPanel;
    public GameObject TrucksPanel;
    public GameObject JobsPanel;
    public GameObject TruckerPanel;
    public GameObject TruckPanel;
    public GameObject TrailerPanel;
    public GameObject ConfirmPanel;

    public GameObject[] Trucks;

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

    private void OnApplicationQuit()
    {
        instance = null;
    }



    public void LoadPanel(GameObject Panel)
    {
        ClosePanel();
        Panel.SetActive(true);
    }

    public void ClosePanel()
    {
        AnalyticsPanel.SetActive(false);
        ShopPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        TrucksPanel.SetActive(false);
        JobsPanel.SetActive(false);
        TruckerPanel.SetActive(false);
        TruckPanel.SetActive(false);
        TrailerPanel.SetActive(false);
        ConfirmPanel.SetActive(false);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
