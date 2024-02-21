using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DashBar : MonoBehaviour
{
    // Start is called before the first frame update
    
    Slider _dashslider;//reference to the slider used onscreen for the dash mechanic

    private void Start()
    {
        _dashslider = GetComponent<Slider>();//grabs the slider
    }
    public void setmaxdashamount(int maxdash)//method used to set the max values of the slider 
    {
        _dashslider.maxValue = maxdash;
        _dashslider.value = maxdash;
    }
    public void setdashamount(int dash)//sets slider value during play
    {
        _dashslider.value = dash;
    }
}
