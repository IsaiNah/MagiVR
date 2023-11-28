using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;


    
    void Start()
    {
        TryInitialize();

    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
       
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
       // Debug.Log("Test");
/*        foreach (var item in devices)
        {
            Debug.Log("Its workings Item Name : " + item.name + " Item Characteristics : " + item.characteristics);
            Debug.Log(item.name + item.characteristics);
        }*/

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefabController = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefabController)
            {
                Debug.Log("Found controller matching device name. Will use this controller");
                spawnedController = Instantiate(prefabController, transform);
            }
            else
            {
                Debug.Log("Did not find controller matching device name. Will use another controller");
                spawnedController = Instantiate(controllerPrefabs[5], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            // Debug.Log("Updating Hand Animation Trigger" );
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
         //   Debug.Log("Resetting Hand Animation");
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
          //  Debug.Log("Gripping" + gripValue);
            handAnimator.SetFloat("Grip", triggerValue);
        }
        else
        {
          //  Debug.Log("Gripping resetting value");
            handAnimator.SetFloat("Grip", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
/*            Debug.Log("Target Device not valid");*/
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
           /* Debug.Log("Target Valid Continue");*/
     
/*
            if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
            {
                Debug.Log("Trigger Pressed");
            }

            if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
            {
                Debug.Log("Touchpad Pressed");
            }*/
        }
    }
}
