/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public string[] dialog;
    public bool playerInRange;
    private int i;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact") && playerInRange)
        {
            if (dialogBox.activeInHierarchy==false)
            {
                dialogBox.SetActive(true);
                i = 0;
                dialogText.text = dialog[i];
               
            }
            else if(dialogBox.activeInHierarchy && i < dialog.Length-1)
            {
                i++;
                dialogText.text = dialog[i];
            }
            else
            {
                dialogBox.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Character")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Character")
        {
            playerInRange = false;
        }
    }

    public void button1()
    {

    }
}
*/