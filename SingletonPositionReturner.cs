using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPositionReturner : MonoBehaviour
{
    public static SingletonPositionReturner Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }

    }

   // public Transform GetPlayerPosition() => gameObject.transform;
}
