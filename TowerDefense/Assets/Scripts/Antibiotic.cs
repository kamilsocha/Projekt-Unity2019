using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibiotic : Turret
{
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bacteria")
        {
            Bacteria _bacteria = other.GetComponent<Bacteria>();
            if (_bacteria != null)
            {
                _bacteria.EnableAntibioticEffect();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bacteria")
        {
            Bacteria _bacteria = other.GetComponent<Bacteria>();
            if (_bacteria != null)
            {
                _bacteria.DisableAntibioticEffect();
            }
        }
    }

}
