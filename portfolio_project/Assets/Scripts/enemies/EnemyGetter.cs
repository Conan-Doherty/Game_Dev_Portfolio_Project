using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyGetter // Script Purpose: To have all of the stats of the enemies in one place so it's easy to tweak this and have all of them change
{

    public LayerMask _whatIsGround, _WhatIsPlayer;

    public int _currentEnemies; // input through unity editor






    // It's worth noting these are the base values, where needed, the stats should be able to change dynamically (I hope).
    public float walkSpeed { get; set; } // rate at which the enemy walks
    public float attackSpeed { get; set; } // rate at which the enemy attacks
    public float bulletSpeed { get; set; } // rate at which the bullet moves
    public float sightRange { get; set; } // range in which the enemy can see the player
    
    public bool willChase { get; set; } // weird name but i don't want to change it now, it basically decides whether or not the enemy can move or not

    public LayerMask whatIsGround { get; set; }
    public LayerMask whatIsPlayer { get; set; }





    public EnemyGetter(int Choice) // this will automatically assign values to each enemy by taking their type "choice" which is in their unique script
    {
        
        whatIsGround = _whatIsGround;
        whatIsPlayer = _WhatIsPlayer;

        switch (Choice)
        {
            case 0: //Grunt
                walkSpeed = 11;
                attackSpeed = 2;
                bulletSpeed = 10;
                sightRange = 8;
                willChase = true;
                break;

            case 1: //Burster
                walkSpeed = 9;
                attackSpeed = 5;
                bulletSpeed = 8;
                sightRange = 10;
                willChase = true;
                break;

            case 2: //Turret
                walkSpeed = 0;
                attackSpeed = 4;
                bulletSpeed = 15;
                sightRange = 15;
                willChase = false;
                break;

            case 3: //Sniper
                walkSpeed = 5;
                attackSpeed = 1;
                bulletSpeed = 10;
                sightRange = 30;
                willChase = true;
                break;

        }
        
    }
}
