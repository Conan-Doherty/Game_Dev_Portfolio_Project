using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    Slider _healthslider;

    private void Start()
    {
        _healthslider = GetComponent<Slider>();
    }
    public void setmaxhealth(int maxhealth)
    {
        _healthslider.maxValue = maxhealth;
        _healthslider.value = maxhealth;
    }
    public void sethealth(int health)
    {
        _healthslider.value = health;
    }
}
