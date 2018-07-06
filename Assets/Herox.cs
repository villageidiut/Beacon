using UnityEngine;

public class Herox : MonoBehaviour
{
    public float MoveSpeed;
    public float XSpeed = 90.0f;

    // Use this for initialization
    void Start()
    {
        MoveSpeed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the player
        float x = Input.GetAxis("Mouse X") * XSpeed;
        Vector3 newRotation = new Vector3(0, x ,0);
        transform.Rotate(newRotation);

        // Move the player relative to the direction they are pointing.
        transform.Translate(
            MoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime,
            0f,
            MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime
        );
    }
}