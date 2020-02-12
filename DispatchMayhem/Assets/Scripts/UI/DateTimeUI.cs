using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class DateTimeUI : MonoBehaviour
{
    public GameObject Dt;
    Text txt;
    public int StartYear;
    public int StartMonth;
    public int StartDay;
    public int StartHour;
    public int StartMinute;
    public int StartSecond;


    private float gameTime;
    private const float MinToSec = 60;
    private const float HourToSec = 60 * 60;
    private const float DayToSec = 60 * 60 * 24;
    private const float MonthToSec = 60 * 60 * 24 * 30;
    private const float YearToSec = 60 * 60 * 24 * 30 * 12;

    void Start()
    {
        txt = Dt.GetComponent<Text>();
        gameTime += StartSecond;
        gameTime += StartMinute * MinToSec;
        gameTime += StartHour * HourToSec;
        gameTime += StartDay * DayToSec;
        gameTime += StartMonth * MonthToSec;
        gameTime += StartYear * YearToSec;

    }

    public void AddYear(int years)
    {
        AddTime((float)years * YearToSec);
    }

    public void AddMonth(int months)
    {
        AddTime((float)months * MonthToSec);
    }

    public void AddDay(int Days)
    {
        AddTime((float)Days * DayToSec);
    }
    public void AddTime(float sec)
    {
        gameTime += sec;
    }
    public string GetTime()
    {
        StringBuilder sb = new StringBuilder();
        float currentTime = gameTime;

        int currentYear = (int)(gameTime / YearToSec);
        currentTime -= currentYear;

        int currentMonth = (int)(gameTime / MonthToSec);
        currentTime -= currentMonth;

        int currentDay = (int)(gameTime / DayToSec);
        currentTime -= currentMonth;

        int currentHour = (int)(gameTime / HourToSec);
        currentTime -= currentHour;

        int currentMinute = (int)(gameTime / MinToSec);
        currentTime -= currentMinute;

        int currentSecond = (int)gameTime;

        sb.Append(currentYear);
        sb.Append("//");
        sb.Append(currentMonth);
        sb.Append("//");
        sb.Append(currentDay);

        return sb.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = GetTime();
    }
}