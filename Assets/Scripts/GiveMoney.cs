using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMoney : MonoBehaviour
{
    public int money;

    private CharacterMoney characterMoney;

    // Start is called before the first frame update
    void Start()
    {
        characterMoney = GameObject.FindGameObjectWithTag("Player" ).GetComponent<CharacterMoney>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //AddMoney(money);
            Destroy(gameObject);
        }
    }

    /*public void AddMoney(int amt)
    {
        foreach(Transform child in transform)
        {
            characterMoney.moneyBag.GetComponent<MoneyScript>().AddMoney(amt);

        }
    }*/
}
