using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DashBar : MonoBehaviour
{
    // Start is called before the first frame update
    Slider _dashslider;

    private void Start()
    {
        _dashslider = GetComponent<Slider>();
    }
    public void setmaxdashamount(int maxdash)
    {
        _dashslider.maxValue = maxdash;
        _dashslider.value = maxdash;
    }
    public void setdashamount(int dash)
    {
        _dashslider.value = dash;
    }
}
