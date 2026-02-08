using UnityEngine;
using System;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 2f;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDrainPerSecond = 20f;
    [SerializeField] private float staminaRecoveryPerSecond = 10f;

    private float currentStamina;
    private float currentMoveSpeed;
    private Vector3 velocity;

    [Header("Ground Checking")]
    [SerializeField] private Transform groundCheck;
    [SerializeField][Range(0.1f, 1f)] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask waterMask;

    private CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        currentMoveSpeed = moveSpeed;
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (GameManager.Instance.isPaused) return;

        Sprint();
        Move();
        ApplyGravity();

        if (Input.GetButton("Jump") && IsGrounded())
        {
            Jump();
        }

        GameUIManager.Instance.UpdateStaminaBar(currentStamina, maxStamina);
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * x + transform.forward * z;
        characterController.Move(direction * currentMoveSpeed * Time.deltaTime);
    }

    void Sprint()
    {
        bool sprintInput = Input.GetButton("Sprint");

        if (sprintInput && currentStamina > 0f)
        {
            currentMoveSpeed = sprintSpeed;

            currentStamina -= staminaDrainPerSecond * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
        else
        {
            currentMoveSpeed = moveSpeed;

            currentStamina += staminaRecoveryPerSecond * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }

    void ApplyGravity()
    {
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, checkRadius, groundMask);
    }

    bool onWater()
    {
        return Physics.CheckSphere(groundCheck.position, checkRadius, waterMask);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
