using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestinationReached : MonoBehaviour
{
    //[SerializeField] UnityEvent destinationReached;
    [SerializeField] private BioMechController bioMechController;
  
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ontrigger Enter with " + other.name);
        bioMechController.DestinationTriggerEvent();
      //  destinationReached.Invoke();
    }
    
    
}
