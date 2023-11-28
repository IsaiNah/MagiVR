using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class InputListener : MonoBehaviour
{
    public static InputListener Instance { get; private set; }

    public XRNode leftHandInputSource, rightHandInputSource;
    private Vector2 leftStickInputAxis, rightStickInputAxis, leftTrackPad, rightTrackPad;
    private Vector3 leftDevicePositionTest, rightDevicePositionTest;
    private float leftDeviceTrigger, rightDeviceTrigger, leftDeviceGrip, rightDeviceGrip;
    private bool rightDeviceMainButton, leftDeviceMainButton, rightDeviceMenuButton;

    // Prevents Multiple Instances
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputDevice leftDevice = InputDevices.GetDeviceAtXRNode(leftHandInputSource);
        InputDevice rightDevice = InputDevices.GetDeviceAtXRNode(rightHandInputSource);

        //Sticks
        leftDevice.TryGetFeatureValue(CommonUsages.secondary2DAxis, out leftStickInputAxis);
        rightDevice.TryGetFeatureValue(CommonUsages.secondary2DAxis, out rightStickInputAxis);

        //Touchpad
        leftDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftTrackPad);
        rightDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightTrackPad);


        //Position
        leftDevice.TryGetFeatureValue(CommonUsages.devicePosition, out leftDevicePositionTest);
        rightDevice.TryGetFeatureValue(CommonUsages.devicePosition, out rightDevicePositionTest);

        //Trigger
        leftDevice.TryGetFeatureValue(CommonUsages.trigger, out leftDeviceTrigger);
        rightDevice.TryGetFeatureValue(CommonUsages.trigger, out rightDeviceTrigger);

        //Grip
        leftDevice.TryGetFeatureValue(CommonUsages.grip, out leftDeviceGrip);
        rightDevice.TryGetFeatureValue(CommonUsages.grip, out rightDeviceGrip);

        //Main Button
        leftDevice.TryGetFeatureValue(CommonUsages.primaryButton, out leftDeviceMainButton);
        rightDevice.TryGetFeatureValue(CommonUsages.primaryButton, out rightDeviceMainButton);

        //TEST FOR INPUT
        rightDevice.TryGetFeatureValue(CommonUsages.menuButton, out rightDeviceMenuButton);
    }

    //public getters

    //Sticks
    public Vector2 LeftStickInput()
    {
        return leftStickInputAxis;
    }
    public Vector2 RightStickInput()
    {
        return rightStickInputAxis;
    }

    //Touchpad
    public Vector2 LeftTrackPad()
    {
        return leftTrackPad;
    }
    public Vector2 RightTrackPad()
    {
        return rightTrackPad;
    }

    //Trigger
    public float LeftDeviceTrigger()
    {
        return leftDeviceTrigger;
    }
    public float RightDeviceTrigger()
    {
        return rightDeviceTrigger;
    }

    //Grip
    public float LeftDeviceGrip()
    {
        return leftDeviceGrip;
    }
    public float RightDeviceGrip()
    {
        return rightDeviceGrip;
    }

    //Position
    public Vector2 LeftDevicePositionTest()
    {
        return leftDevicePositionTest;
    }
    public Vector2 RightDevicePositionTest()
    {
        return rightDevicePositionTest;
    }

    //Buttons
    public bool LeftDeviceMainButton()
    {
        return leftDeviceMainButton;
    }
    public bool RightDeviceMainButton()
    {
        return rightDeviceMainButton;
    }

    public bool RightDeviceMenuButton()
    {
        return rightDeviceMenuButton;
    }
}
