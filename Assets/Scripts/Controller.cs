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

    public int maxHealth = 100;

    public int currentHealth;
    public HealthBarScript healthBar;

    public int maxFood = 100;
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



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Initializes the physics engine of your rigid body to the sprite
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentFood = maxFood;
        foodBar.SetMaxFood(maxFood);

        time = GameObject.Find("Clock").AddComponent<TimeTracker>();
        previousHours = "06";
        sleep = false;
        gold = 100;
        moneyCount.text = $"{gold}";

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

        Vector2 inputMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0)
        {
            animator.SetFloat("IdleHorizontal", inputMovement.x);
            animator.SetFloat("IdleVertical", inputMovement.y);
            animator.SetFloat("Horizontal", inputMovement.x);
            animator.SetFloat("Vertical", inputMovement.y);
        }
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0)
        {

            animator.SetFloat("IdleHorizontal", inputMovement.x);
            animator.SetFloat("IdleVertical", inputMovement.y);
            animator.SetFloat("Horizontal", inputMovement.x);
            animator.SetFloat("Vertical", inputMovement.y);
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > 0)
        {
            animator.SetFloat("IdleHorizontal", inputMovement.x);
            animator.SetFloat("IdleVertical", inputMovement.y);
            animator.SetFloat("Horizontal", inputMovement.x);
            animator.SetFloat("Vertical", inputMovement.y);
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") < 0)
        {
            animator.SetFloat("IdleHorizontal", inputMovement.x);
            animator.SetFloat("IdleVertical", inputMovement.y);
            animator.SetFloat("Horizontal", inputMovement.x);
            animator.SetFloat("Vertical", inputMovement.y);
        }
        else if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetHungrier(2);
        }

        

        if(previousHours != time.GetHours() && sleep == false)
        {
            GetHungrier(2);
            previousHours = time.GetHours();
        }


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
        currentHealth -= damage;

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

}