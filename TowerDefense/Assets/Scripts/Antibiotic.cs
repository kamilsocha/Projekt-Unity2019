using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibiotic : Turret
{
    private void OnTriggerEnter(Collider other) // when object enters collider
    {
        if(other.tag == "Bacteria")             // if bacteria, else return
        {
            Bacteria _bacteria = other.GetComponent<Bacteria>();
            if (_bacteria != null)
            {
                _bacteria.EnableAntibioticEffect(); // enable effect on bacteria
            }
        }
    }

    private void OnTriggerExit(Collider other)  // when object exits collider
    {
        if (other.tag == "Bacteria")            // if bacteria, else return
        {
            Bacteria _bacteria = other.GetComponent<Bacteria>();   
            if (_bacteria != null)
            {
                _bacteria.DisableAntibioticEffect();    // disable effect on bacteria
            }
        }
    }

}
