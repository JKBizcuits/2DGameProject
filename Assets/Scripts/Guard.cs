using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public GameObject Player;
    
    private Vector2 movement;
    public float speed;
    public float distance;
    public float outOfRange;
    public double damageModified;
    System.Random rnd = new System.Random();
    
    //triggers
    public bool checkPlayer;
    private int criminalRating;

    public bool gracePeriod = false;
    public bool taxCollected;
    private bool failedSteal;
    public bool taxChase;
    private bool criminalChase;
    public bool curfew;
    public GameObject taxCollector;
    public GameObject clock;
    public bool activePatrol;
    public Transform[] moveSpots;
    private int randomSpot;
    private int lastSpot;
    private float waitTime;
    public float startWaitTime;
    private Animator animator;
    private float deltaX;
    private float deltaY;
    private float absDeltaX;
    private float absDeltaY;

    private TimeTracker timeTracker;
    public TaxCollector taxCollectorScript;

    public GameObject music;
    private MusicPlayer mp3;

    public PlayerInteract playerInteract;




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        waitTime = startWaitTime;
        randomSpot = 0;
        lastSpot = moveSpots.Length -1;
        animator.SetFloat("Speed", speed);
        
        curfew = false;
        checkPlayer = false;
        taxCollected = false;
        failedSteal = false;
        taxChase = false;
        criminalChase = false;
        activePatrol = true;
        
        
        
        timeTracker = clock.AddComponent<TimeTracker>();
        taxCollectorScript = taxCollector.GetComponent<TaxCollector>();
        mp3 = music.GetComponent<MusicPlayer>();
        playerInteract = Player.GetComponent<PlayerInteract>();
    }

    // Update is called once per frame
    void Update()           
    {
        if (activePatrol == true)
        {
            

            //movement = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
            //transform.position = movement;

            if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
            {
                if (waitTime <= 0)
                {

                    lastSpot = randomSpot;
                    randomSpot += 1;
                    if (randomSpot == moveSpots.Length)
                    {
                        randomSpot = 0;
                    }
                    waitTime = startWaitTime;
                    animator.SetFloat("Speed", speed);


                }
                else
                {
                    waitTime -= Time.deltaTime;
                    animator.SetFloat("Speed", 0);



                }
            }






            
            deltaX = moveSpots[randomSpot].position.x - transform.position.x;
            deltaY = moveSpots[randomSpot].position.y - transform.position.y;
            
            /*
            deltaX = moveSpots[randomSpot].position.x - moveSpots[lastSpot].position.x;
            deltaY = moveSpots[randomSpot].position.y - moveSpots[lastSpot].position.y;


            if (deltaX > 0 && deltaY == 0)
            {
                animator.SetFloat("IdleHorizontal", 1);
                animator.SetFloat("IdleVertical", 0);
                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Vertical", 0);
            }
            else if (deltaX < 0 && deltaY == 0)
            {

                animator.SetFloat("IdleHorizontal", -1);
                animator.SetFloat("IdleVertical", 0);
                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 0);
            }
            else if (deltaX == 0 && deltaY > 0)
            {
                animator.SetFloat("IdleHorizontal", 0);
                animator.SetFloat("IdleVertical", 1);
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 1);
            }
            else if (deltaX == 0 && deltaY < 0)
            {
                animator.SetFloat("IdleHorizontal", 0);
                animator.SetFloat("IdleVertical", -1);
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", -1);
            }
            else if (deltaX == 0 && deltaY == 0)
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 0);
            }
            */

        }

        playerInteract = Player.GetComponent<PlayerInteract>();
        criminalRating = Player.GetComponent<Controller>().criminalRating;
        if (taxChase == true || criminalChase == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

            deltaX = Player.transform.position.x - transform.position.x;
            deltaY = Player.transform.position.y - transform.position.y;

            if (Vector2.Distance(transform.position, Player.transform.position) > outOfRange)
            {
                taxChase = false;
                criminalChase = false;
                checkPlayer = false;
                activePatrol = true;
                mp3.chased = false;
            }
            
     
        }
        
        
        if (Vector2.Distance(transform.position, Player.transform.position) < distance && checkPlayer == false)
        {
            
            playerInteract = Player.GetComponent<PlayerInteract>();
            if (playerInteract.taxOverDue == true && taxCollected == false)
            {
                activePatrol = false;
                taxChase = true;
                mp3.chased = true;
                checkPlayer = true;
            }
            else if (/*((criminalRating > 0 && (rnd.Next(1, 100) <= criminalRating)) ||*/ curfew == true /*|| failedSteal == true)*/ && taxChase == false)
            {
                activePatrol = false;
                criminalChase = true;
                mp3.chased = true;
                checkPlayer = true;
            }
        }

        /*if (timeTracker.hoursDisplay == "06")
        {
            taxCollected = false;
        }*/

        

        absDeltaX = System.Math.Abs(deltaX);
        absDeltaY = System.Math.Abs(deltaY);

        if (absDeltaX > absDeltaY)
        {
            if (deltaX > 0)
            {
                animator.SetFloat("IdleHorizontal", 1);
                animator.SetFloat("IdleVertical", 0);
                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Vertical", 0);
            }
            else if (deltaX < 0)
            {

                animator.SetFloat("IdleHorizontal", -1);
                animator.SetFloat("IdleVertical", 0);
                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 0);
            }
        }
        else if (absDeltaX < absDeltaY)
        {
            if (deltaY > 0)
            {
                animator.SetFloat("IdleHorizontal", 0);
                animator.SetFloat("IdleVertical", 1);
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 1);
            }
            else if (deltaY < 0)
            {
                animator.SetFloat("IdleHorizontal", 0);
                animator.SetFloat("IdleVertical", -1);
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", -1);
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject Warrick = collision.gameObject;
        
        if (Warrick.gameObject.name == "Character")
        {
            Controller script = Player.GetComponent<Controller>();
            if (taxChase == true)
            {
                taxChase = false;
                if (script.gold >= 20)
                {
                    script.takeMoney(20);
                    script.TakeDamage(-20);

                }
                else
                {
                    script.TakeDamage(-40);
                }

                
                taxCollected = true;


            }
            else if(criminalChase == true)
            {
                criminalChase = false;


                if(curfew == true)
                {
                    script.TakeDamage(-10);
                }
                else{
                    script.TakeDamage(crimeDamage(script));
                   // script.inventory.clearStolenGoods();
                    script.GetComponent<Controller>().criminalRating = 0;
                }
               




            }

            activePatrol = true;

        }


            
    }

    private int taxDamage()
    {
        int damage =(int)System.Math.Ceiling((double)taxCollectorScript.lateDue * (.5+damageModified));
        
        return damage;
    }

    private void damageModifier(double d)
    {
        damageModified = d;
        
    }

    private int fine(Controller script)
    {
        int fine = taxCollectorScript.lateDue;
        double denominator = (double)fine;
        double numerator = 0;
        if(fine > script.gold)
        {
            taxCollectorScript.lateDue -= script.gold;
            numerator = (double)taxCollectorScript.lateDue;
            fine = script.gold;
           
            damageModifier(numerator / denominator);
        }
        else if(fine<= script.gold)
        {

            taxCollectorScript.lateDue =0;
            
        }

        
        return fine;
    }

    private int crimeDamage(Controller script)
    {
        int damage = 0;// (int)System.Math.Ceiling((double)script.stolenGoodsValue() * .5);

        return damage;
    }

    
}
    

