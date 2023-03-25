using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallJumping : MonoBehaviour
{
    [Header("WallRuning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallJumpUpForce;
    public float wallJumpSideForce;

    [Header("Input")]
    private ThridPersonAsset playerActionsAsset;
    private InputAction jump;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit forwardWallhit;
    private bool wallForward;

    [Header("Setup")]
    public Transform orientation;
    private Rigidbody rb;
    public Transform PObj;

    private void Awake()
    {
        playerActionsAsset = new ThridPersonAsset();
    }
    private void OnEnable()
    {
        jump = playerActionsAsset.Player.Jump;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckForWall();
        StateMachine();
    }
    private void CheckForWall()
    {
        wallForward = Physics.Raycast(PObj.position, PObj.forward, out forwardWallhit, wallCheckDistance, whatIsWall);
    }
    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }
    private void StateMachine()
    {
        if (wallForward && jump.WasPressedThisFrame() && AboveGround())
        {
            WallJump();
        }
    }
    private void WallJump()
    {
        Vector3 wallNormal = forwardWallhit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
}
