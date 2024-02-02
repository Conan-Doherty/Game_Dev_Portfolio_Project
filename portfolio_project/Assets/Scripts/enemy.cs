using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy
{
    public int _currentdmg = 0;
    public int _currenthp = 0;
    public int _currentspeed = 3;
    public int _currentfiredelay = 0;
    public int Dmg
    {
        get { return _currentdmg; }
        set { _currentdmg = value; }
    }
    public int HP
    {
        get { return _currenthp; }
        set { _currenthp = value; }
    }
    public int Speed
    {
        get { return _currentspeed; }
        set { _currentspeed = value; }
    }
    public int Firedelay
    {
        get { return _currentfiredelay; }
        set { _currentfiredelay = value; }
    }
    public enemy(int Dmg, int HP, int Speed, int Firedelay)
    {
        _currentdmg = Dmg;
        _currenthp = HP;
        _currentspeed = Speed;
        _currentfiredelay = Firedelay;
    }
}
