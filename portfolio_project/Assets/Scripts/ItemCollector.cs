using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector // script used for creating a instance of a item collector to store the game values that are important for the player
{
    public int _currentkills = 0;
    public int _currenttreasure = 0;
    public int _currentammo = 3;
    public int _currentkeys = 0;
    //getters and setters below used for assigning and returning the values
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
    public ItemCollector(int kills, int Treasure,int Keys,int Ammo)//constructor used to create instance in the game manager
    {
        _currentkills = kills;
        _currenttreasure = Treasure;
        _currentkeys = Keys;
    }
    //below are methods that can be called to edit the values of the instance using its designation
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

