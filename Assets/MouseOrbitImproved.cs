using System.Net.Mail;
using UnityEngine;
 
public class MouseOrbitImproved: MonoBehaviour {
 
    // The thing we are looking at
    public Transform target;
    
    // Distance away from the thing we are looking at
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
 
    // Y axis is up/down.
    // When tilting to look up, how far down can we tilt (stops camera going underground)?
    public float yMinLimit = -20f;
    // When tilting to look down, how far up can we go?
    public float yMaxLimit = 80f;
 
    // Closest we can be to the thing we are looking at
    public float distanceMin = .5f;
    // Furthest out we can be from the thing we are looking at
    public float distanceMax = 15f;
 
    private Rigidbody rigidbody;
 
    float x = 0.0f;
    float y = 0.0f;
 
    // Use this for initialization
    void Start () 
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
 
        rigidbody = GetComponent<Rigidbody>();
 
        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }
 
    void LateUpdate () 
    {
        if (!target) return;
        
        // Get mouse input and alter the camera rotation
        x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
 
        y = ClampAngle(y, yMinLimit, yMaxLimit); // Limit up and down movement
 
        Quaternion rotation = Quaternion.Euler(y, x, 0); // 0 here constrains us to no camera roll. It's always vertical.
 
        // Get the change in mouse wheel scroll and alter our zoom distance
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
 
        // Do not allow us to collide with anything??
        RaycastHit hit;
        if (Physics.Linecast (target.position, transform.position, out hit)) 
        {
            distance -=  hit.distance;
        }
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;
 
        // Add the new rotation and position the the transform which will be applied to the target 
        transform.rotation = rotation;
        transform.position = position;
    }

    // Seems to stop the angle from going outside 360 degrees either + or -.
    // Then keeps it between the min and max angle specified.
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}