using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debtCollector : MonoBehaviour
{
    private GameObject Player;
    private Vector2 movement;
    public float speed;
    public float distance;
    public float outOfRange;
    public double damageModified;
    System.Random rnd = new System.Random();

    //triggers
    private bool rentCollected;
    private bool rentChase;
    private GameObject renter;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Character");
        rentCollected = false;
        rentChase = false;
        renter = GameObject.Find("renter");
    }

    // Update is called once per frame
    void Update()
    {
        if (rentChase == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, Player.transform.position) > outOfRange)
            {
                rentChase = false;

            }


        }

        if (Vector2.Distance(transform.position, Player.transform.position) < distance)
        {


            if (renter.GetComponent<Renter>().lateDue > 0 && rentCollected == true)
            {
                rentChase = true;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject Warrick = collision.gameObject;

        if (Warrick.gameObject.name == "Character")
        {
            Controller script = Warrick.GetComponent<Controller>();
            if (rentChase == true)
            {
                rentChase = false;
                script.TakeDamage(rentDamage());
                rentCollected = true;

            }
        }
    }

    private int rentDamage()
    {
        int damage = (int)System.Math.Ceiling((double)renter.GetComponent<Renter>().lateDue * (.5));

        return damage;
    }
}
