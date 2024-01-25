using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector
{
    public int _currentkills = 0;
    public int _currenttreasure = 0;
    public int _currentammo = 3;
    public int _currentkeys = 0;
    public int Keys
    {
        get { return _currentkeys; }
        set { _currentkeys = value; }
    }
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
    public ItemCollector(int kills, int Treasure,int Keys)
    {
        _currentkills = kills;
        _currenttreasure = Treasure;
        _currentkeys = Keys;
    }
    public void addkey()
    {
        _currentkeys++;
    }
    public void removekey() 
    { 
        _currentkeys--;
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

