using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationLoader : MonoBehaviour
{
    public static DestinationLoader Instance { get; private set; }

    [SerializeField] private Transform[] _loadDestinations;
// Prevents Multiple Instances
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    public Transform[] LoadDestinations()
    {

        return _loadDestinations;
    }
}
