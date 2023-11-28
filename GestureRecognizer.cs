using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;

public class GestureRecognizer : MonoBehaviour
{
    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public float inputThreshold = 0.1f;
    public Transform movementSource;

    public float newPositionThresholdDistance = 0.05f; // Register position only if distance exceededs this
    public GameObject debugCubePrefab;
    public bool creationMode = true;
    public string newGestureName;

    private List<Gesture> trainingSet = new List<Gesture>(); 
    private bool isMoving = false;
    private List<Vector3> positionList = new List<Vector3>();

    [SerializeField] private Camera vrCamera;

    public float recognitionThreshold = 0.9f;
    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> {}
    public UnityStringEvent OnRecognized;
    
    /*public static GestureRecognizer Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }

    }*/
    
    // Start is called before the first frame update
    void Start()
    {
        //Reading saved gestures
        string[] gestureFile = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (var item in gestureFile)
        {
         trainingSet.Add(GestureIO.ReadGestureFromFile(item));   
        }

    }

    // Update is called once per frame
    void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed,
            inputThreshold);
            
        //start movement
        if (!isMoving && isPressed)
        {
            StartMovement();
            
        }
//Ending Movement
        else if (isMoving && !isPressed)
        {
            EndMovement();
        }
//Updating the Movement
        else if (isMoving && isPressed)
        {
            UpdateMovement();
        }
    }
    private void StartMovement()
    {
        Debug.Log("Start Movement");
        isMoving = true;
        positionList.Clear();
        positionList.Add(movementSource.position);
        if (debugCubePrefab)
        Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);
        //^ Instantiate and Destroy after 3 seconds



    }
    
    private void EndMovement()
    {
        Debug.Log("End Movement");
        isMoving = false;
        
        // Create Gesture From positionList
        Point[] pointArray = new Point[positionList.Count];

        for (int i = 0; i < positionList.Count; i++)
        {
            //Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionList[i]);
            Vector2 screenPoint = vrCamera.WorldToScreenPoint(positionList[i]);
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }
            
        Gesture newGesture = new Gesture(pointArray);//Creating gesture from array

        //Adding a new gesture to training set
        if (creationMode)
        {
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);

            string fileName = Application.persistentDataPath + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointArray, newGestureName, fileName);
        }
        //Recognizing gesture
        else
        {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log("Result name and score " + result.GestureClass + " " + result.Score);// Result name and score

            if (result.Score > recognitionThreshold)
            {
                OnRecognized.Invoke(result.GestureClass); // Invoking OnRecongize event
            }
        }
   
    }

    private void UpdateMovement()
    {
       // Debug.Log("Update Movement");
        Vector3 lastPosition = positionList[positionList.Count - 1];
        
        //If position threshold > than distance between source and last pos
        if (Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance)
        {
            positionList.Add(movementSource.position);
            if (debugCubePrefab)
                Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);
        }
    }
}



    



