using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class new_PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public int moveSpeed;

    public float groundDrag;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private ThridPersonAsset playerActionsAsset;
    private InputAction move;

    private void Awake()
    {
        playerActionsAsset = new ThridPersonAsset();
    }
    private void OnEnable()
    {
        playerActionsAsset.Player.Jump.started += PressJump;
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Jump.started -= PressJump;
        playerActionsAsset.Player.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation= true;
        readyToJump = true;
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        
        MyInput();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;    
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    { 
        horizontalInput = move.ReadValue<Vector2>().x;
        verticalInput = move.ReadValue<Vector2>().y;

    }
    private void MovePlayer()
    { 
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void PressJump(InputAction.CallbackContext obj)
    {
        if (readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
