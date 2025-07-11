using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    private Vector2 moveAmt;
    private Vector2 lookAmt;

    public float walkSpeed = 5f;
    public float rotationSpeed = 2f;
    public float jumpForce = 5f;

    public Rigidbody rb;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        moveAction = inputActions.FindAction("Move");
        lookAction = inputActions.FindAction("Look");
        jumpAction = inputActions.FindAction("Jump");

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveAmt = moveAction.ReadValue<Vector2>();
        lookAmt = lookAction.ReadValue<Vector2>();

        if (jumpAction.WasPressedThisFrame())
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Walking();
        Rotating();
    }

    private void Jump()
    {
        rb.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.up, ForceMode.Impulse);
    }

    private void Walking()
    {
        rb.MovePosition(rb.position + transform.forward * moveAmt.y * walkSpeed * Time.deltaTime);
    }

    private void Rotating()
    {
        if (moveAmt.y != 0)
        {
            float rotationAmount = lookAmt.x * rotationSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0, rotationAmount, 0);
            rb.MoveRotation(rb.rotation * rotation);
        }
    }
}

