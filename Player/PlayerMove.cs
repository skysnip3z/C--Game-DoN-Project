using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerSpeed = 4.0f;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity.y = 0.0f;     
    }

    // Fixed update is called frame-rate independent
    void FixedUpdate()
    {
        float measuredSpeed = playerSpeed * Time.deltaTime;
        float moveForwardBack = Input.GetAxis("Vertical") * measuredSpeed;
        float moveLeftRight = Input.GetAxis("Horizontal") * measuredSpeed;

        velocity.z = moveForwardBack;
        velocity.x = moveLeftRight;

        transform.Translate(velocity);
    }
}
