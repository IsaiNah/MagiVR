using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;


public class WordPickedAttackerController : MonoBehaviour
{

    [SerializeField]
    private wordAttackerController [] _wordAttackerControllers;
    
     
     private String _wordPicked;
     private String[] _wordsFromFile;

    private List<Char> _wordLetters = new List<char>();
    [SerializeField] private Transform[] _spawnPositions;

    public bool startWordAttackers = false;
    public bool reStartWordAttackers = false;

    public bool startMathAttackers = false;
    // Start is called before the first frame update
    void Start()
    {
        //Reading words from file into local script array
        _wordsFromFile = FileReader.Instance.ReturnWordsFromFile();
        //_wordPicked = "HELLO";
        //Using Randomizer to select random word from array
       
        
        
      
    }
    
    // Update is called once per frame
   void Update()
   {

       if (startWordAttackers)
       {
           startWordAttackers = false;
           StartWordAttackers();
           
       }

       if (startMathAttackers)
       {
           startMathAttackers = false;
           StartMathAttackers();
       }
   }

    private IEnumerator StartWordAttackers(int i,char c, Transform[] spawnPositions)
    {
        yield return new WaitForSeconds(5.0f + i);
        _wordAttackerControllers[i].WordAttackerActivate(_wordPicked[i].ToString(), _spawnPositions);
    }

    private string WordPickRandomizer()
    {
        //int test = Random.Range(0, 1);
        return _wordsFromFile[Random.Range(0, _wordsFromFile.Length)];
    }

    private void StartWordAttackers()
    {
        Debug.Log("StartWordAttackers");
        _wordPicked = WordPickRandomizer();

        for (int i = 0; i < _wordPicked.Length; i++)
        {
            // Debug.Log(" i = " + i);
            StartCoroutine(StartWordAttackers(i, _wordPicked[i], _spawnPositions));
      
        }
    }

    private void StartMathAttackers()
    {
        Debug.Log("StartMathAttackers");
        _wordPicked = WordPickRandomizer();
        
      _wordAttackerControllers[0].WordAttackerActivate(_wordPicked, _spawnPositions);

    }

    public void RestartWordAttackers()
    {
        /*foreach (var wordAttackController in _wordAttackerControllers)
        {
         if wordAttackController.   
        }*/
       // startWordAttackers = true;
       StartWordAttackers();
    }
   
}
