using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxCollector : MonoBehaviour
{

    private int lumpSum;
    public int lateDue;
    public int currentDue;
    public int lateInterest;
    public int gracePeriod;
    public int gracePeriodRenewal;
    public int tier;
    private GameObject clock;
    private GameObject guard;

    // Start is called before the first frame update
    void Start()
    {
        clock = GameObject.Find("Clock");
        guard = GameObject.Find("Guard");
    }

    // Update is called once per frame
    void Update()
    {
        if (tier == 0)
        {
            gracePeriodRenewal = 0;
        }
        else if (tier == 1)
        {
            gracePeriodRenewal = 1;
        }
        else if (tier == 2)
        {
            gracePeriodRenewal = 2;
        }
        if (((int)clock.GetComponent<TimeTracker>().days % 7) == 0)
        {
            if(lateDue == 0)
            {
                gracePeriod = gracePeriodRenewal;
            }
            lateDue += currentDue;
            lateDue = lateDue * lateInterest;
            currentDue += lumpSum;

        }

        if (((int)clock.GetComponent<TimeTracker>().days % 7) == gracePeriod)
        {
            gracePeriod = 0;
            if(gracePeriod == 0)
            {
                guard.GetComponent<Guard>().gracePeriod = false;
            }
            else
            {
                guard.GetComponent<Guard>().gracePeriod = true;
            }
        }
       


    }
}
