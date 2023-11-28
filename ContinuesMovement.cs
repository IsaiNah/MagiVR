using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuesMovement : MonoBehaviour
{
    public static ContinuesMovement Instance { get; private set; }

    public float speed = 1.0f;
  //  public XRNode inputSource;
    public float gravity = -9.81f; // Approx Earth Gravity
    public LayerMask groundLayer;
    public float additionaHeight = 0.2f;

    private float fallingSpeed;
    private XRRig rig;
    private Vector2 inputAxis;
    private CharacterController character;





    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject); // Prevents multiples 
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();


    }

    // Update is called once per frame
    void Update()
    {
   

        // Will Call Input Listener Instance to get inputAxis
        inputAxis = InputListener.Instance.LeftStickInput();
       // Debug.Log(inputAxis);


        //Test to delete
       /* Debug.Log(InputListener.Instance.RightStickInput());
        Debug.Log(InputListener.Instance.LeftTrackPad());
        Debug.Log(InputListener.Instance.RightTrackPad());
        Debug.Log(InputListener.Instance.LeftDeviceTrigger());
        Debug.Log(InputListener.Instance.RightDeviceTrigger());
        Debug.Log(InputListener.Instance.LeftDeviceGrip());
        Debug.Log(InputListener.Instance.RightDeviceGrip());
        Debug.Log(InputListener.Instance.LeftDevicePositionTest());
        Debug.Log(InputListener.Instance.RightDevicePositionTest());*/
    }

    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionaHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

         character.Move(direction * Time.fixedDeltaTime * speed);
   
        //Apply gravity
        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            fallingSpeed = 0;
         //   Debug.Log("Grounded");
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
           // Debug.Log("NOT Grounded");
        }
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

   public bool CheckIfGrounded()
    {
        //tells us if on ground
        Vector3 rayStart = transform.TransformPoint(character.center);
        // float rayLength = character.center.y + 0.01f;SAFE SAVE
        float rayLength = character.center.y + 1.0f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer); ;
       // Debug.DrawRay(rayStart, Vector3.down, Color.red, rayLength);
       // Debug.DrawRay(transformPosBottom.position,  .down, Color.blue);

        return hasHit;
    }

}
