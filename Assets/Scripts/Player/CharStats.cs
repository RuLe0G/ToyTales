using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [SerializeField]
    private int HP;
    [SerializeField]
    private int maxHP;

    public event EventHandler onHpChanged;

    public void Awake()
    {
        HP = maxHP;
    }
    public int getHealth()
    {
        return HP;
    }
    public bool isMaxHp()
    { 
        if (HP == maxHP)
            return true;
        return false;
    }

    public void takeDamage(int incomingDamage)
    { 
        HP -= incomingDamage;
        if (HP <= 0)
        {
            death();
        }
        if (onHpChanged != null)
        {
            onHpChanged(this, EventArgs.Empty);
        }
    } 

    public void takeHeal(int incomingHeal)
    { 
        HP += incomingHeal;
        if (HP > maxHP)
            HP = maxHP;

        if (onHpChanged != null)
        {
            onHpChanged(this, EventArgs.Empty);
        }
    }

    public void death()
    {
        Debug.Log("ялепрэ " + gameObject.name);
    }
}
