using UnityEngine;
 
public class MouseOrbitImproved: MonoBehaviour {
    // The thing we are looking at
    public Transform Target;
    
    // Distance away from the thing we are looking at
    public float Distance = 5.0f;
    public float YSpeed = 120.0f;
 
    // Y axis is up/down.
    // When tilting to look up, how far down can we tilt (stops camera going underground)?
    public float YMinLimit = -20f;
    // When tilting to look down, how far up can we go?
    public float YMaxLimit = 80f;
 
    // Closest we can be to the thing we are looking at
    public float DistanceMin = .5f;
    // Furthest out we can be from the thing we are looking at
    public float DistanceMax = 15f;
 
    private Rigidbody _rigidbody;
 
    private float _y;
 
    // Use this for initialization
    void Start () 
    {
        Vector3 angles = transform.eulerAngles;
        // Not sure why the original script swaps x and y. Perhaps mouse vs Euler.
        _y = angles.x;
 
        _rigidbody = GetComponent<Rigidbody>();
 
        // Make the rigid body not change rotation
        if (_rigidbody != null)
        {
            _rigidbody.freezeRotation = true; // Physics will not rotate the thing. Only this script.
        }
    }
 
    void LateUpdate () 
    {
        if (!Target) return;
        
        _y -= Input.GetAxis("Mouse Y") * YSpeed * 0.02f;
        _y = ClampAngle(_y, YMinLimit, YMaxLimit); // Limit up and down movement
 
        // Takes degrees as arguments.
        // Mouse Y != Euler Y
        // Camera rotates around its target, not it's own position in space.
        Quaternion rotation = Quaternion.Euler(_y, Target.eulerAngles.y, 0); // 0 here constrains us to no camera roll. It's always vertical.
 
        // Get the change in mouse wheel scroll and alter our zoom distance
        Distance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * 5, DistanceMin, DistanceMax);
 
        // Do not allow us to collide with anything??
        RaycastHit hit;
        if (Physics.Linecast (Target.position, transform.position, out hit)) 
        {
            Distance -=  hit.distance;
        }
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -Distance);
        Vector3 position = rotation * negDistance + Target.position;
 
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