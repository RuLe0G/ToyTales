using UnityEngine;
using UnityEngine.InputSystem;

public class Climbing : MonoBehaviour
{
    [Header("Setup")]
    public Transform orientation;
    private Rigidbody rb;
    public new_PlayerMovement pm;
    public Transform PObj;
    public LayerMask whatIsWall;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;

    private bool climbing;

    [Header("Detection")]
    public float detectionLenght;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLockAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    [Header("Input")]
    private ThridPersonAsset playerActionsAsset;
    private InputAction move;
    private void Awake()
    {
        playerActionsAsset = new ThridPersonAsset();
    }
    private void OnEnable()
    {
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Disable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<new_PlayerMovement>();
    }
    private void Update()
    {
        WallCheck();
        StateMachine();

        if (climbing) ClimbingMovement();
    }
    private void StateMachine()
    {
        if (wallFront && move.ReadValue<Vector2>().y > 0 && wallLockAngle < maxWallLookAngle)
        {
            if (!climbing && climbTimer > 0) StartClimb();

            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimb();
        }

        else
        {
            if (climbing) StopClimb();
        }
    }
    private void WallCheck()
    {
        wallFront = Physics.SphereCast(PObj.position, sphereCastRadius, PObj.forward, out frontWallHit, detectionLenght, whatIsWall);
        wallLockAngle = Vector3.Angle(PObj.forward, -frontWallHit.normal);

        if (pm.grounded)
        {
            climbTimer = maxClimbTime;
        }
    }

    private void StartClimb()
    {
        climbing = true;
        pm.climbing = true;
    }
    private void ClimbingMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
    }
    private void StopClimb()
    {
        climbing = false;
        pm.climbing = false;
    }
}
