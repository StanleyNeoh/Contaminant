using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider HealthSlider;

    public void SetHealth(float Health) {
        HealthSlider.value = Health;
    }

    public void SetMaxHealth(float MaxHealth) {
        HealthSlider.maxValue = MaxHealth;
    }
}
