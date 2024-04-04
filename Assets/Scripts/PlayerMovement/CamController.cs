using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public bool useOffsetVal;

    public float rotSpeed;

    public Transform pivot;

    public Transform camPivotTarget;


    public AshPC player;

    

    private const float YAngleMin = -12.0f;
    private const float YAngleMax = 50.0f;


    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetVal)
        {
            offset = camPivotTarget.position - transform.position;
        }
        player = GameObject.Find("Character").GetComponent<AshPC>();

        pivot.transform.position = camPivotTarget.transform.position;
        pivot.transform.parent = camPivotTarget.transform;
        Cursor.lockState = CursorLockMode.Locked;
        
        

    }

    

    // Update is called once per frame
    void Update()
    {
        // get x pos of mouse to rotate the target
        if (!AshPC.isPaused && !AshPC.hasDied)
        {
            float horizontal = Input.GetAxis("Mouse X") * rotSpeed;
            target.Rotate(0, horizontal, 0);

            //get y pos of mouse to rotate the pivot
            float vertical = Input.GetAxis("Mouse Y") * rotSpeed;
            //vertical = Mathf.Clamp(vertical, YAngleMin, YAngleMax);
            pivot.Rotate(-vertical, 0f, 0);

            //limit vertical cam rotation
            if (pivot.rotation.eulerAngles.x > 50 && pivot.rotation.eulerAngles.x < 180)
            {
                pivot.rotation = Quaternion.Euler(50f, 0f, 0f);
            }
            if (pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 285)
            {
                pivot.rotation = Quaternion.Euler(285f, 0f, 0f);
            }


            //move the camera based on current rotation of the target and the original offset
            float desiredYAngle = target.eulerAngles.y;
            float desiredXAngle = pivot.eulerAngles.x;


            Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);

            transform.position = camPivotTarget.position - (rotation * offset);


            //transform.position = target.position - offset;

            if (transform.position.y < target.position.y - .5f)
            {
                transform.position = new Vector3(transform.position.x, (target.position.y - .5f), transform.position.z);
            }

            if (transform)
                transform.LookAt(camPivotTarget);
        }
    }

    public void SetRotSpeed(float value)
    {
        rotSpeed = value;
    }


}
