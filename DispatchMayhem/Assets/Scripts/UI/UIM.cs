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
    public GameObject FoodPanel;
    public GameObject FlatPanel;
    public GameObject TruckPanel;
    public GameObject TrailerPanel;
    public GameObject ConfirmPanel;
    public GameObject GameOverPanel;
    //public GameObject LoadingPanel;
    public GameObject CreditsPanel;
    public GameObject TutorialPanel;
    public GameObject CoinIcon;
    public GameObject MessagePanel;
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
        GameOverPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        TutorialPanel.SetActive(false);
        MessagePanel.SetActive(false);
        FlatPanel.SetActive(false);
        FoodPanel.SetActive(false);
        //LoadingPanel.SetActive(false);

    }
    
    public void exitGame()
    {
        Application.Quit();
    }

    /***************************************************************************
    MessageBox
    This method pops up a panel with the specified message and details to
    the middle of the screen.

    to KISS the missage is always centered, and can only be closed
    (no button press callbacks supported at this time)

    type: Message type (Warning, Alert, etc...) appears in upper left panel title bar
    topic: Large Bold centered text as one or two word subject matter
    message: The primary message to present (full text)
    btntxt: The label to place on the close button (not upper right X button)

****************************************************************************/
    public void MessageBox(string type, string topic, string message, string btntxt)
    {

        MessageBoxCtrl mbc = MessagePanel.GetComponent<MessageBoxCtrl>();
        //Debug.Log(type + " " + topic + " " + message + " " + btntxt);
        MessagePanel.SetActive(true);
        mbc.title = type;
        mbc.topic = topic;
        mbc.message = message;
        mbc.buttonTxt = btntxt;
    }
}
