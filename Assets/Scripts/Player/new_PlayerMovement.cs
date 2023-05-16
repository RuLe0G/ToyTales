using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class new_PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float wallrunSpeed;
    public float dashSpeed;
    public float dashSpeedChangeFactor;
    public float speedIncreaseMultiplier;
    public float climbSpeed;

    public float groundDrag;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Additional functions")]
    private float coyoteTime = 0.2f;
    public float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;

    public Transform orientation;
    public Transform PObj;
    public Animator playerAnim;


    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        air,
        dashing,
        climbing,
        wallRuning
    }

    public bool dashing;
    public bool climbing;
    public bool wallrunning;

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
        rb.freezeRotation = true;
        rb.useGravity = true;
        Physics.gravity = new Vector3(0, -9.8f * 2, 0);
        readyToJump = true;
    }

    public void TeleportTo(Vector3 v)
    {
        gameObject.transform.position = v;
    }


    private void Update()
    {
        float groundCheckRadius = 0.3f;
        Vector3 groundCheckPosition = transform.position + Vector3.down * (playerHeight * 0.5f + groundCheckRadius);
        grounded = Physics.CheckSphere(groundCheckPosition, groundCheckRadius, whatIsGround);
        if (grounded == false)
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        }
        if (grounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        MyInput();
        SpeedControl();
        StateHandler();

        if (state == MovementState.walking)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        CheckJumpBuffer();
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
    bool bufJump = false;
    private void CheckJumpBuffer()
    {
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            jumpBufferCounter = 0;
            bufJump = true;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;
    private bool keepMomentum;

    private void StateHandler()
    {
        if (climbing)
        {
            state = MovementState.climbing;
            desiredMoveSpeed = climbSpeed;
        }
        else if (dashing)
        {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
        }
        else if (wallrunning)
        {
            state = MovementState.wallRuning;
            desiredMoveSpeed = wallrunSpeed;
            playerAnim.SetFloat("WallRun", 1);
        }
        else if (grounded)
        {
            playerAnim.SetFloat("WallRun", 0);
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;

            if (desiredMoveSpeed < moveSpeed)
                desiredMoveSpeed = moveSpeed;
        }

        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.dashing) keepMomentum = true;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;

    }
    private float speedChangeFactor;
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            time += Time.deltaTime * boostFactor;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }

    private int VectorRound(Vector3 a)
    {
        int x = Mathf.Abs(Mathf.RoundToInt(a.x));
        int y = Mathf.Abs(Mathf.RoundToInt(a.y));
        int z = Mathf.Abs(Mathf.RoundToInt(a.z));

        return x + y + z;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        playerAnim.SetInteger("Speed", VectorRound(moveDirection));

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        if (!wallrunning) rb.useGravity = true;
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
        if (grounded && readyToJump && bufJump)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        else
        {
            jumpBufferCounter = jumpBufferTime;
        }
    }
    private void Jump()
    {
        playerAnim.SetTrigger("Jump");
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
