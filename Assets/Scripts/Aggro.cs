using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    public bool aggro = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            aggro = true;    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        aggro = false;
    }
}
