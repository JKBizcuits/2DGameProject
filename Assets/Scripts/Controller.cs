/**
 * Movement Script
 * This script serves to add a movement component for a kinematic rigidbody object
 * in unity through the use of vectors.
 * @cporter
 * @version 1.0
 * Spring 2020
 * 2DGameProjectEAFJAMCNP
 * https://youtu.be/CeXAiaQOzmY
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controller : MonoBehaviour
{

    //Any variables that do not have a value will be assigned
    //a textbox under the script in unity where you can edit the value.
    public float speed;
    

    //Health values

    public int maxHealth;

    public int currentHealth;
    public HealthBarScript healthBar;
    public Canvas deathScreen;

    public int maxFood;
    public int currentFood;
    public FoodBarScript foodBar;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private Animator animator;

    public int gold;
    public int tier;
    public int criminalRating;

    public TimeTracker time;
    public string previousHours;

    public bool sleep;

    public TextMeshProUGUI moneyCount;

    public int deltaHealth;

    public bool firstFrame;

    public Canvas winScreen;
    public ParticleSystem confetti;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Initializes the physics engine of your rigid body to the sprite
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        deathScreen.enabled = false;

        currentFood = maxFood;
        foodBar.SetMaxFood(maxFood);
        currentFood += 4;

        time = GameObject.Find("Clock").AddComponent<TimeTracker>();
        previousHours = "06";
        sleep = false;
        gold = 100;
        moneyCount.text = $"{gold}";
        firstFrame = false;

        winScreen.enabled = false;

    }//end Start



    // Update is called once per frame. Here, we are gathering player input.
    void Update()
    {
        /**
         * Vector2 means a vector in 2 space. Here, we create a new vector and use use the Input object in unity that reads key controls.
         * Key names and other controls are designated under Edit > Project Settings > Input.
         * There are two different versions of GetAxis we can use.
         * Input.GetAxis(String) has a smoother movement where the character gradually speeds up and slows after the key is let go.
         * Input.GetAxisRaw(String) has a snappier movement where the character reaches max speed as soon as the key is pressed
         * and stops immediately after the key is let go.
         */

        Vector2 inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        if (currentHealth <= 0)
        {
            deathScreen.enabled = true;
        }

        else
        {
        


        

            if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                animator.SetFloat("IdleHorizontal", inputMovement.x);
                animator.SetFloat("IdleVertical", inputMovement.y);
                animator.SetFloat("Horizontal", inputMovement.x);
                animator.SetFloat("Vertical", inputMovement.y);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") == 0)
            {

                animator.SetFloat("IdleHorizontal", inputMovement.x);
                animator.SetFloat("IdleVertical", inputMovement.y);
                animator.SetFloat("Horizontal", inputMovement.x);
                animator.SetFloat("Vertical", inputMovement.y);
            }
            else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") > 0)
            {
                animator.SetFloat("IdleHorizontal", inputMovement.x);
                animator.SetFloat("IdleVertical", inputMovement.y);
                animator.SetFloat("Horizontal", inputMovement.x);
                animator.SetFloat("Vertical", inputMovement.y);
            }
            else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") < 0)
            {
                animator.SetFloat("IdleHorizontal", inputMovement.x);
                animator.SetFloat("IdleVertical", inputMovement.y);
                animator.SetFloat("Horizontal", inputMovement.x);
                animator.SetFloat("Vertical", inputMovement.y);
            }
            else if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                animator.SetFloat("Horizontal", inputMovement.x);
                animator.SetFloat("Vertical", inputMovement.y);
            }


            /**
             * Here, we take our normal move vector and add a vector speed to it. However; an issue with this is that if we were to move diagonally,
             * like normal physics we would have a greater speed then we did moving side to side. Adding the normalized method fixes this and the
             * speed is kept constant.
             */

            animator.SetFloat("Speed", inputMovement.sqrMagnitude);

            moveVelocity = inputMovement.normalized * speed;
      }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                TakeDamage(2);
            }


        

        if(previousHours != time.GetHours() && sleep == false && firstFrame == true)
        {
            
            GetHungrier(2);
            previousHours = time.GetHours();
            healthRegen(currentFood);
            TakeDamage(deltaHealth);

        }


        if(currentFood < 0)
        {
            foodBar.SetFood(0);
            currentFood = 0;
        }
        else if(currentFood > 100)
        {
            foodBar.SetFood(100);
            currentFood = 100;
        }

        if (currentHealth < 0)
        {
            healthBar.SetHealth(0);
            currentHealth = 0;
        }
        else if(currentHealth > 100)
        {
            healthBar.SetHealth(100);
            currentHealth = 100;
        }

        firstFrame = true;





    }//end Update




    /**
     * For this method, we are going to adjust the physics and actually move the character.
     */
    private void FixedUpdate()
    {
        /*
         * Here, we are taking our rigidbody and getting the starting position, adding the vector with respect to the velocity vector in regards
         * to speed (moveVelocity) and adding a time component, which will ensure that the action persists as long as you are holding down the arrow
         * keys.
         */

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

    }//end FixedUpdate

    public void TakeDamage(int damage)
    {
        currentHealth += damage;

        healthBar.SetHealth(currentHealth);

    }//end TakeDamage


    public void GetHungrier(int amt)
    {
        currentFood -= amt;
        foodBar.SetFood(currentFood);
    }

    public void AddHunger(int amt)
    {
        currentFood += amt;
        foodBar.SetFood(currentFood);
    }

    public void AddMoney(int amt)
    {
        gold += amt;
        moneyCount.text = $"{gold}";
        
    }
    public void takeMoney(int amt)
    {
        gold -= amt;
        moneyCount.text = $"{gold}";
        
    }

    public void healthRegen(int food)
    {
        
        if (currentFood >80)
        {
            deltaHealth = 4;
            
        }
        else if (currentFood > 60 && currentFood < 81)
        {
            deltaHealth = 3;
        }
        else if (currentFood > 40 && currentFood < 61)
        {
            deltaHealth = 2;
        }
        else if (currentFood > 20 && currentFood < 41)
        {
            deltaHealth = 1;
        }
        else if (currentFood == 20)
        {
            deltaHealth = 0;
        }
        else if (currentFood > 15 && currentFood < 21)
        {
            deltaHealth = -6;
        }
        else if (currentFood > 10 && currentFood < 16)
        {
            deltaHealth = -12;
        }
        else if (currentFood > 5 && currentFood < 11)
        {
            deltaHealth = -18;
        }
        else if (currentFood < 6)
        {
            deltaHealth = -24;
        }

    }

    public void Victory()
    {
        winScreen.enabled = true;
        Vector2 confettiPosition = new Vector2(transform.position.x, transform.position.y + 4); 
        Instantiate(confetti, confettiPosition, Quaternion.identity);
    }

}