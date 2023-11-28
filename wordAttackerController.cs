using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;
using System.Collections;
using UnityEngine.Events;

public class wordAttackerController : MonoBehaviour
{
    [SerializeField] private UnityEvent PlayerIsHit;
    
    
    private NavMeshAgent _navMeshAgent;
    private Transform playerPosition;
    [SerializeField] private TextMesh _textMeshChild;
    [SerializeField] private GameObject hitParticles;
    [SerializeField] private GameObject missParticles;
    [SerializeField] private String _attackerLetter;
    //[SerializeField] private Transform [] _spawnTransformLocations;
    [SerializeField] private float _spawnDelay = 12.0f;
    private int _spawnCount;

    /*
    private void OnEnable()
    {
        Debug.Log("Subscribing to event");
        Engine.GameOverEvent += CollisionWithPlayer();
    }

    private void OnDisable()
    {
        Debug.Log("Unsubscribing from event");
        Engine.GameOverEvent -= CollisionWithPlayer();
    }
    */

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
       // _audioSource = GetComponent<AudioSource>();
       //gameObject.SetActive(false);
       //textMeshChild.text = "";
       //   _textMeshChild.text = _attackerLetter;
       _attackerLetter = _textMeshChild.text;
       
       Debug.Log("Awake my attacker letter is " + _attackerLetter);

    

    }

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GetPlayerPosition();
        Debug.Log("Start my attacker letter is " + _attackerLetter);
    }

    private Transform GetPlayerPosition()
    {
        //Transform pos =
        return playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent.enabled)
            _navMeshAgent.SetDestination(SingletonPositionReturner.Instance.transform.position);
        
        //_textMeshChild.transform.rotation = Quaternion.LookRotation( _textMeshChild.transform.position - camera.main.transform.position );
    }

    private void LateUpdate() // Freezing rotation
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
     
    }

    /*public void SetAttackLetter(string setLetter)
    {
        _attackerLetter = setLetter ;
        _textMeshChild.text = _attackerLetter;
        Debug.Log("SetAttackLetter called " + setLetter);

    }*/

    public void WordAttackerActivate(string letterToSet, Transform [] positions)
    {
        gameObject.SetActive(true);
        WhereToSpawn(letterToSet, positions); //TODO IF MATH READ LAST LETTER FOR ANSWER AND POP TO NOT SHOW IT
     
    }

    private  void WhereToSpawn(string letterToSet, Transform [] positions)
    {
        var randomSpawnIndex =  UnityEngine.Random.Range(0, 4);
        //TMP TEST
        randomSpawnIndex = 2;
        Debug.Log("Random Spawn Index : " + randomSpawnIndex);
        gameObject.transform.position = positions[randomSpawnIndex].position;
        //Rotating wordAttackers correctly based on spawnpoint
        if (randomSpawnIndex == 0) 
            _textMeshChild.transform.Rotate(0, -90, 0);
        else if (randomSpawnIndex == 1)
            _textMeshChild.transform.Rotate(0, 90, 0);
        else if (randomSpawnIndex == 2)
            _textMeshChild.transform.Rotate(0, -180, 0);
           // _textMeshChild.transform.rotation =  Quaternion.RotateTowards(transform.rotation,transform, 5);
        _textMeshChild.text = letterToSet;
        _attackerLetter = letterToSet;

        // _textMeshChild.rotation = Quaternion.LookRotation( textMeshTransform.position - Camera.main.transform.position );
    }

  

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter " + other.name);
        if (other.name.Equals("SpherePlayerCollidier"))
        {
            Debug.Log("Collision with player is detected");
            CollisionWithPlayer();
        }
        else
        {
            String compareString = other.GetComponent<FireBall>().myLetter;
            
            bool compare = _attackerLetter.Equals(other.GetComponent<FireBall>().myLetter);
            if (compare.Equals(true))
            {
                Debug.Log("Bullet YES matched");
                AudioManager.Instance.PlayHitAudio();
                Instantiate(hitParticles, gameObject.transform.position, gameObject.transform.rotation);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Bullet NO matched " + " Attacker Letter = " + _attackerLetter + " Bullet Letter = " +
                          compareString);
                AudioManager.Instance.PlayMissAudio();
                Instantiate(missParticles, gameObject.transform.position, gameObject.transform.rotation);
            }

            other.GetComponent<FireBall>().DestroyThisObject();
        }
    }

    private void CollisionWithPlayer()
    {
        PlayerIsHit.Invoke();
    }
}
