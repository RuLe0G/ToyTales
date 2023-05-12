using System.Collections;
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
    private Transform orientation;
    private Rigidbody rb;
    private Transform PObj;

    Animator anim;

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
        new_PlayerMovement pm = GetComponent<new_PlayerMovement>();
        orientation = pm.orientation;
        PObj = pm.PObj;
        anim = pm.playerAnim;
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
        anim.SetTrigger("WallJump");
        Vector3 wallNormal = forwardWallhit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        StopCoroutine(SmoothTurn(0.2f));
        StartCoroutine(SmoothTurn(0.2f));

        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
    private IEnumerator SmoothTurn(float duration)
    {
        Quaternion startRotation = PObj.transform.rotation;
        Quaternion endRotation = Quaternion.LookRotation(-PObj.transform.forward, transform.up);

        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            PObj.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        PObj.transform.rotation = endRotation;
    }
}
