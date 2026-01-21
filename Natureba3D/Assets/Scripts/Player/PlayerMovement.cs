using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpForce = 2f;
    public float sprintSpeed = 8f;
    float currentMoveSpeed;

    public Transform groundCheck;
    [Range(0.1f, 1f)]
    public float checkRadius = 0.2f;
    public LayerMask groundMask;

    Vector3 velocity;

    void Start()
    {
        currentMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            if (IsGrounded() && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetButton("Sprint"))
            {
                currentMoveSpeed = sprintSpeed;
            }
            else
            {
                currentMoveSpeed = moveSpeed;
            }

            Move();

            if (Input.GetButton("Jump") && IsGrounded())
            {
                Jump();
            }

            velocity.y += gravity * Time.deltaTime;
            
            characterController.Move(velocity * Time.deltaTime);
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * x + transform.forward * z;

        characterController.Move(direction * currentMoveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, checkRadius, groundMask);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
