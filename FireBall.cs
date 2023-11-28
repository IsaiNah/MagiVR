using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private TextMesh _textMesh;
    [SerializeField] private float _shotPower = 50.0f;
    public String myLetter { get; private set; } //getter for collision 


    private void Awake()
    {
      //  myLetter = _textMesh.text;De.//.De..De().othother.otherother.Get
      gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * _shotPower);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletDidNotHit());
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position.Normalize();
        
    }

    

    private IEnumerator BulletDidNotHit()
    {
        yield return new WaitForSeconds(30.0f);
        Destroy(gameObject);
        //For pooling 
        //gameObject.SetActive(false);
       // SimpleShoot.Instance.AddBulletToPool(this);
    }

    public void SetBulletLetter(string letter)
    {
        myLetter = letter;
        _textMesh.text = letter;
        Debug.Log("TextBall my letter is " + myLetter);
    }

    public void DestroyThisObject()
    {
        Destroy(gameObject);
    }
}
