using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : Turret
{
    public float slowRate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy _enemy = other.GetComponent<Enemy>();
            if (_enemy != null)
            {
                _enemy.Slow(slowRate);
            }
        }
        else if (other.tag == "Bacteria")
        {
            Bacteria _bacteria = other.GetComponent<Bacteria>();
            if (_bacteria != null)
            {
                _bacteria.Slow(slowRate);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy _enemy = other.GetComponent<Enemy>();
            if (_enemy != null)
            {
                _enemy.EndSlow();
            }
        }
        else if (other.tag == "Bacteria")
        {
            Bacteria _bacteria = other.GetComponent<Bacteria>();
            if (_bacteria != null)
            {
                _bacteria.EndSlow();
            }
        }
    }

}
