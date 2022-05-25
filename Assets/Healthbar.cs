using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    
    public Slider slider;

    public void MaxHealth(float Health)
    {
        slider.maxValue = Health;
        slider.value = Health;
        Debug.Log(Health);
    }

    public void Health(float Health)
    {
        slider.value = Health;
        Debug.Log(Health);
    }
}
