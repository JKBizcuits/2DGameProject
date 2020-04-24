using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    private Controller controller;
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public string dialog;
    public GameObject currentInterObj;
    public string playerStatus;
    int value;
    public int[] job;
    public bool selling;
    public bool jobbing;
    public bool sleeping;
    public bool jobMode;
    public bool sleepMode;
    public GameObject clock;
    private TimeTracker time;
    int workedHours;
    public GameObject prefab;
    private int i;
    public bool taxing;
    public int taxValue;
    public int late;
    public bool taxOverDue;
    
    public int taxExchange;
    public bool renewTax;
    public bool renting;
    public int rentValue;
    public int rentExchange;
    public bool renewRent;
    public bool housing;
    public int houseCost;
    public string potentialStatus;
    public bool victory;
    public GameObject guard;
    public Guard guardScript;
    public bool curfew;


    private void Start()
    {
        controller = GetComponent<Controller>();
        time = clock.GetComponent<TimeTracker>();
        workedHours = 0;
        taxValue = 100;
        late = 0;
        renewTax = true;
        renewRent = true;
        rentValue = 50;
        potentialStatus = "townfolk";
        guardScript = guard.GetComponent<Guard>();


    }

    void Update()
    {
        if (currentInterObj != null && Input.GetButtonDown("Interact"))
        {
            if (selling == true && controller.gold >= value)
            {

                controller.takeMoney(value);
                prefab = currentInterObj.GetComponent<InteractionObject>().prefab;
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            else if (jobbing == true && job[1] > workedHours )
            {
                jobMode = true;
                
            }
            else if (sleeping == true)
            {
                
                sleepMode = true;
                controller.sleep = true;
                /*if (time.getHoursDisplay() > 6)
                {


                    time.changeAddedTime((30 - time.getHoursDisplay()) * 60);
                    controller.GetHungrier(1);
                }
                else if (time.getHoursDisplay() < 6)
                {
                    time.changeAddedTime((6 - time.getHoursDisplay()) * 60);
                }
                else
                {
                    time.changeAddedTime((24 * 60));
                }*/
            }
            else if (taxing == true)
            {
                if(taxValue > controller.gold)
                {
                    taxExchange = controller.gold;
                    taxValue -= taxExchange;
                    controller.takeMoney(taxExchange);
                    dialogText.text = "Not all of your tax was paid. You still owe " + taxValue;
                }
                else if (taxValue == 0)
                {
                    dialogText.text = "You've already paid you twat";
                }
                else
                {
                    controller.takeMoney(taxValue);
                    taxValue = 0;
                    dialogText.text = "Congratulations! You've paid everything. For now";
                    taxOverDue = false;
                }
                
            }
            else if (renting == true)
            {
                if (rentValue > controller.gold)
                {
                    rentExchange = controller.gold;
                    rentValue -= rentExchange;
                    controller.takeMoney(rentExchange);
                    dialogText.text = "Not all of your rent was paid. You still owe " + rentValue;
                }
                else if(rentValue == 0)
                {
                    dialogText.text = "You've already paid you ninny";
                }
                else
                {
                    controller.takeMoney(rentValue);
                    rentValue = 0;
                    dialogText.text = "Congratulations! You've paid everything. For now";
                    
                }

            }
            
        }
        if (currentInterObj != null && Input.GetButtonDown("Interact2"))
        {
            if(renting == true)
            {
                if(playerStatus == "pleb")
                {
                    if(controller.gold >= 1000)
                    {
                        dialogText.text = "Huh. You've scrapped enough for a better place. Do you want to upgrade for 1000 gold? Press Z to buy";
                        houseCost = 1000;
                        housing = true;
                        potentialStatus = "townfolk";
                    }
                    else if (controller.gold < 1000)
                    {
                        dialogText.text = "Get out of here you pleb and don't come back unless you have 1000";
                    }
                }
                else if(playerStatus == "townfolk")
                {
                    if (controller.gold >= 5000 && rentValue == 0)
                    {
                        dialogText.text = "Huh. You've scrapped enough for a better place. Do you want to upgrade for 5000 gold? Press Z to buy";
                        houseCost = 5000;
                        housing = true;
                        potentialStatus = "fancylad";
                    }
                    else if (rentValue > 0)
                    {
                        dialogText.text = "Why don't ya get me my rent money first huh?";
                    }
                    else if (controller.gold < 5000)
                    {
                        dialogText.text = "Get out of here you poor bastard and don't come back unless you have 5000";
                    }
                    
                }
                else if (playerStatus == "fancylad")
                {
                    if (controller.gold >= 15000)
                    {
                        dialogText.text = "Hello sir, would you like to upgrade for 15000? Press Z to buy";
                        houseCost = 15000;
                        
                        victory = true;
                    }
                    else if (controller.gold < 15000)
                    {
                        dialogText.text = "Sorry sir, but you seem to lack the 15000";
                    }
                }

            }
        }
        if (currentInterObj != null && Input.GetButtonDown("Interact3"))
        {
            if (housing == true)
            {
                dialogText.text = "Congratulations! Your house isn't as shit!";
                playerStatus = potentialStatus;
                controller.takeMoney(houseCost);
                housing = false;
            }
            else if(victory == true)
            {
                //win
            }
        }



        if (jobMode == true)
        {
            if (time.getHoursDisplay() < 22 && time.getHoursDisplay() > 6 && job[1] > workedHours)    
            {
                controller.AddMoney(job[0]);
                time.addedTime += (60 - time.getMinutesDisplay());
                workedHours++;

            }
            else
            {
                jobMode = false;

            }

        }
        else if(sleepMode == true)
        {
            if(time.getHoursDisplay() != 6)
            {
                time.addedTime += (60 - time.getMinutesDisplay());
                controller.GetHungrier(1);
                controller.healthRegen(controller.currentFood);
                controller.TakeDamage((controller.deltaHealth)/2);
            }
            else
            {
                sleepMode = false;
                controller.sleep = true;    
            }
        }

        if(time.getHoursDisplay() == 6)
        {
            workedHours = 0;
            
            guardScript.taxCollected = false;

        }

        if (((int)time.days % 7) == 0 && renewTax == false)
        {
            if (taxValue != 0)
            {
                taxOverDue = true;
                late = (int)System.Math.Ceiling((double)taxValue * .25);
                taxValue += late;

            }
            taxValue += 100;
            renewTax = true;           

        }
        else if(((int)time.days % 7) != 0)
        {
            renewTax = false;
        }

        if (((int)time.days % 7) == 0 && renewRent == false)
        {
            if (rentValue != 0)
            {
                
                late = (int)System.Math.Ceiling((double)rentValue * .25);
                rentValue += late;

            }
            if(playerStatus == "pleb")
            {
                rentValue += 50;
            }
            else if (playerStatus == "townfolk")
            {
                rentValue += 100;
            }
            else if (playerStatus == "fancylad")
            {
                rentValue += 200;
            }

            renewRent = true;

        }
        else if (((int)time.days % 7) != 0)
        {
            renewRent = false;
        }

        curfew = time.curfew;
        guardScript.curfew = curfew;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Interactible"))
        {
            currentInterObj = collision.gameObject;

            if (currentInterObj.CompareTag("Interactible"))
            {
                if (currentInterObj.name == "BreadStall")
                {
                    value = 15;
                    selling = true;
                    dialogText.text = "Want to buy bread?\n Press X\n to buy for " + value + " gold";
                    dialogBox.SetActive(true);
                    
                }

                else if(currentInterObj.name == "BeetStall")
                {
                    value = 5;
                    selling = true;
                    dialogText.text = "Do you actually want to buy some beets? Press X to buy for " + value + " gold";
                    dialogBox.SetActive(true);
                }

                else if (currentInterObj.name == "AppleStall")
                {
                    value = 8;
                    selling = true;
                    dialogText.text = "Want to buy an apple?\n Press X\n to buy for " + value + " gold";
                    dialogBox.SetActive(true);
                }

                else if (currentInterObj.name == "CarrotStall")
                {
                    value = 6;
                    selling = true;
                    dialogText.text = "Want to buy a carrot?\n Press X\n to buy for " + value + " gold";
                    dialogBox.SetActive(true);

                }

                else if (currentInterObj.name == "BananaStall")
                {
                    value = 7;
                    selling = true;
                    dialogText.text = "Want to buy some bananas? Press X to buy for " + value + " gold";
                    dialogBox.SetActive(true);

                }

                else if (currentInterObj.name == "PeaStall")
                {
                    value = 2;
                    selling = true;
                    dialogText.text = "Want to buy peas?\n Press X\n to buy for " + value + " gold";
                    dialogBox.SetActive(true);

                }

                else if (currentInterObj.name == "PepperStall")
                { 
                    value = 3;
                    selling = true;
                    dialogText.text = "Want to buy a pepper?\n Press X\n to buy for " + value + " gold";
                    dialogBox.SetActive(true);

                }

                else if (currentInterObj.name == "BookStall")
                {
                    value = 100;
                    selling = true;
                    dialogText.text = "Want to buy a book?\n Press X\n to buy for " + value + " gold";
                    dialogBox.SetActive(true);

                }

                else if (currentInterObj.name == "OrangeStall")
                {
                    value = 7;
                    selling = true;
                    dialogText.text = "Want to buy an orange?\n Press X\n to buy for " + value + " gold";
                    dialogBox.SetActive(true);

                }

                else if (currentInterObj.name == "CornStall")
                {
                    value = 11;
                    selling = true;
                    dialogText.text = "Want to buy some corn?\n Press X\n to buy for " + value + " gold";
                    dialogBox.SetActive(true);

                }

                else if (currentInterObj.name == "Timothy")
                {

                    taxing = true;
                    dialogText.text = "Need to pay your weekly tax? Press X to pay " + taxValue;
                    dialogBox.SetActive(true);
                }
                else if (currentInterObj.name == "Robert Barone")
                {

                    renting = true;
                    dialogText.text = "Need to pay your weekly rent? Press X to pay " + rentValue + " or C to upgrade house";
                    dialogBox.SetActive(true);
                }


                if (playerStatus == "pleb")
                {
                    if (currentInterObj.name == "JobFarmRight" || currentInterObj.name == "JobFarmLeft")
                    {

                        job[0] = 6;
                        job[1] = 10;
                        jobbing = true;
                        dialogText.text = "Want to work? Press X to work for " + job[0] + " gold per hour for up to " + job[1] + " hours";
                        dialogBox.SetActive(true);
                    }
                    else if (currentInterObj.name == "House0")
                    {
                        sleeping = true;
                        dialogText.text = "Want to sleep?\n Press X";
                        dialogBox.SetActive(true);
                    }
                    
                }
                else if(playerStatus == "townfolk")
                {
                    if (currentInterObj.name == "JobBlacksmith")
                    {
                        job[0] = 7;
                        job[1] = 10;
                        jobbing = true;
                        dialogText.text = "Want to work?\n Press X\n to work for " + job[0] + " gold per hour for up to " + job[1] + " hours";
                        dialogBox.SetActive(true);
                    }
                    else if (currentInterObj.name == "JobTailor")
                    {
                        job[0] = 9;
                        job[1] = 7;
                        jobbing = true;
                        dialogText.text = "Want to work?\n Press X\n to work for " + job[0] + " gold per hour for up to " + job[1] + " hours";
                        dialogBox.SetActive(true);
                    }
                    else if (currentInterObj.name == "House1")
                    {
                        sleeping = true;
                        dialogText.text = "Want to sleep?\n Press X";
                        dialogBox.SetActive(true);
                    }
                }
                else if (playerStatus == "fancylad")
                {
                    if (currentInterObj.name == "JobMerchent")
                    {

                        job[0] = 10;
                        job[1] = 9;
                        jobbing = true;
                        dialogText.text = "Want to work? Press X to work for " + job[0] + " gold per hour for up to " + job[1] + " hours";
                        dialogBox.SetActive(true);
                    }

                    if (currentInterObj.name == "JobTrader")
                    {
                        job[0] = 11;
                        job[1] = 8;
                        jobbing = true;
                        dialogText.text = "Want to work? Press X to work for " + job[0] + " gold per hour for up to " + job[1] + " hours";
                        dialogBox.SetActive(true);
                    }
                    else if (currentInterObj.name == "House2")
                    {
                        sleeping = true;
                        dialogText.text = "Want to sleep?\n Press X";
                        dialogBox.SetActive(true);
                    }
                }


            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactible"))
        {
            
            sleeping = false;
            jobbing = false;
            selling = false;
            currentInterObj = null;
            dialogBox.SetActive(false);
            renting = false;
            taxing = false;
            housing = false;
        }

             

    }
}


/*public GameObject dialogBox;
public TextMeshProUGUI dialogText;
public string[] dialog;
private int i;

GameObject currentInterObj = null;
public string playerStatus = "pleb";
public string currentJob = null;
public bool jobDone = false;
public string possibleStatus = "pleb";
bool jobbing;

string[] item;
int[] job;

private void Update()
{
    if (GameObject.Find("Time").GetComponent<TimeTracker>().curfew == true)
        jobDone = false;
    if (currentInterObj != null && Input.GetButtonDown("Interact"))
    {
        if (currentInterObj.GetComponent<InteractionObject>().sell == true)
        {
            currentInterObj.GetComponent<InteractionObject>().selling(item);
        }
        else if (jobbing == true)
        {
            GameObject.Find("Character").GetComponent<Controller>().AddMoney(20);
        } /*else if (currentInterObj.GetComponent<InteractionObject>().sleep == true)
        {

            currentInterObj.GetComponent<InteractionObject>().sleeping();
        }*/

/* }

}

void OnTriggerEnter2D(Collider2D collision)
{
 //Debug.Log("Job Check, " + currentInterObj.tag + ", " + currentInterObj.name);
 item = new string[2];
 job = new int[2];

 if (collision.CompareTag("Interactible") || collision.CompareTag("Level0Job") || collision.CompareTag("Level1Job") ||
     collision.CompareTag("Level2Job") || collision.CompareTag("Level0House") || collision.CompareTag("Level1House") ||
     collision.CompareTag("Level2House"))
 {
     currentInterObj = collision.gameObject;

     if (currentInterObj.CompareTag("Interactible"))
     {
         if (currentInterObj.name == "BreadStall")
         {

             item[0] = "bread";
             item[1] = "15";
             //currentInterObj.SendMessage("DoSellInteraction", item);
             currentInterObj.GetComponent<InteractionObject>().DoSellInteraction();
         }

         if (currentInterObj.name == "BeetStall")
         {
             item[0] = "beets";
             item[1] = "5";
             currentInterObj.GetComponent<InteractionObject>().DoSellInteraction();
         }

         if (currentInterObj.name == "AppleStall")
         {
             item[0] = "apples";
             item[1] = "8";
             currentInterObj.GetComponent<InteractionObject>().DoSellInteraction();
         }

         if (currentInterObj.name == "CarrotStall")
         {
             item[0] = "carrots";
             item[1] = "6";
             currentInterObj.GetComponent<InteractionObject>().DoSellInteraction();
         }

         if (currentInterObj.name == "BananaStall")
         {
             item[0] = "bananas";
             item[1] = "7";
             currentInterObj.GetComponent<InteractionObject>().DoSellInteraction();
         }

         if (currentInterObj.name == "PeaStall")
         {
             item[0] = "peass";
             item[1] = "2";
             currentInterObj.GetComponent<InteractionObject>().DoSellInteraction();
         }

         if (currentInterObj.name == "PepperStall")
         {
             item[0] = "peppers";
             item[1] = "3";
             currentInterObj.GetComponent<InteractionObject>().DoSellInteraction();
         }

         if (currentInterObj.name == "BookStall")
         {
             item[0] = "books";
             item[1] = "100";
             currentInterObj.GetComponent<InteractionObject>().DoSellInteraction();
         }

         if (currentInterObj.name == "OrangeStall")
         {
             item[0] = "oranges";
             item[1] = "7";
             currentInterObj.GetComponent<InteractionObject>().DoSellInteraction();
         }

         if (currentInterObj.name == "CornStall")
         {
             item[0] = "corn";
             item[1] = "11";
             currentInterObj.GetComponent<InteractionObject>().DoSellInteraction();
         }

         if (currentInterObj.name == "JobBlacksmith")
         {
             job[0] = 7;
             job[1] = 10;
             jobbing = true;
         }



     }
     /*
     else if (jobDone == false)
     {

         if (currentInterObj.CompareTag("Level0Job"))
         {
             if (playerStatus == "pleb")
             {
                 if (currentInterObj.name == "JobFarmRight" || currentInterObj.name == "JobFarmLeft")
                 {

                     job[0] = 6;
                     job[1] = 10;
                     currentInterObj.GetComponent<InteractionObject>().DoJobInteraction();
                 }
             }
         }
         else if (currentInterObj.CompareTag("Level1Job"))
         {
             if (playerStatus == "townfolk")
             {
                 if (currentInterObj.name == "JobBlacksmith")
                 {
                     job[0] = 7;
                     job[1] = 10;
                     currentInterObj.GetComponent<InteractionObject>().DoJobInteraction();
                 }

                 if (currentInterObj.name == "JobTailor")
                 {
                     job[0] = 9;
                     job[1] = 7;
                     currentInterObj.GetComponent<InteractionObject>().DoJobInteraction();
                 }
             }
         }
         else if (currentInterObj.CompareTag("Level2Job"))
         {
             if (playerStatus == "fancylad")
             {
                 if (currentInterObj.name == "JobMerchent")
                 {

                     job[0] = 10;
                     job[1] = 9;
                     currentInterObj.GetComponent<InteractionObject>().DoJobInteraction();
                 }

                 if (currentInterObj.name == "JobTrader")
                 {
                     job[0] = 11;
                     job[1] = 8;
                     currentInterObj.GetComponent<InteractionObject>().DoJobInteraction();
                 }
             }
         }
     }

     if (currentInterObj.CompareTag("Level0House"))
     {

         if (playerStatus == "pleb")

         currentInterObj.GetComponent<InteractionObject>().DoSleepInteraction();
     }

     if (currentInterObj.CompareTag("Level1House"))
     {
         if (playerStatus == "townfolk")
             currentInterObj.GetComponent<InteractionObject>().DoSleepInteraction();
     }

     if (currentInterObj.CompareTag("Level0House"))
     {
         if (playerStatus == "fancylad")
             currentInterObj.GetComponent<InteractionObject>().DoSleepInteraction();
     }

 }
}
*/
/* void OnTriggerExit2D(Collider2D other)
 {
     if (other.CompareTag("Interactible") || other.CompareTag("Level0Job") || other.CompareTag("Level1Job") ||
         other.CompareTag("Level2Job") || other.CompareTag("Level0House") || other.CompareTag("Level1House") ||
         other.CompareTag("Level2House"))
     {
         currentInterObj = other.gameObject;
         currentInterObj.SendMessage("LeaveInteraction");
         currentInterObj = null;
     }
     /*if (other.gameObject == currentInterObj.gameObject)
     {
         currentInterObj.SendMessage("LeaveInteraction");
         currentInterObj = null;

     }*/

/* }

}
}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    
    GameObject currentInterObj = null;
    public string playerStatus = "pleb";
    public string currentJob = null;
    public bool jobDone = false;
    private void Update()
    {
        if (GameObject.Find("Time").GetComponent<TimeTracker>().curfew == true)
            jobDone = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        string[] item = new string[2];
        int[] job = new int[2];

        if (collision.CompareTag("Interactible"))
        {
            
            currentInterObj = collision.gameObject;

            if (currentInterObj.name == "BreadStall")
            {
                item[0] = "bread";
                item[1] = "15.0";
                currentInterObj.SendMessage("DoSellInteraction", item);
            }

            if (currentInterObj.name == "BeetStall")
            {
                item[0] = "beets";
                item[1] = "5.0";
                currentInterObj.SendMessage("DoSellInteraction", item);
            }

            if (currentInterObj.name == "AppleStall")
            {
                item[0] = "apples";
                item[1] = "8.0";
                currentInterObj.SendMessage("DoSellInteraction", item);
            }

            if (currentInterObj.name == "CarrotStall")
            {
                item[0] = "carrots";
                item[1] = "6.0";
                currentInterObj.SendMessage("DoSellInteraction", item);
            }

            if (currentInterObj.name == "BananaStall")
            {
                item[0] = "bananas";
                item[1] = "7.0";
                currentInterObj.SendMessage("DoSellInteraction", item);
            }

            if (currentInterObj.name == "PeaStall")
            {
                item[0] = "peass";
                item[1] = "2.0";
                currentInterObj.SendMessage("DoSellInteraction", item);
            }

            if (currentInterObj.name == "PepperStall")
            {
                item[0] = "peppers";
                item[1] = "3.0";
                currentInterObj.SendMessage("DoSellInteraction", item);
            }

            if (currentInterObj.name == "BookStall")
            {
                item[0] = "books";
                item[1] = "100.0";
                currentInterObj.SendMessage("DoSellInteraction", item);
            }

            if (currentInterObj.name == "OrangeStall")
            {
                item[0] = "oranges";
                item[1] = "7.0";
                currentInterObj.SendMessage("DoSellInteraction", item);
            }

            if (currentInterObj.name == "CornStall")
            {
                item[0] = "corn";
                item[1] = "11.0";
                currentInterObj.SendMessage("DoSellInteraction", item);
            }
        }

        if (jobDone == false)
        {
            if (collision.CompareTag("Level0Job"))
            {
                if (playerStatus == "pleb")
                {
                    if (currentInterObj.name == "JobFarmRight" || currentInterObj.name == "JobFarmLeft")
                    {
                        job[0] = 6;
                        job[1] = 10;
                        currentInterObj.SendMessage("DoJobInteraction", "job");
                    }
                }
            }

            if (collision.CompareTag("Level1Job"))
            {
                if (playerStatus == "townfolk")
                {
                    if (currentInterObj.name == "JobBlacksmith")
                    {
                        job[0] = 7;
                        job[1] = 10;
                        currentInterObj.SendMessage("DoJobInteraction", "job");
                    }

                    if (currentInterObj.name == "JobTailor")
                    {
                        job[0] = 9;
                        job[1] = 7;
                        currentInterObj.SendMessage("DoJobInteraction", "job");
                    }
                }
            }

            if (collision.CompareTag("Level2Job"))
            {
                if (playerStatus == "fancylad")
                {
                    if (currentInterObj.name == "JobMerchent")
                    {
                        job[0] = 10;
                        job[1] = 9;
                        currentInterObj.SendMessage("DoJobInteraction", "job");
                    }

                    if (currentInterObj.name == "JobTrader")
                    {
                        job[0] = 11;
                        job[1] = 8;
                        currentInterObj.SendMessage("DoJobInteraction", "job");
                    }
                }
            }
        }

        if (collision.CompareTag("Level0House"))
        {
            if (playerStatus == "pleb")
                currentInterObj.SendMessage("DoSleepInteraction");
        }

        if (collision.CompareTag("Level1House"))
        {
            if (playerStatus == "townfolk")
                currentInterObj.SendMessage("DoSleepInteraction");
        }

        if (collision.CompareTag("Level0House"))
        {
            if (playerStatus == "fancylad")
                currentInterObj.SendMessage("DoSleepInteraction");
        }
    }

        
    void OnTriggerExit2D(Collider other)
    {
        if (other.CompareTag("Interactible"))
        {
            if (other.gameObject == currentInterObj)
            {
                currentInterObj.SendMessage("LeaveInteraction");
                currentInterObj = null;
            }

        }
    }
}
    
    */
      