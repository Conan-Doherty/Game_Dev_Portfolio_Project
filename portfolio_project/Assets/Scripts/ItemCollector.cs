using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector
{
    public int _currentkills = 0;
    public int _currenttreasure = 0;
    public int _currentammo = 3;
    public int Kills
    {
        get { return _currentkills; }
        set { _currentkills = value; }
    }
    public int Treasure
    {
        get { return _currenttreasure; }
        set { _currenttreasure = value; }
    }
    public int Ammo
    {
        get { return _currentammo; }
        set { _currentammo = value; }
    }
    public ItemCollector(int kills, int Treasure)
    {
        _currentkills = kills;
        _currenttreasure = Treasure;
    }
    public void addkill()
    {
        _currentkills++;
    }
    public void addtreasure()
    {
        _currenttreasure++;
    }
    public void addammo()
    {
        _currentammo = _currentammo + 1;
    }
    public void removeammo()
    {
        _currentammo--;
    }
}

