using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float moveSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpForce = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    void Update()
    {
        Move();

        if (Input.GetButton("Jump") && IsGrounded())
        {
            Jump();
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * x + transform.forward * z;

        characterController.Move(direction * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
