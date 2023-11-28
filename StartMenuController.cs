using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class StartMenuController : MonoBehaviour
{
 //   delegate void ToggleListenerDelegate(int mode);
 //public event EventHandler OnStartPressed;

 [SerializeField] private UnityEvent startPressed; 
 

 
    //public event ToggleListenerDelegate toggleDelegateEvent;
    private int _selectedDifficulty = 1; // 1 easy , 2 med, 3 hard 
    [SerializeField] private Toggle easy, medium, hard;
    [SerializeField] private Button startButton;
    public bool byPassMenuStartGame = false;

    private void Awake()
    {
     
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (startButton.onClick.AddListener() =>
            
            );*/
        if (byPassMenuStartGame)
            StartButtonPressed();
        
    }

    public void DifficultyChanged(int difficutly)
    {
        Debug.Log("DifficultyChanged from " + _selectedDifficulty + " to " + difficutly);
        _selectedDifficulty = difficutly;
    }

    public void StartButtonPressed()
    {
        Debug.Log("Start Button has been pressed");
        if (_selectedDifficulty == 1)
        startPressed.Invoke();
        else if (_selectedDifficulty == 2)
        {
            
        }
        else
        {
            Debug.Log("Selected difficulty 3");
        }
        
    }

    
    
    
    
    /*
    public void ToggleChanged(bool change)
    {
        Debug.Log("Togglechange called");
    }

    public void MenuToggleSelected(int mode)
    {
        Debug.Log("MenuToggleSelected called" + mode);

        /*switch (mode)
        {
            case 0:
                Debug.Log(" case 0");
                easy.isOn = true;
                medium.isOn = false;
                hard.isOn = false;
                break;
            
            case 1:
                Debug.Log(" case 1");
                easy.isOn = false;
                medium.isOn = true;
                hard.isOn = false;
                break;
            
            case 2:
                Debug.Log(" case 2");
                easy.isOn = false;
                medium.isOn = false;
                hard.isOn = true;
                break;
        }#1#


        /*for (int i = 0; i < toggles.Length; i++)
        {
            if (mode == i)
            {
                if (!toggles[i]._toggleIsOn)
                    toggles[i].IsON(true);
            }
            else
            {
                toggles[i].IsON(false);
            }
        }#1#
      /*foreach (var toggle in toggles)
        {
            Debug.Log("MenuToggleSelected is toggle on ? + " +   toggle._toggleIsOn);
          
            toggle.IsON(false);
          //  toggle.
            
            //toggle._toggleIsOn
        //    toggle.isOn = false;
        }#1#
      
      /*
            toggles[1].isOn = true;#1#
    }*/

}
