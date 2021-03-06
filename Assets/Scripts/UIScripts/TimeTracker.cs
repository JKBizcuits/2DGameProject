﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeTracker : MonoBehaviour
{
    private float totalSeconds; //represents how many seconds has passed since the game has started
    private float minutes; //represents the total minutes in game time
    private string minutesDisplay;
    private float hours; //represents the total hours in game time
    public string hoursDisplay;
    public float days; //represents the total days in game time.
    public TextMeshProUGUI displayText;
    public bool curfew;
    public float addedTime;

    // Start is called before the first frame update
    void Start()
    {
        displayText = GetComponent<TextMeshProUGUI>();

        totalSeconds = 0;

        curfew = false;

        addedTime = 360;

    }//end start

    // Update is called once per frame
    void Update()
    {
        totalSeconds = Time.fixedTime + addedTime;

        //We want each second to be one minute in this game.
        minutes = totalSeconds;
        hours = minutes / 60;
        days = hours / 24;

        if((minutes % 60) < 10)
        {
            minutesDisplay = $"0{(int)minutes % 60}";
        }
        else
        {
            minutesDisplay = $"{(int)minutes % 60}";
        }

        if((hours % 24) < 10)
        {
            hoursDisplay = $"0{(int)hours % 24}";
        }
        else
        {
            hoursDisplay = $"{(int)hours % 24}";
        }

        displayText.text = $"Day {(int) days} Time {hoursDisplay}:{minutesDisplay}";

        if (hoursDisplay == "22")
        {
            curfew = true;
        }
        else if (hoursDisplay == "06")
        {
            curfew = false;
        }
    }//end update

    public int getMinutesDisplay()
    {
         return (int)minutes % 60;
    }

    public int getHoursDisplay()
    {
        return (int)hours % 24;
    }
    public string GetHours()
    {
        return hoursDisplay;

    }//end getHours

    public void setDisplayMinutesToZero()
    {
        minutesDisplay = "00";
    }
    public void setDisplayHours( int i)
    {
        hoursDisplay = $"{((int)hours % 24) + i}";

    }

    public void changeAddedTime(float f)
    {
        addedTime += f;
    }
    public void updateTime()
    {
        totalSeconds = Time.fixedTime + addedTime;

        //We want each second to be one minute in this game.
        minutes = totalSeconds;
        hours = minutes / 60;
        days = hours / 24;

        if ((minutes % 60) < 10)
        {
            minutesDisplay = $"0{(int)minutes % 60}";
        }
        else
        {
            minutesDisplay = $"{(int)minutes % 60}";
        }

        if ((hours % 24) < 10)
        {
            hoursDisplay = $"0{(int)hours % 24}";
        }
        else
        {
            hoursDisplay = $"{(int)hours % 24}";
        }

        displayText.text = $"Day {(int)days} Time {hoursDisplay}:{minutesDisplay}";

    }

}
