using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinLoseMenuController : MonoBehaviour
{
    [SerializeField] private UnityEvent _onTryAgainPressed;
    
    
    [SerializeField] private Button _tryAgain;


    public void OnTryAgain()
    {
        _onTryAgainPressed.Invoke();
        gameObject.SetActive(false);
    }
    
}
