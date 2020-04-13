using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxFood(int amt)
    {
        slider.maxValue = amt;
        slider.value = amt;

        fill.color = gradient.Evaluate(1f);
    }//end SetMaxFood

    public void SetFood(int amt)
    {

        slider.value = amt;

        fill.color = gradient.Evaluate(slider.normalizedValue);

    }//end SetFood
}
