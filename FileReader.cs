using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileReader : MonoBehaviour
{
public static FileReader Instance { get; private set; }

    StreamReader reader = new StreamReader("./Assets/test4.txt");
    private String itemString;
    char[] delimiter = {'@'};
    private string[] wordsFromFile;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        itemString = reader.ReadLine(); 
        if (itemString !=null)
        {
        wordsFromFile = itemString.Split(delimiter);
        Debug.Log("testing Reader from FileReader" + wordsFromFile);
        for (int i = 0; i < wordsFromFile.Length; i++)
        {
           Debug.Log(wordsFromFile[i]);
        }
        }
        else
        {
            Debug.Log("Error itemString is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public string[] ReturnWordsFromFile()
   {
       return wordsFromFile;
   }
}
