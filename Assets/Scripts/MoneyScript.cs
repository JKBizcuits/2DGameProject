using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyScript : MonoBehaviour
{


    public TextMeshProUGUI text;

    int money;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        money = 0;

    }

    public void AddMoney(int amt)
    {
        money += amt;
        text.text = $"{money}";
        System.Console.WriteLine("Money Added");
    }
}
