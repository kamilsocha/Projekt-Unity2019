using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : Turret
{
    public float slowRate;  // x/100 of enemy speed after effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")   // in case of virus
        {
            Enemy _enemy = other.GetComponent<Enemy>();
            if (_enemy != null)
            {
                _enemy.Slow(slowRate);  // put effect
            }
        }
        else if (other.tag == "Bacteria")   // in case of bacteria
        {
            Bacteria _bacteria = other.GetComponent<Bacteria>();
            if (_bacteria != null)
            {
                _bacteria.Slow(slowRate);   // put effect
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")   // in case of virus
        {
            Enemy _enemy = other.GetComponent<Enemy>();
            if (_enemy != null)
            {
                _enemy.EndSlow(); // restore start speed
            }
        }
        else if (other.tag == "Bacteria")   // in case of bacteria
        {
            Bacteria _bacteria = other.GetComponent<Bacteria>();
            if (_bacteria != null)
            {
                _bacteria.EndSlow(); //restore start speed
            }
        }
    }

}
