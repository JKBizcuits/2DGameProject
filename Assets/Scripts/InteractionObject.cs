using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class InteractionObject : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public string[] dialog;
    private int i;

    bool stolenFrom = false;
    public Canvas messageCanvas;
    public GameObject prefab;
    private GameObject clock;
    private TimeTracker time;
    private int hours;
    public GameObject character;
    private int money;
    private string status;

    public bool sell;
    public bool profession;
    public bool sleep;

    private int finalPrice;

    private void Start()
    {
        messageCanvas.enabled = false;
        clock = GameObject.Find("Clock");
        time = clock.GetComponent<TimeTracker>();
        character = GameObject.Find("Character");
        sell = false;
        profession = false;
        sleep = false;
    }

    public void DoSellInteraction()
    {
        sell = true;
        messageCanvas.enabled = true;
           
        /*money = character.GetComponent<Controller>().gold;
        if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }
        else
        {
            dialogBox.SetActive(true);
        }
        string itemName = item[1];
        double itemPrice = Convert.ToDouble(item[2]);

        double increase = itemPrice * .1;
        if (stolenFrom == true)
        {
            itemPrice += increase;
            Math.Ceiling(itemPrice);
        }

        Convert.ToInt32(itemPrice);

        if (Input.GetButtonDown("Interact"))
        {
            dialogBox.SetActive(true);
                i = 0;
                dialogText.text = dialog[i];
            if(money >= itemPrice)
            {
                
                SendMessage("SubtractMoney", "itemPrice");
                //Inventory.NextSlot(itemName);
                //show item in inventory

            }

            else
            {
                
            } 
        }*/
    }

    public void selling(string[] item)
    {
        
        money = character.GetComponent<Controller>().gold;
        /*if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }
        else
        {
            dialogBox.SetActive(true);
        }*/
        string itemName = item[0];
        int itemPrice = int.Parse(item[1]);
        

  
        if (stolenFrom == true)
        {
            double increase = (double)itemPrice * .1;
            increase += (double)itemPrice;
            itemPrice += (int)Math.Ceiling(increase);
            
        }

        finalPrice = Convert.ToInt32(itemPrice);
        if (money >= itemPrice)
        {

            //SendMessage("SubtractMoney", "itemPrice");
            character.GetComponent<Controller>().takeMoney(itemPrice);
            Instantiate(prefab, GameObject.Find("Character").transform.position, Quaternion.identity);

            if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }
        else
        {
            dialogBox.SetActive(true);
        }

        }

        else
        {

        }
        /*
        if (Input.GetButtonDown("Interact"))
        {
            dialogBox.SetActive(true);
                i = 0;
                dialogText.text = dialog[i];
            if (money >= itemPrice)
            {

                SendMessage("SubtractMoney", "itemPrice");
                money -= finalPrice;
                //Inventory.NextSlot(itemName);
                //show item in inventory

            }

            else
            {

            }
        }*/
    }

    public void IsStolenFrom()
    {
        stolenFrom = true;
    }

    public void LeaveInteraction()
    {
        messageCanvas.enabled = false;
        sell = false;
        sleep = false;
        profession = false;
    }

    public void DoJobInteraction()
    {
        /*if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }
        else
        {
            dialogBox.SetActive(true);
        } */
        /*
        //Console.WriteLine("Job Interaqction"); // You spelled interaction wrong
        int hoursPassed = 0;
        int fullPay = 0;
        hours = time.getHoursDisplay();
        //int time = Convert.ToInt32(GameObject.Find("Time").GetComponent<TimeTracker>().hoursDisplay);
        money = character.GetComponent<Controller>().gold;
        int maxHours = job[0];
        int hourPay = job[1];*/

        if (hours < 22 && hours > 5)
        {
            messageCanvas.enabled = true;
            profession = true;

            /*if (Input.GetButtonDown("Interact"))
            {
                character.GetComponent<PlayerInteract>().jobDone = true;
                messageCanvas.SendMessage("FadeImage", "false");

                while (hours > 22 && hours < 6)
                {
                    
                    time.addedTime += (60- time.getMinutesDisplay());
                    hoursPassed += 1;
                    //FoodPoints -= 2;
                }
            }

            messageCanvas.enabled = false;
            fullPay = hoursPassed * hourPay;
            SendMessage("AddMoney", "fullPay");
            SendMessage("FadeImage", "true");*/
        }

        else
        {
            messageCanvas.enabled = false;
        }

    }

    public void working(int[] job)
    {
        money = character.GetComponent<Controller>().gold;
        int hoursPassed = 0;
        int fullPay = 0;
        hours = time.getHoursDisplay();
        //int time = Convert.ToInt32(GameObject.Find("Time").GetComponent<TimeTracker>().hoursDisplay);
        
        int maxHours = job[0];
        int hourPay = job[1];

        character.GetComponent<PlayerInteract>().jobDone = true;
        messageCanvas.SendMessage("FadeImage", "false");

        while (hours > 22 && hours < 6)
        {

            time.addedTime += (60 - time.getMinutesDisplay());
            hoursPassed += 1;
            //FoodPoints -= 2;
        }

        messageCanvas.enabled = false;
        fullPay = hoursPassed * hourPay;
        //SendMessage("AddMoney", "fullPay");
        character.GetComponent<Controller>().AddMoney(fullPay);
        SendMessage("FadeImage", "true");
    }

    public void DoSleepInteraction()
    {
        
        sleep = true;
        messageCanvas.enabled = true;/*
        status = character.GetComponent<PlayerInteract>().playerStatus;
        //int time = Convert.ToInt32(GameObject.Find("Time").GetComponent<TimeTracker>().hoursDisplay);
        money = character.GetComponent<Controller>().gold;
        hours = time.getHoursDisplay();

        if (Input.GetButtonDown("Interact"))
        {
            messageCanvas.SendMessage("FadeImage", "false");

            if (money > 100000 && status == "fancylad")
            {
                //win
                //honestly, nobody gonna win, so I'm a just leave this one blank XD
            }

            if (money > 10000 && status == "townfolk")
            {
                character.GetComponent<PlayerInteract>().possibleStatus = "fancylad";
            }

            if (money > 1000 && status == "pleb")
            {
                character.GetComponent<PlayerInteract>().possibleStatus = "townfolk";
            }

            while (hours != 6)
            {
                character.GetComponent<Controller>().sleep = true;
                time.addedTime += (60 - time.getMinutesDisplay());
                character.GetComponent<Controller>().GetHungrier(1);
            }

            messageCanvas.SendMessage("FadeImage", "true");
            character.GetComponent<Controller>().sleep = false;
        }*/

    }

    public void sleeping()
    {
        
        status = character.GetComponent<PlayerInteract>().playerStatus;
        //int time = Convert.ToInt32(GameObject.Find("Time").GetComponent<TimeTracker>().hoursDisplay);
        money = character.GetComponent<Controller>().gold;
        hours = time.getHoursDisplay();
        
        messageCanvas.SendMessage("FadeImage", "false");
        
        if (money > 100000 && status == "fancylad")
        {
            //win
            //honestly, nobody gonna win, so I'm a just leave this one blank XD
        }

        if (money > 10000 && status == "townfolk")
        {
            character.GetComponent<PlayerInteract>().possibleStatus = "fancylad";
        }

        if (money > 1000 && status == "pleb")
        {
            character.GetComponent<PlayerInteract>().possibleStatus = "townfolk";
        }
        
        while (hours != 6)
        {
            character.GetComponent<Controller>().sleep = true;
            time.addedTime += (60 - time.getMinutesDisplay());
            time.updateTime();
            character.GetComponent<Controller>().GetHungrier(1);
        }
        
        messageCanvas.SendMessage("FadeImage", "true");
        character.GetComponent<Controller>().sleep = false;
        if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }
        else
        {
            dialogBox.SetActive(true);
        }
    }
}/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    bool stolenFrom = false;
    public Canvas messageCanvas;

    private void Start()
    {
        messageCanvas.enabled = false;
    }

    public void DoSellInteraction(string[] item)
    {
        messageCanvas.enabled = true;

        string itemName = item[1];
        double itemPrice = Convert.ToDouble(item[2]);

        double increase = itemPrice * .1;
        if (stolenFrom == true)
            itemPrice += increase;
    }

    public void IsStolenFrom()
    {
        stolenFrom = true;
    }

    public void LeaveInteraction()
    {

    }
    
}
*/