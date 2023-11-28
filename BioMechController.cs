using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class BioMechController : MonoBehaviour
{
   // public static event Action<BioMechController> OnMechDestroyed;   
    [SerializeField]private float _attackRange = 1.0f;
    [SerializeField] private int _health = 2;
    
    [SerializeField] private Transform currentDestination;


  [SerializeField]  private bool startMoving = true;

  [SerializeField]  Transform[] _destinations;
    
    private int _currentHealth;
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Rigidbody _rigidbody;

    
    private bool Alive => _currentHealth > 0;

    private void Awake()
    {
        _currentHealth = _health;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
      // _navMeshAgent.enabled = false;
      
      //Loading destinations
      _destinations = DestinationLoader.Instance.LoadDestinations();
      currentDestination = DestinationDecider(_destinations);
      
      startMoving = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Alive)
            return;
        
       // var player = FindObjectOfType<XRRig>();
        //Testing walk point to point

        
        if (startMoving)
        {
            _animator.SetBool("Moving", true);
            _navMeshAgent.SetDestination(currentDestination.position);
            //StartCoroutine(DestinationReached());
            startMoving = false;
        }
        

        //Testing if moving
        if(_rigidbody.velocity.magnitude > 0)
        {
            Debug.Log("BioMech Movement detected");
        }
       
        
        /*if (_navMeshAgent.enabled)
        {   
            Debug.Log("nav mesh enabled");
           // _navMeshAgent.SetDestination(player.transform.position);
            if (startMoving)
                _navMeshAgent.SetDestination(pointB.position);
            else
            {
                StartCoroutine(DestinationReached());
            }
            
        }*/
        
        
        /*var player = FindObjectOfType<PlayerMovement>();
        if (_navMeshAgent.enabled)
            _navMeshAgent.SetDestination(player.transform.position);

        if (Vector3.Distance(transform.position, player.transform.position) < _attackRange)
            Attack();*/
    }

    private Transform DestinationDecider(Transform [] destinations)
    {
        int randomIndex = UnityEngine.Random.Range(0, destinations.Length);

        return destinations[randomIndex];

    }

    public void DestinationTriggerEvent()
    {
     Debug.Log("DestinationTriggerEvent");
     // _animator.SetTrigger("Idle");
     _navMeshAgent.enabled = false;
     _animator.SetBool("Moving", false);

     StartCoroutine(DestinationReached());
     /*if (gameObject.transform.position == currentDestination.position)
     {
         
         Debug.Log("DestinationTriggerEvent Reached");
         // _animator.SetTrigger("Idle");
         _navMeshAgent.enabled = false;
         _animator.SetBool("Moving", false);

         StartCoroutine(DestinationReached());
     }
     else
     {
         {
             Debug.Log("DestinationTriggerEvent NOT correct Reached" + gameObject.transform);
         }
     }*/
    }
    
    private IEnumerator DestinationReached()
    {
       
        Debug.Log("DestinationReached coroutine");
        yield return new WaitForSeconds(20.0f);
        ResetDestination();
    }

    private void ResetDestination()
    {

        Transform lastDestination = currentDestination;
        while (currentDestination == lastDestination)
        {
            currentDestination = DestinationDecider(_destinations);
        }

        _navMeshAgent.enabled = true;
            _animator.SetBool("Moving" , true);
        //_animator.SetTrigger("StartMoving");
        startMoving = true;
       //_animator.SetBool("Moving", true);
      
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        /*var blasterShot = collision.collider.GetComponent<BlasterShot>();
        if (blasterShot != null)
        {
            _currentHealth--;
            if (_currentHealth <= 0)
                Die();
            else
                TakeHit();
        }*/
        Debug.Log("OnCollisionEnter Event called");
    }

    private void TakeHit()
    {
        _navMeshAgent.enabled = false;
        _animator.SetTrigger("Hit");
    }

    private void Die()
    {
        /*GetComponent<Collider>().enabled = false;
        _navMeshAgent.enabled = false;
        _animator.SetTrigger("Death");
        OnMechDestroyed?.Invoke(this);
        Destroy(gameObject, 5.0f);*/
        
    }

    private void Attack()
    {
        if (Alive)
        {
            _animator.SetTrigger("Attack");
            _navMeshAgent.enabled = false;
        }
    }

    public void StartWalking()
    {
        _navMeshAgent.enabled = true;
        _animator.SetBool("Moving", true);
    }

    public void ScreamAnimation()
    {
        _animator.SetTrigger("Scream");
    }

    #region Animation Callbacks

    //Animation Callback region

   
    void AttackComplete()
    {
        
    }

    void AttackHit()
    {
        Debug.Log("Killed player");
       // SceneManager.LoadScene(0);
    }

    void HitComplete()
    {
        if (Alive)
            _navMeshAgent.enabled = true;
    }

    #endregion Animation Callbacks

}
