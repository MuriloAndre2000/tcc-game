using UnityEngine;
using System.IO;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // Reference to the character's transform
    public Vector3 offset = new Vector3(0f, 14f, 0f); // Offset from the character's position

    public float smoothSpeed = 3f; // Speed at which the camera follows the character
    public float rotationSpeed = 0f; // Speed at which the camera rotates

    void Start(){
        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate the desired position and rotation of the camera
        
        Vector3 desiredPosition = target.position + offset;
        //Debug.Log(desiredPosition);
        //Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);

        // Smoothly move the camera towards the desired position and rotation
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        
        //transform.position = desiredPosition;
    }
}
