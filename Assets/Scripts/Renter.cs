using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renter : MonoBehaviour
{
    private int rent;
    private int tier;
    public int lateDue;
    public int currentDue;
    public int lateInterest;
    private GameObject clock;
    // Start is called before the first frame update
    void Start()
    {
        clock = GameObject.Find("Clock");
    }

    // Update is called once per frame
    void Update()
    {
        if(tier == 0)
        {
            //change rent value
        }
        else if(tier == 1)
        {

        }
        else if(tier == 2)
        {

        }

        if (((int)clock.GetComponent<TimeTracker>().days % 7) == 3)
        {
            lateDue += currentDue;
            lateDue = lateDue * lateInterest;
            currentDue += rent;
        }
    }
}
