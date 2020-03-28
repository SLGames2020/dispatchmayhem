using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

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
    public AudioClip buttonClick;

    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

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

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsPanel.SetActive(true);
            //Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        LowPass();
    }

    public void LowPass()
    {
        if(Time.timeScale == 0)
        {
            paused.TransitionTo(0.1f);
        }
        else
        {
            unpaused.TransitionTo(0.1f);
        }
    }

    public void LoadPanel(GameObject Panel)
    {
        ClosePanel();
        Panel.SetActive(true);
        //Pause();
    }

    public void ClosePanel()
    {
        AnalyticsPanel.SetActive(false);
        ShopPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        TrucksPanel.SetActive(false);
        JobsPanel.SetActive(false);
        TruckerPanel.SetActive(false);
        
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
