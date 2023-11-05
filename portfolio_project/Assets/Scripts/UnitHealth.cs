using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth
{
    //Fields
    int _currentHealth;
    int _currentMaxHealth;

    //properties
    public int Health
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }
    public int MaxHealth
    {
        get { return _currentMaxHealth; }
        set { _currentMaxHealth = value; }
    }
    //contructor
    public UnitHealth(int health, int maxhealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxhealth;
    }
    //Methods
    public void dmgUnit(int dmgAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= dmgAmount;
        }
    }
    public void HealUnit(int healAmount)
    {
        if (_currentHealth < _currentMaxHealth)
        {
            _currentHealth += healAmount;
        }
        else
        {
            Debug.Log("max health reached");
        }
    }
}
