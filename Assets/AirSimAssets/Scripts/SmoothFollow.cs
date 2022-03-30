using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public bool shouldRotate = true;

    // The target we are following
    public Transform target;

    private float wantedRotationAngle;
    private float currentRotationAngle;

    private float xRotationOffset = 0;
    private float yRotationOffset = 0;


    private Quaternion currentRotation;

    private Vector3 offsetPostion, finalPosition;

    private void Start()
    {
        offsetPostion = new Vector3(0, 5, -10);
    }

    private void LateUpdate()
    {
        if (!target)
        {
            return;
        }

        // Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel");
        if (0 != ScrollWheelChange)
        {
            offsetPostion = offsetPostion + new Vector3(0, 0, ScrollWheelChange * 10f);
        }
        if (Input.GetKey("w"))
        {
            xRotationOffset = xRotationOffset + 1;
        }
        if (Input.GetKey("s"))
        {
            xRotationOffset = xRotationOffset - 1;
        }
        if (Input.GetKey("a"))
        {
            yRotationOffset = yRotationOffset + 1;
        }
        if (Input.GetKey("d"))
        {
            yRotationOffset = yRotationOffset - 1;
        }
        if (Input.GetKey("r"))
        {
            yRotationOffset = 0;
            xRotationOffset = 0;
            offsetPostion = new Vector3(0, 5, -10);
        }




        // Calculate the current rotation angles
        wantedRotationAngle = target.eulerAngles.y + yRotationOffset;
        currentRotationAngle = transform.eulerAngles.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, 0.3f);

        // Convert the angle into a rotation
        currentRotation = Quaternion.Euler(xRotationOffset, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position + (currentRotation * offsetPostion);

        // Always look at the target
        transform.LookAt(target);
    }
}