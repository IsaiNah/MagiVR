using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private WordPickedAttackerController wordPickedAttackerController;
   [SerializeField] private StartMenuController startMenuController;
   [SerializeField] private WinLoseMenuController  winLoseMenuController;

   
    //Player collision event game over
 //   public delegate void GameOverEvent();

   // public static event GameOverEvent GameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EngineStartWordAttackers()
    {
        Debug.Log("EngineStartWordAttackers called");
        wordPickedAttackerController.gameObject.SetActive(true);
    }
    
    public void EngineStopWordAttackers()
    {
        wordPickedAttackerController.enabled = false;
    }

    public void EngineRestartWordAttackers()
    {
     wordPickedAttackerController.RestartWordAttackers();   
    }
    
    public void EngineStartMenuController()
    {
        startMenuController.enabled = true;
    }
    
    public void EngineStopMenuController()
    {
        Debug.Log("EngineStopMenuController called");
        startMenuController.gameObject.SetActive(false);
    }

    public void EnginePlayerIsHit()
    {
        Debug.Log("Engine player is hit");
        winLoseMenuController.gameObject.SetActive(true);
        wordPickedAttackerController.gameObject.SetActive(false);
    }

    
}
