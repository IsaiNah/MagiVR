using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureText : MonoBehaviour
{
    private TextMesh _textMesh;
    [SerializeField] private String symbolCompare;

    [SerializeField] public String spawnPositionNumber;
   // [SerializeField] private String symbolToMatch;
     void Awake()
     {
         _textMesh = GetComponent<TextMesh>();
     }

    // Start is called before the first frame update
    void Start()
    {
       // _textMesh.text = symbolCompare[0].ToString();
 
        

    }

    // Update is called once per frame
    void Update()
    {
       
            
      
    }

    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }

    public void ChangeGestureText(string text)
    {
        
        Debug.Log("ChangeGestureText "+ text);
        
        if (text.Equals( _textMesh.text))
        {
            Debug.Log("It's a match");
        }
        else
        {
            Debug.Log("No match");
        }
        
        _textMesh.text = text;
    }
}
